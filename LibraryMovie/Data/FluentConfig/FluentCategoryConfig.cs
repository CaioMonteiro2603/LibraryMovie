
using LibraryMovie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryMovie.Data.FluentConfig
{
    public class FluentCategoryConfig : IEntityTypeConfiguration<CategoryModel>
    {
        public void Configure(EntityTypeBuilder<CategoryModel> modelBuilder)
        {
           
            modelBuilder
                .Property(c => c.Theme)
                .HasColumnName("Theme");
        }
    }
}
