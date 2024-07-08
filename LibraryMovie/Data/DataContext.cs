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
            base.OnModelCreating(modelBuilder);
            //seeding data 
            modelBuilder.Entity<UsersModel>().HasData(
                new UsersModel {Id = 1,  Name = "Caio", Email = "caiomonteiropro@gmail.com", Password = "123789", Role = "admin" }
            );

            modelBuilder.Entity<CategoryModel>()
               .ToTable("MovieCategory")
               .HasKey(c => c.MovieCategoryId);

            modelBuilder.Entity<CategoryModel>()
                .HasMany(c => c.Movies)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsersModel>()
              .HasMany(c => c.Categories)
              .WithOne(c => c.Users)
              .HasForeignKey(c => c.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsersModel>()
                .HasMany(c => c.Movies)
                .WithOne(c => c.Users)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
