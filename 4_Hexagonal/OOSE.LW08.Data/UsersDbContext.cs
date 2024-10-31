using Microsoft.EntityFrameworkCore;
using OOSE.LW08.Domain.Entities;

namespace OOSE.LW08.Data
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
