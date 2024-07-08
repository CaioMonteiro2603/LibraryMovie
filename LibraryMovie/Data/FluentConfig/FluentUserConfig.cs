using LibraryMovie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMovie.Data.FluentConfig
{
    public class FluentUserConfig : IEntityTypeConfiguration<UsersModel>
    {
        public void Configure(EntityTypeBuilder<UsersModel> modelBuilder) 
        {
            modelBuilder.HasKey(c => c.Id);

            modelBuilder.ToTable("Users"); 

            modelBuilder.Property(c => c.Name).HasColumnName("Name");

            modelBuilder.Property(c => c.Password)
                .HasColumnName("Password")
                .HasMaxLength(12);

            modelBuilder.Property(c => c.Role).HasColumnName("Role");

        }
    }
}
