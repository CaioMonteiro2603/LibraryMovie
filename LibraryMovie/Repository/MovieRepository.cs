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
        public async Task<IList<MoviesModel>> FindAll()
        {
            var findAllMovies = await _dataContext.Movies.AsNoTracking()
                                                         .ToListAsync();

            return findAllMovies;
        }

        public async Task<IList<MoviesModel>> FindByTitle(string title)
        {
            var findByTitle = await _dataContext.Movies.AsNoTracking()
                                                       .Include(c => c.Category)
                                                       .Where(t => t.Title.ToLower().Contains(title.ToLower()))
                                                       .ToListAsync();

            return findByTitle == null ? new List<MoviesModel>() : findByTitle;
        }

        public async Task<MoviesModel> FindById(int id)
        {
            var findMovieId = await _dataContext.Movies.AsNoTracking()
                                                       .FirstOrDefaultAsync(i => i.Id == id);

            return findMovieId; 
        }

        public async Task<MoviesModel> FindByRegistrationDate(DateTime registrationDate)
        {
            var findByRegistrationdate = await _dataContext.Movies.AsNoTracking()
                                                            .Include(c => c.Category) // inner join
                                                            .FirstOrDefaultAsync(r => r.RegistrationDate == registrationDate);

            return findByRegistrationdate; 
        }

        public async Task<int> Insert(MoviesModel moviesModel)
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
