using MediatR;
using Tekus.Application.Common.Exceptions;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Services.Commands.UpdateService;

public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateServiceCommandHandler(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(request.Id);
        
        if (service == null)
        {
            throw new NotFoundException(nameof(Service), request.Id);
        }

        service.Update(request.Name, request.HourlyRate);

        await _serviceRepository.UpdateAsync(service);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
