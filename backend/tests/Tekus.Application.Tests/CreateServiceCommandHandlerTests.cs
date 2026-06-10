using Moq;
using Tekus.Application.Features.Services.Commands.CreateService;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Tests;

public class CreateServiceCommandHandlerTests
{
    private readonly Mock<IServiceRepository> _serviceRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateServiceCommandHandler _handler;

    public CreateServiceCommandHandlerTests()
    {
        _serviceRepositoryMock = new Mock<IServiceRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateServiceCommandHandler(_serviceRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsServiceId()
    {
        // Arrange
        var command = new CreateServiceCommand("Test Service", 50.0m);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
        _serviceRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Service>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
