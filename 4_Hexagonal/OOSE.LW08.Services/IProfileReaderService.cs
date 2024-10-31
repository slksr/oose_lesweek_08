using System.Threading.Tasks;
using OOSE.LW08.Domain.Entities;

namespace OOSE.LW08.Services
{
    public interface IProfileReaderService
    {
        Task<Profile> GetByUsername(string username);
    }
}