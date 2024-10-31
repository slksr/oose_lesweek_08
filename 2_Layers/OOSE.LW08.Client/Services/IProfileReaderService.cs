using System.Threading.Tasks;
using OOSE.LW08.Client.Models;

namespace OOSE.LW08.Client.Services
{
    public interface IProfileReaderService
    {
        Task<Profile> GetByUsername(string username);
    }
}