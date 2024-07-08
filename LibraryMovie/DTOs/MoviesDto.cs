namespace LibraryMovie.DTOs
{
    public class MoviesDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int RunningTime { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }
    }
}
