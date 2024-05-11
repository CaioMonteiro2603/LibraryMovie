using LibraryMovie.Models;

namespace LibraryMovie.Repository.Interface
{
    public interface IMoviesRepository
    {
        public Task<IList<MoviesModel>> FindAll();

        public Task<IList<MoviesModel>> FindByTitle(string title);

        public Task<MoviesModel> FindById(int id);

        public Task<MoviesModel> FindByRegistrationDate(DateTime registrationDate);

        public Task<int> Insert(MoviesModel moviesModel);

        public void Update(MoviesModel moviesModel);

        public void Delete(int id);
    }
}
