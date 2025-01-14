using Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Service
{
    public class TMSContext : DbContext
    {
        public TMSContext(DbContextOptions<TMSContext> options)
        : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}
