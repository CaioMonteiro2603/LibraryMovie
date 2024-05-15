using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMovie.Models
{
    [Table("Movies")]
    public class MoviesModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; }

        [Required]
        public DateTime RegistrationDate {  get; set; }

        [Required]
        public int RunningTime {  get; set; }

        public int? CategoryId { get; set; }
        public virtual CategoryModel? Category { get; set; }

        public int? UserId { get; set; }
        public virtual UsersModel? Users { get; set; }

        public MoviesModel()
        {
            
        }

        public MoviesModel(int id, string title, DateTime registrationDate, int runningTime,
            int categoryId, CategoryModel category, int userId, UsersModel users)
        {
            Id = id;
            Title = title;
            RegistrationDate = registrationDate;
            RunningTime = runningTime;
            CategoryId = categoryId;
            Category = category;
            UserId = userId;
            Users = users;
        }
    }
} 
