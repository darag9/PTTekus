using FluentValidation;

namespace Tekus.Application.Features.Services.Commands.CreateService;

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.HourlyRate).GreaterThanOrEqualTo(0);
    }
}
