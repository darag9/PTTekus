using MediatR;
using Tekus.Application.Common.Exceptions;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Services.Commands.DeleteService;

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
{
    private readonly IServiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteServiceCommandHandler(IServiceRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (service == null)
        {
            throw new NotFoundException(nameof(Tekus.Domain.Entities.Service), request.Id);
        }

        await _repository.DeleteAsync(service, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
