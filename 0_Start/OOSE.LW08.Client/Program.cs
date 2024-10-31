using Microsoft.EntityFrameworkCore;
using OOSE.LW08.Client.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

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

            await Run(userRepository, userPreferencesRepository);
        }

        private static async Task Run(DatabaseUserRepository userRepository, FileUserPreferencesRepository userPreferencesRepository)
        {
            User user = await userRepository.GetByUsername("Donald Duck");
            UserPreferences userPreferences = await userPreferencesRepository.GetByUserId(user.Id);

            if (userPreferences.IsPrivate)
            {
                throw new Exception("User is private, not allowed to read profile");
            }
            Profile profile = new Profile()
            {
                Username = user.Username,
                ColorTheme = userPreferences.ColorTheme
            };

            Console.WriteLine($"User Profile\n{profile}");
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
                IsPrivate = false,
                ColorTheme = ColorTheme.Dark
            };
            string userPreferencesJson = JsonSerializer.Serialize(userPreferences);

            string userPreferencesPath = Path.Combine(userPath, "preferences.json");
            await File.WriteAllTextAsync(userPreferencesPath, userPreferencesJson);
        }
    }
}
