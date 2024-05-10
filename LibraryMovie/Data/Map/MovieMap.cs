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
            builder.HasOne(x => x.User);
            builder.HasOne(x => x.Category);
        }
    }
}
