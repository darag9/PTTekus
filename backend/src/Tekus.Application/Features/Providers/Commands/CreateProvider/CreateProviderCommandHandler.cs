using MediatR;
using Tekus.Application.Common.Exceptions;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Providers.Commands.CreateProvider;

public class CreateProviderCommandHandler : IRequestHandler<CreateProviderCommand, Guid>
{
    private readonly IProviderRepository _providerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProviderCommandHandler(IProviderRepository providerRepository, IUnitOfWork unitOfWork)
    {
        _providerRepository = providerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
    {
        var existingProvider = await _providerRepository.GetByNitAsync(request.Nit, cancellationToken);
        if (existingProvider != null)
        {
            throw new ValidationException(new Dictionary<string, string[]>
            {
                { "Nit", new[] { "The specified NIT is already in use." } }
            });
        }

        var provider = Provider.Create(
            request.Nit,
            request.Name,
            request.WebsiteUrl,
            request.Email,
            request.Country);

        await _providerRepository.AddAsync(provider);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return provider.Id;
    }
}
