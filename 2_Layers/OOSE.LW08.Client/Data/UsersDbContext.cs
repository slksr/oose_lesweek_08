using Microsoft.EntityFrameworkCore;
using OOSE.LW08.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOSE.LW08.Client.Data
{
    public class UsersDbContext : DbContext
    {
        private Guid _userId = Guid.NewGuid();

        public UsersDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
