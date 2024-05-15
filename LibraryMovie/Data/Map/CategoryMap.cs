using LibraryMovie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMovie.Data.Map
{
    public class CategoryMap : IEntityTypeConfiguration<CategoryModel> 
    {
        public void Configure(EntityTypeBuilder<CategoryModel> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Theme);
            builder.Property(x => x.UserId);

            builder.HasOne(x => x.User);
            
        }

    }
}
