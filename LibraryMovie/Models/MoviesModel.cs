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

        public int UserId { get; set; }

        public UsersModel User { get; set; }

        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }
        
        public MoviesModel()
        {
            
        }
    }
} 
