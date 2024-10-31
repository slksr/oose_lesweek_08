using Microsoft.EntityFrameworkCore;
using OOSE.LW08.Client.Models;

namespace OOSE.LW08.Client
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
