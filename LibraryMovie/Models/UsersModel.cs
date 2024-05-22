using System.ComponentModel.DataAnnotations;

namespace LibraryMovie.Models
{
    public class UsersModel
    {  
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string? Role { get; set; }

        public virtual ICollection<MoviesModel>? Movies {  get; set; }

        public int? CategoryId { get; set; }
        public CategoryModel? Category { get; set; }
        public UsersModel()
        {
            
        }

        public UsersModel(int id, string name, string email, 
            string password, string? role, List<MoviesModel>? movies, int? categoryId, CategoryModel? category)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            Movies = movies;
            CategoryId = categoryId;
            Category = category;
        }
    }
}
