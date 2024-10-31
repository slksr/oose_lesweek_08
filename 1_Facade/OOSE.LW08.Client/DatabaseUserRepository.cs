using Microsoft.EntityFrameworkCore;
using OOSE.LW08.Client.Models;
using System.Threading.Tasks;

namespace OOSE.LW08.Client
{
    public class DatabaseUserRepository
    {
        private readonly UsersDbContext _context;

        public DatabaseUserRepository(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
