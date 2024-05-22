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
                .HasMany(c => c.Movies)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoryModel>()
                .HasOne(u => u.Users)
                .WithOne()
                .HasForeignKey<UsersModel>(u => u.CategoryId); 
                
            modelBuilder.Entity<UsersModel>()
                .HasMany(c => c.Movies)
                .WithOne(c => c.Users)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

           modelBuilder.Entity<MoviesModel>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Movies)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MoviesModel>()
                .HasOne(c => c.Users)
                .WithMany(c => c.Movies)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
