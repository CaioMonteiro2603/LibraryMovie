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

        public virtual ICollection<CategoryModel>? Categories { get; set; }
        public UsersModel()
        {
            
        }

        public UsersModel(int id, string name, string email, string password, string? role, ICollection<MoviesModel>? movies, ICollection<CategoryModel>? categories)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            Movies = movies;
            Categories = categories;
        }
    }
}
