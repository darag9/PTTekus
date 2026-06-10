using Tekus.Domain.Entities;
using Tekus.Infrastructure.Persistence;

namespace Tekus.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (context.Users.Any())
        {
            return; // DB has been seeded
        }

        // 1. Seed User
        var adminPassword = BCrypt.Net.BCrypt.HashPassword("Admin123!");
        var admin = User.Create("admin@tekus.com", adminPassword, "System Administrator");
        context.Users.Add(admin);

        // 2. Seed Services
        var services = new List<Service>
        {
            Service.Create("Descarga espacial de contenidos", 50.0m),
            Service.Create("Desaparición forzada de bytes", 75.5m),
            Service.Create("Desarrollo Web Frontend", 40.0m),
            Service.Create("Desarrollo Web Backend", 45.0m),
            Service.Create("Consultoría Cloud", 120.0m),
            Service.Create("Auditoría de Seguridad", 150.0m),
            Service.Create("Diseño UI/UX", 35.0m),
            Service.Create("Soporte Técnico L2", 25.0m)
        };
        context.Services.AddRange(services);

        // 3. Seed Providers
        var providers = new List<Provider>
        {
            Provider.Create("800123456-1", "Importaciones Tekus S.A.", "https://tekus.com", "contacto@tekus.com", "Colombia"),
            Provider.Create("900987654-2", "Global Cloud Tech", "https://globalcloud.com", "info@globalcloud.com", "USA"),
            Provider.Create("700456123-3", "Sistemas Andinos", "https://sistemasandinos.co", "ventas@sandinos.co", "Colombia"),
            Provider.Create("RFC-123456", "Desarrollos Azteca", "https://dazteca.mx", "hola@dazteca.mx", "Mexico"),
            Provider.Create("B-87654321", "Iberica Solutions SL", "https://ibericasolutions.es", "contacto@ibericasolutions.es", "España"),
            Provider.Create("CUIT-3012345", "Tango Software", "https://tangosw.ar", "info@tangosw.ar", "Argentina"),
            Provider.Create("900111222-4", "Tech Innovation SAS", "https://techinnov.co", "hello@techinnov.co", "Colombia"),
            Provider.Create("US-555666", "Silicon Valley IT", "https://svit.com", "sales@svit.com", "USA"),
            Provider.Create("MX-999888", "Soluciones Norte", "https://solnorte.mx", "ventas@solnorte.mx", "Mexico"),
            Provider.Create("ES-444333", "Madrid Tech Services", "https://madridtech.es", "info@madridtech.es", "España")
        };

        foreach (var provider in providers)
        {
            context.Providers.Add(provider);
        }

        await context.SaveChangesAsync();

        // 4. Seed ProviderServices
        // Tekus S.A.
        providers[0].AddService(services[0].Id, services[0].Name);
        providers[0].AddService(services[1].Id, services[1].Name);
        
        // Global Cloud Tech
        providers[1].AddService(services[4].Id, services[4].Name);
        providers[1].AddService(services[5].Id, services[5].Name);

        // Sistemas Andinos
        providers[2].AddService(services[2].Id, services[2].Name);
        providers[2].AddService(services[3].Id, services[3].Name);
        providers[2].AddService(services[7].Id, services[7].Name);

        // Desarrollos Azteca
        providers[3].AddService(services[2].Id, services[2].Name, 30.0m); // Custom rate
        providers[3].AddService(services[6].Id, services[6].Name);

        // Iberica Solutions
        providers[4].AddService(services[3].Id, services[3].Name, 50.0m);
        providers[4].AddService(services[4].Id, services[4].Name);

        // Tango Software
        providers[5].AddService(services[2].Id, services[2].Name);
        providers[5].AddService(services[7].Id, services[7].Name, 20.0m);

        // Tech Innovation
        providers[6].AddService(services[0].Id, services[0].Name);
        providers[6].AddService(services[3].Id, services[3].Name);

        // Silicon Valley IT
        providers[7].AddService(services[4].Id, services[4].Name, 150.0m);
        providers[7].AddService(services[5].Id, services[5].Name, 200.0m);

        // Soluciones Norte
        providers[8].AddService(services[7].Id, services[7].Name);

        // Madrid Tech Services
        providers[9].AddService(services[2].Id, services[2].Name);
        providers[9].AddService(services[6].Id, services[6].Name);

        // Explicitly add ProviderServices to the DbContext for InMemoryDatabase tracking
        var allProviderServices = providers.SelectMany(p => p.ProviderServices).ToList();
        context.ProviderServices.AddRange(allProviderServices);

        await context.SaveChangesAsync();
    }
}
