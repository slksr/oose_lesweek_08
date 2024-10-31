using System;
using System.Threading.Tasks;
using OOSE.LW08.Domain.Entities;
using OOSE.LW08.Repositories;

namespace OOSE.LW08.Services
{
    public class ProfileReaderService : IProfileReaderService
    {
        private readonly IDatabaseUserRepository _userRepository;
        private readonly IFileUserPreferencesRepository _userPreferencesRepository;

        public ProfileReaderService(IDatabaseUserRepository userRepository, IFileUserPreferencesRepository userPreferencesRepository)
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
