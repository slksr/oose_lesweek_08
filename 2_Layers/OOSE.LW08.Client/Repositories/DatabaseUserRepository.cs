using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OOSE.LW08.Client.Data;
using OOSE.LW08.Client.Models;

namespace OOSE.LW08.Client.Repositories
{
    public class DatabaseUserRepository : IDatabaseUserRepository
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
