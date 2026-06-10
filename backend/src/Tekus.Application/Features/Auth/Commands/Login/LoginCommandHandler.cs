using MediatR;
using Tekus.Application.Common.Exceptions;
using Tekus.Application.Common.Interfaces;
using Tekus.Application.Features.Auth.DTOs;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new ValidationException("Invalid email or password.");
        }

        var token = _tokenService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            FullName = user.FullName,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60) // Should match JWT expiration
        };
    }
}
