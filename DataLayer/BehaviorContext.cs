using Dao.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dao
{
    public class BehaviorContext : DbContext
    {
        public BehaviorContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DateBase.db");
        }
    }
}