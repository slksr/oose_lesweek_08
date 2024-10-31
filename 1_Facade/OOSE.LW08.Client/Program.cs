using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOSE.LW08.Client.Models;


namespace OOSE.LW08.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseInMemoryDatabase("users").Options;
            UsersDbContext context = new UsersDbContext(options);
            await Seed(context);

            DatabaseUserRepository userRepository = new DatabaseUserRepository(context);
            FileUserPreferencesRepository userPreferencesRepository = new FileUserPreferencesRepository();

            ProfileReaderFacade profileReader = new ProfileReaderFacade(userRepository, userPreferencesRepository);

            await Run(profileReader);
        }

        /// <summary>
        /// Deze run methode is de daadwerkelijke Client. Hierin wordt de gebruiker opgehaald en de bijbehorende voorkeuren.
        /// De client moet direct met de susbsystemen communiceren.
        /// </summary>
        /// <param name="ProfileReaderFacade"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static async Task Run(ProfileReaderFacade profileReader)
        {
            try
            {
                Profile profile = await profileReader.GetByUsername("Donald Duck");
                Console.WriteLine($"User Profile\n{profile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"User Profile\n{ex.Message}");
            }
        }

        private static async Task Seed(UsersDbContext context)
        {
            Guid userId = await SeedUser(context);
            await SeedUserPreferences(userId);
        }

        private static async Task<Guid> SeedUser(UsersDbContext context)
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

        private static async Task SeedUserPreferences(Guid userId)
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
