using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMovie.Models
{
    [Table("MovieCategory")]
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieCategoryId { get; set; }

        [Required]
        [StringLength(15)]
        public string Theme {  get; set; }

        public virtual ICollection<MoviesModel>? Movies { get; set; }

        public UsersModel Users { get; set; }
        public CategoryModel()
        {
            
        }

        public CategoryModel(int movieCategoryId, string theme, List<MoviesModel>? movies, UsersModel users)
        {
            MovieCategoryId = movieCategoryId;
            Theme = theme;
            Movies = movies;
            Users = users;
        }
    }
}
