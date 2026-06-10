using MediatR;
using Tekus.Application.Common.Exceptions;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Providers.Commands.DeleteProvider;

public class DeleteProviderCommandHandler : IRequestHandler<DeleteProviderCommand>
{
    private readonly IProviderRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProviderCommandHandler(IProviderRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (provider == null)
        {
            throw new NotFoundException(nameof(Tekus.Domain.Entities.Provider), request.Id);
        }

        await _repository.DeleteAsync(provider, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
