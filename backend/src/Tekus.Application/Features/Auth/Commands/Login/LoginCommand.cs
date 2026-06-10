using MediatR;
using Tekus.Application.Features.Auth.DTOs;

namespace Tekus.Application.Features.Auth.Commands.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;
