using Microsoft.EntityFrameworkCore;

namespace Wpm.Management.Api.DataAccess
{
    public class ManagementDbContext(DbContextOptions<ManagementDbContext> options): DbContext(options)
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Breed> Breeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pet>().HasData(
                [
                    new Pet(){Id = 1, Name = "Peluche", Age = 13, BreedId=1},
                    new Pet(){Id = 2, Name = "Chata", Age = 10, BreedId=2},
                    new Pet(){Id = 3, Name = "Bowie", Age = 5, BreedId=3}
                ]
                );
            modelBuilder.Entity<Breed>().HasData(
                [
                    new Breed(1, "Cocker" ),
                    new Breed(2, "Pug" ),
                    new Breed(3, "Airedale Terrier" )
                ]                
                );
        }
    }

    public static class ManagementDbContextExtensions
    {
        public static void EnsureDbIsCreated(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ManagementDbContext>();
            context!.Database.EnsureCreated();
        }
    }
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int BreedId { get; set; }

        public Breed Breed { get; set; }
    }

    public record Breed(int Id, string Name);
}
