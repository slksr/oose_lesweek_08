using System;
using System.Threading.Tasks;
using OOSE.LW08.Client.Models;

namespace OOSE.LW08.Client.Repositories
{
    public interface IFileUserPreferencesRepository
    {
        Task<UserPreferences> GetByUserId(Guid userId);
    }
}