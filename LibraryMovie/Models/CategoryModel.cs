using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMovie.Models
{
    [Table("MovieCategory")]
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Theme {  get; set; }

        public int? UserId { get; set; }
        public virtual UsersModel? User { get; set; }
        public CategoryModel()
        {
            
        }

        public CategoryModel(int id, string theme, UsersModel user)
        {
            Id = id;
            Theme = theme;
            User = user;
        }
    }
}
