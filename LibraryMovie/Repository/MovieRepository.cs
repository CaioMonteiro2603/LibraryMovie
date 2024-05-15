using LibraryMovie.Data;
using LibraryMovie.Models;
using LibraryMovie.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryMovie.Repository
{
    public class MovieRepository : IMoviesRepository
    {
        private readonly DataContext _dataContext; 
        public MovieRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IList<MoviesModel> FindAll()
        {
            var findAllMovies = _dataContext.Movies.AsNoTracking()
                                                         .Include(x => x.Category)
                                                         .Include(x=> x.Users)
                                                         .ToList();

            return findAllMovies;
        }

        public IList<MoviesModel> FindByRegistrationDate(DateTime? registrationReference, int height)
        {
            var findByDate =  _dataContext.Movies.Where(r => r.RegistrationDate > registrationReference)
                                                 .OrderBy(r => r.RegistrationDate)
                                                 .Take(height)
                                                 .AsNoTracking()
                                                 .ToList();

            return findByDate; 
        }
        public IList<MoviesModel> FindByTitle(string title)
        {
            var findByTitle = _dataContext.Movies.Where(t => t.Title.ToLower().Contains(title.ToLower()))
                                                 .Include(x => x.Category)
                                                 .Include(x => x.Users)
                                                 .AsNoTracking()
                                                 .ToList();

            return findByTitle == null ? new List<MoviesModel>() : findByTitle;
        }

        public MoviesModel FindById(int id)
        {
            var findMovieId = _dataContext.Movies.AsNoTracking()
                                                 .Include(x => x.Category)
                                                 .Include(x => x.Users)
                                                 .FirstOrDefault(i => i.Id == id);

            return findMovieId; 
        }

        public int Insert(MoviesModel moviesModel)
        {
            _dataContext.Movies.Add(moviesModel);
            _dataContext.SaveChanges();

            return moviesModel.Id; 
        }

        public void Update(MoviesModel moviesModel)
        {
            _dataContext.Movies.Update(moviesModel);
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var deleteMovie = new MoviesModel()
            {
                Id = id
            };

            _dataContext.Movies.Remove(deleteMovie);
            _dataContext.SaveChanges();
        }
    }
}
