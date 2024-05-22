using System.ComponentModel.DataAnnotations;
namespace LibraryMovie.Models
{
    public class CategoryModel
    {
        public int MovieCategoryId { get; set; }

        [Required]
        public string Theme { get; set; }
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
