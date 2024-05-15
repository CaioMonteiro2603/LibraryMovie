using LibraryMovie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMovie.Data.Map
{
    public class MovieMap : IEntityTypeConfiguration<MoviesModel>
    {
        public void Configure(EntityTypeBuilder<MoviesModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title);
            builder.Property(x => x.RegistrationDate);
            builder.Property(x => x.RunningTime);
            builder.Property(x => x.CategoryId);
            builder.Property(x => x.UserId);

            builder.HasOne(x => x.Users);
            builder.HasOne(x => x.Category); 
        }
    }
}
