using MediatR;
using Tekus.Application.Common.Exceptions;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Providers.Commands.AddServiceToProvider;

public class AddServiceToProviderCommandHandler : IRequestHandler<AddServiceToProviderCommand>
{
    private readonly IProviderRepository _providerRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddServiceToProviderCommandHandler(
        IProviderRepository providerRepository,
        IServiceRepository serviceRepository,
        IUnitOfWork unitOfWork)
    {
        _providerRepository = providerRepository;
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddServiceToProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetWithServicesAsync(request.ProviderId, cancellationToken);
        if (provider == null)
        {
            throw new NotFoundException(nameof(Provider), request.ProviderId);
        }

        var service = await _serviceRepository.GetByIdAsync(request.ServiceId);
        if (service == null)
        {
            throw new NotFoundException(nameof(Service), request.ServiceId);
        }

        provider.AddService(service.Id, service.Name, request.CustomHourlyRate);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
