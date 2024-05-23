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

            modelBuilder.Entity<MoviesModel>()
                .ToTable("Movies")
                .HasKey(c => c.Id);

            modelBuilder.Entity<MoviesModel>()
                .Property(c => c.Title)
                .HasColumnName("title"); 



            modelBuilder.Entity<UsersModel>()
                .ToTable("Users")
                .HasKey(c => c.Id);

            modelBuilder.Entity<UsersModel>()
                .Property(c => c.Name)
                .HasColumnName("Name");

            modelBuilder.Entity<UsersModel>()
                .Property(c => c.Password)
                .HasColumnName("Password")
                .HasMaxLength(12);

            modelBuilder.Entity<UsersModel>()
                .Property(c => c.Role)
                .HasColumnName("Role");
                

            modelBuilder.Entity<CategoryModel>()
                .ToTable("MovieCategory")
                .HasKey(c => c.MovieCategoryId);

            modelBuilder.Entity<CategoryModel>()
                .Property(c => c.Theme)
                .HasColumnName("Theme");
                

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
