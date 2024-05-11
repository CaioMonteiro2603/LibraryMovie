using System.ComponentModel.DataAnnotations;

namespace LibraryMovie.ViewModel
{
    public class LoginRequestVM
    {
        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}
