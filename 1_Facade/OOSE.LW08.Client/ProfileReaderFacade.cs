using OOSE.LW08.Client.Models;
using System;
using System.Threading.Tasks;

namespace OOSE.LW08.Client
{
    /// <summary>
    /// Facade voorbeeld
    /// </summary>
    /// <remarks>
    /// Het is niet nodig om Facade in de naam te laten terugkomen, alleen hier voor het voorbeeld.
    /// </remarks>
    public class ProfileReaderFacade
    {
        private readonly DatabaseUserRepository _userRepository;
        private readonly FileUserPreferencesRepository _userPreferencesRepository;

        public ProfileReaderFacade(DatabaseUserRepository userRepository, FileUserPreferencesRepository userPreferencesRepository)
        {
            _userRepository = userRepository;
            _userPreferencesRepository = userPreferencesRepository;
        }

        public async Task<Profile> GetByUsername(string username)
        {
            User user = await _userRepository.GetByUsername(username);
            UserPreferences preferences = await _userPreferencesRepository.GetByUserId(user.Id);

            if (preferences.IsPrivate)
            {
                throw new Exception("User is private, not allowed to read profile");
            }

            Profile profile = new Profile()
            {
                Username = user.Username,
                ColorTheme = preferences.ColorTheme
            };

            return profile;
        }
    }
}
