using AutoMapper;
using Tekus.Application.Features.Providers.DTOs;
using Tekus.Application.Features.Services.DTOs;
using Tekus.Domain.Entities;

namespace Tekus.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Provider, ProviderDto>()
            .ForMember(d => d.ServiceCount, opt => opt.MapFrom(s => s.ProviderServices.Count));

        CreateMap<Provider, ProviderDetailDto>()
            .ForMember(d => d.ServiceCount, opt => opt.MapFrom(s => s.ProviderServices.Count))
            .ForMember(d => d.Services, opt => opt.MapFrom(s => s.ProviderServices));

        CreateMap<ProviderService, ProviderServiceDto>()
            .ForMember(d => d.ServiceName, opt => opt.MapFrom(s => s.Service.Name))
            .ForMember(d => d.HourlyRate, opt => opt.MapFrom(s => s.Service.HourlyRate));

        CreateMap<Service, ServiceDto>()
            .ForMember(d => d.ProviderCount, opt => opt.MapFrom(s => s.ProviderServices.Count));
    }
}
