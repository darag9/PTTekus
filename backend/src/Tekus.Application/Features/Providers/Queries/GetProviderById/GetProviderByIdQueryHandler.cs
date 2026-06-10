using AutoMapper;
using MediatR;
using Tekus.Application.Common.Exceptions;
using Tekus.Application.Features.Providers.DTOs;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Providers.Queries.GetProviderById;

public class GetProviderByIdQueryHandler : IRequestHandler<GetProviderByIdQuery, ProviderDetailDto>
{
    private readonly IProviderRepository _providerRepository;
    private readonly IMapper _mapper;

    public GetProviderByIdQueryHandler(IProviderRepository providerRepository, IMapper mapper)
    {
        _providerRepository = providerRepository;
        _mapper = mapper;
    }

    public async Task<ProviderDetailDto> Handle(GetProviderByIdQuery request, CancellationToken cancellationToken)
    {
        var provider = await _providerRepository.GetWithServicesAsync(request.Id, cancellationToken);

        if (provider == null)
        {
            throw new NotFoundException(nameof(Provider), request.Id);
        }

        return _mapper.Map<ProviderDetailDto>(provider);
    }
}
