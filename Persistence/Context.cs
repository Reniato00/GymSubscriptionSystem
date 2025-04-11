
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;


namespace GymSubscriptionSystem.Persistence
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {}

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Plans> Plans { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plans>().ToTable("plans");
            modelBuilder.Entity<Customer>().ToTable("customers");
            modelBuilder.Entity<Instructor>().ToTable("instructors");

            modelBuilder.Entity<Plans>().HasKey(p => p.Id);
            modelBuilder.Entity<Customer>().HasKey(c => c.Id);
            modelBuilder.Entity<Instructor>().HasKey(i => i.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}