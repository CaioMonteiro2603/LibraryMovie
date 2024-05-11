namespace LibraryMovie.ViewModel
{
    public class LoginResponseVM
    {
        public string UserEmail { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

        public string? Role { get; set; }

        public string Token { get; set; }
    }
}
