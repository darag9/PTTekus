using Moq;
using Tekus.Application.Features.Providers.Commands.CreateProvider;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Application.Common.Exceptions;

namespace Tekus.Application.Tests;

public class CreateProviderCommandHandlerTests
{
    private readonly Mock<IProviderRepository> _providerRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateProviderCommandHandler _handler;

    public CreateProviderCommandHandlerTests()
    {
        _providerRepositoryMock = new Mock<IProviderRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateProviderCommandHandler(_providerRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsProviderId()
    {
        // Arrange
        var command = new CreateProviderCommand("12345", "Test", "http://test.com", "test@test.com", "USA");
        _providerRepositoryMock.Setup(r => r.GetByNitAsync(command.Nit, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Provider?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        _providerRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Provider>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DuplicateNit_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateProviderCommand("12345", "Test", "http://test.com", "test@test.com", "USA");
        _providerRepositoryMock.Setup(r => r.GetByNitAsync(command.Nit, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Provider.Create("12345", "Old", "http://test.com", "old@test.com", "USA"));

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
