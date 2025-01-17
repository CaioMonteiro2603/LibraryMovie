﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMovie.Models
{
    [Table("Users")]
    public class UsersModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(40)]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(12)]
        public string Password { get; set; }

        [Required]
        [StringLength(10)]
        public string? Role { get; set; }

        public virtual List<MoviesModel> Movies { get; set; }

        

        public UsersModel()
        {
            
        }

       
    }
}
