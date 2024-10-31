using System.Threading.Tasks;
using OOSE.LW08.Client.Models;

namespace OOSE.LW08.Client.Repositories
{
    public interface IDatabaseUserRepository
    {
        Task<User> GetByUsername(string username);
    }
}