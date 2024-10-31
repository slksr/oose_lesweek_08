using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using OOSE.LW08.Client.Models;

namespace OOSE.LW08.Client.Repositories
{
    public class FileUserPreferencesRepository : IFileUserPreferencesRepository
    {
        private const string USERS_DIRECTORY_NAME = "users";
        private const string USER_PREFERENCES_FILE_NAME = "preferences.json";

        public async Task<UserPreferences> GetByUserId(Guid userId)
        {
            string userPreferencesFilePath = Path.Combine(USERS_DIRECTORY_NAME, userId.ToString(), USER_PREFERENCES_FILE_NAME);

            string userPreferencesJson = await File.ReadAllTextAsync(userPreferencesFilePath);
            UserPreferences userPreferences = JsonSerializer.Deserialize<UserPreferences>(userPreferencesJson);

            return userPreferences;
        }
    }
}
