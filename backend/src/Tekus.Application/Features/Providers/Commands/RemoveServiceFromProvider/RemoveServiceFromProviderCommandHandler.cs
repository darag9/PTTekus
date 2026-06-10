using MediatR;
using Tekus.Application.Common.Exceptions;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Providers.Commands.RemoveServiceFromProvider;

public class RemoveServiceFromProviderCommandHandler : IRequestHandler<RemoveServiceFromProviderCommand>
{
    private readonly IProviderRepository _providerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveServiceFromProviderCommandHandler(IProviderRepository providerRepository, IUnitOfWork unitOfWork)
    {
        _providerRepository = providerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveServiceFromProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetWithServicesAsync(request.ProviderId, cancellationToken);
        if (provider == null)
        {
            throw new NotFoundException(nameof(Provider), request.ProviderId);
        }

        provider.RemoveService(request.ServiceId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
