using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tekus.Application.Common.Interfaces;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Persistence;
using Tekus.Infrastructure.Persistence.Repositories;
using Tekus.Infrastructure.Services;

namespace Tekus.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                options.UseInMemoryDatabase("TekusDb");
            }
            else
            {
                options.UseSqlServer(connectionString);
            }
        });

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IProviderRepository, ProviderRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddTransient<ITokenService, JwtTokenService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
