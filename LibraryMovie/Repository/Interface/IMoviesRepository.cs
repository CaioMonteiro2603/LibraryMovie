using LibraryMovie.Models;

namespace LibraryMovie.Repository.Interface
{
    public interface IMoviesRepository
    {
        public Task<IList<MoviesModel>> FindAll();

        public Task<IList<MoviesModel>> FindByRegistrationDate(DateTime? registrationReference, int height);

        public Task<IList<MoviesModel>> FindByTitle(string title);

        public Task<MoviesModel> FindById(int id);

        public Task<int> Insert(MoviesModel moviesModel);

        public Task<MoviesModel> Update(MoviesModel moviesModel, int id);

        public Task<bool> Delete(int id);
    }
}
