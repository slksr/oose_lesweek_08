using System.Threading.Tasks;
using OOSE.LW08.Domain.Entities;

namespace OOSE.LW08.Repositories
{
    public interface IDatabaseUserRepository
    {
        Task<User> GetByUsername(string username);
    }
}