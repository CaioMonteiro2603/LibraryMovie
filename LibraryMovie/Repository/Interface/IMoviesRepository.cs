using LibraryMovie.Models;

namespace LibraryMovie.Repository.Interface
{
    public interface IMoviesRepository
    {
        public IList<MoviesModel> FindAll();

        public IList<MoviesModel> FindByRegistrationDate(DateTime? registrationReference, int height);

        public IList<MoviesModel> FindByTitle(string title);

        public MoviesModel FindById(int id);

        public int Insert(MoviesModel moviesModel);

        public void Update(MoviesModel moviesModel);

        public void Delete(int id);
    }
}
