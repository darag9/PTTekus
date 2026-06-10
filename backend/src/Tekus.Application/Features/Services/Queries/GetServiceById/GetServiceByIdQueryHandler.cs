using AutoMapper;
using MediatR;
using Tekus.Application.Common.Exceptions;
using Tekus.Application.Features.Services.DTOs;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Services.Queries.GetServiceById;

public class GetServiceByIdQueryHandler : IRequestHandler<GetServiceByIdQuery, ServiceDto>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;

    public GetServiceByIdQueryHandler(IServiceRepository serviceRepository, IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _mapper = mapper;
    }

    public async Task<ServiceDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(request.Id);

        if (service == null)
        {
            throw new NotFoundException(nameof(Service), request.Id);
        }

        return _mapper.Map<ServiceDto>(service);
    }
}
