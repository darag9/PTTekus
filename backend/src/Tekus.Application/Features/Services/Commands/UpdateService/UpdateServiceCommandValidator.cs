using FluentValidation;

namespace Tekus.Application.Features.Services.Commands.UpdateService;

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.HourlyRate).GreaterThanOrEqualTo(0);
    }
}
