using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OOSE.LW08.Client.Data;
using OOSE.LW08.Client.Models;
using OOSE.LW08.Client.Repositories;
using OOSE.LW08.Client.Services;

namespace OOSE.LW08.Client
{
    public class App
    {
        private IHost _host;

        public App()
        {
            // De host wordt hier aangemaakt. De host is een dependency injection container waarin de services worden geregistreerd.
            _host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((context, service) =>
                {
                    service.AddDbContext<UsersDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("users");
                    });
                    // De services (businesslaag) en repositories (datalaag) worden hier geregistreerd. Dit is dependency injection.

                    // Scoped betekent dat er één nieuwe instantie van de service wordt gemaakt per aanvraag. Dit betekent dat als een service meerdere keren wordt aangevraagd binnen dezelfde aanvraag, dezelfde instantie wordt geretourneerd.
                    // Transient betekent dat er elke keer dat de service wordt aangevraagd een nieuwe instantie van de service wordt gemaakt. Dit betekent dat als een service meerdere keren wordt aangevraagd, verschillende instanties worden geretourneerd.
                    service.AddScoped<IDatabaseUserRepository, DatabaseUserRepository>();
                    service.AddScoped<IFileUserPreferencesRepository, FileUserPreferencesRepository>();
                    
                    service.AddScoped<IProfileReaderService, ProfileReaderService>();
                })
                .Build();
        }

        /// <summary>
        /// Deze run methode is de daadwerkelijke Client. Hierin wordt de gebruiker opgehaald en de bijbehorende voorkeuren.
        /// De client moet direct met de susbsystemen communiceren.
        /// </summary>
        /// <param name="ProfileReaderFacade"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Run()
        {
            // De ProfileReaderService wordt hier opgehaald uit de host. De host geeft dus een nieuwe instantie van de service.
            // Welk Design Pattern hoort hier bij?
            IProfileReaderService profileReader = _host.Services.GetRequiredService<IProfileReaderService>();
            try
            {
                Profile profile = await profileReader.GetByUsername("Donald Duck");
                Console.WriteLine($"\nUser Profile\n{profile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUser Profile\n{ex.Message}");
            }
        }

        /// <summary>
        /// Deze methode is verantwoordelijk voor het vullen van de database met demo data.
        /// </summary>
        /// <returns></returns>
        public async Task Initialize()
        {
            await Seed(_host.Services.GetRequiredService<UsersDbContext>());
        }

        private async Task Seed(UsersDbContext context)
        {
            Guid userId = await SeedUser(context);
            await SeedUserPreferences(userId);
        }

        private async Task<Guid> SeedUser(UsersDbContext context)
        {
            User user = new User()
            {
                Email = "test@han.nl",
                Username = "Donald Duck"
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user.Id;
        }

        private async Task SeedUserPreferences(Guid userId)
        {
            string userPath = Path.Combine("users", userId.ToString());
            Directory.CreateDirectory(userPath);

            UserPreferences userPreferences = new UserPreferences()
            {
                IsPrivate = true,
                ColorTheme = ColorTheme.Blue
            };
            string userPreferencesJson = JsonSerializer.Serialize(userPreferences);

            string userPreferencesPath = Path.Combine(userPath, "preferences.json");
            await File.WriteAllTextAsync(userPreferencesPath, userPreferencesJson);
        }
    }
}
