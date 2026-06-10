using FluentValidation;

namespace Tekus.Application.Features.Providers.Commands.CreateProvider;

public class CreateProviderCommandValidator : AbstractValidator<CreateProviderCommand>
{
    public CreateProviderCommandValidator()
    {
        RuleFor(x => x.Nit).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(200);
        RuleFor(x => x.WebsiteUrl).MaximumLength(200);
        RuleFor(x => x.Country).NotEmpty().MaximumLength(100);
    }
}
