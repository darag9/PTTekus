using MediatR;
using Tekus.Application.Common.Exceptions;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Providers.Commands.UpdateProvider;

public class UpdateProviderCommandHandler : IRequestHandler<UpdateProviderCommand>
{
    private readonly IProviderRepository _providerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProviderCommandHandler(IProviderRepository providerRepository, IUnitOfWork unitOfWork)
    {
        _providerRepository = providerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetByIdAsync(request.Id);
        if (provider == null)
        {
            throw new NotFoundException(nameof(Provider), request.Id);
        }

        var existingProvider = await _providerRepository.GetByNitAsync(request.Nit, cancellationToken);
        if (existingProvider != null && existingProvider.Id != request.Id)
        {
            throw new ValidationException(new Dictionary<string, string[]>
            {
                { "Nit", new[] { "The specified NIT is already in use by another provider." } }
            });
        }

        provider.Update(request.Name, request.WebsiteUrl, request.Email, request.Country);

        await _providerRepository.UpdateAsync(provider);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
