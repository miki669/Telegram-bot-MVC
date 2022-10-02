using Microsoft.EntityFrameworkCore;
using Shop.Context.Table;

using System.Collections.Generic;

namespace Shop.Context
{
    public class PrimaryDatabaseContext : DbContext
    {

        public PrimaryDatabaseContext(DbContextOptions<PrimaryDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        public void CancelChanges() 
        {
            foreach (var Entry in ChangeTracker.Entries().ToList())
            {
                switch (Entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        Entry.State = EntityState.Modified;
                        Entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        Entry.State = EntityState.Detached;
                        break;
                }
            }
        }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

        public DbSet<Users> Users { get; set; }
        
        
    }
   

}
