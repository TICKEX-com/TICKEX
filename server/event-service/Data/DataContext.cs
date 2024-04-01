using Microsoft.EntityFrameworkCore;
using event_service.Entities;
using System.Linq.Expressions;

namespace event_service.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Title = "Match",
                    Description = "i am a football match",
                    Date = DateTime.Now,
                    Location = "maps",
                    MinPrize = 500,
                    DesignId = 1,
                    OrganizerId = 1,
                    CategoryId = 1
                }
                );
                modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Sport",
                }
                );
                modelBuilder.Entity<Event>().HasData(
                    new Event
                    {
                        Id = 2,
                        Title = "Cinema",
                        Description = "i am a movie",
                        Date = DateTime.Now,
                        Location = "maps",
                        MinPrize = 500,
                        DesignId = 2,
                        OrganizerId = 2,
                        CategoryId = 2
                    }
                );
                modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 2,
                    Name = "Cinema",
                }
                );
            } catch (Exception ex)
            {
                Console.WriteLine( ex.ToString() );
                Console.ReadLine();
            }
            
        }

    }
}
