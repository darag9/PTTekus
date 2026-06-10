using MediatR;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Services.Commands.CreateService;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateServiceCommandHandler(IServiceRepository serviceRepository, IUnitOfWork unitOfWork)
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var service = Service.Create(request.Name, request.HourlyRate);

        await _serviceRepository.AddAsync(service);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return service.Id;
    }
}
