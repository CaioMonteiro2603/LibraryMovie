using LibraryMovie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMovie.Data.FluentConfig
{
    public class FluentMovieConfig : IEntityTypeConfiguration<MoviesModel>
    {
        public void Configure(EntityTypeBuilder<MoviesModel> modelBuilder)
        {
            modelBuilder.HasKey(c => c.Id);

            modelBuilder.ToTable("Movies");

            modelBuilder.Property(c => c.Title).HasColumnName("title");
        }
    }
}
