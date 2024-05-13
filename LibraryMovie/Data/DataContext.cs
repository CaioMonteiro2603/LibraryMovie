using LibraryMovie.Data.Map;
using LibraryMovie.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMovie.Data
{
    public class DataContext : DbContext
    {
        public DbSet<UsersModel> Users { get; set; }

        public DbSet<MoviesModel> Movies { get; set; }

        public DbSet<CategoryModel> Category { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected DataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieMap()); 

            base.OnModelCreating(modelBuilder);

            //seeding data 
            modelBuilder.Entity<UsersModel>().HasData(
                new UsersModel {Id = 1,  Name = "Caio", Email = "caiomonteiropro@gmail.com", Password = "123789", Role = "admin" }
            );
        }
    }
}
