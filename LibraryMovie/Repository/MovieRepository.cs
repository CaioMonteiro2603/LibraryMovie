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
                                                         .Include(x => x.Category)
                                                         .Include(x=> x.Users)
                                                         .ToListAsync();

            return findAllMovies;
        }

        public async Task<IList<MoviesModel>> FindByRegistrationDate(DateTime? registrationReference, int height)
        {
            var findByDate =  await _dataContext.Movies.Where(r => r.RegistrationDate > registrationReference)
                                                 .OrderBy(r => r.RegistrationDate)
                                                 .Include(x => x.Category)
                                                 .Include(x=> x.Users)
                                                 .Take(height)
                                                 .AsNoTracking()
                                                 .ToListAsync();

            return findByDate; 
        }
        public async Task<IList<MoviesModel>> FindByTitle(string title)
        {
            var findByTitle = await _dataContext.Movies.Where(t => t.Title.ToLower().Contains(title.ToLower()))
                                                 .Include(x => x.Category)
                                                 .Include(x => x.Users)
                                                 .AsNoTracking()
                                                 .ToListAsync();

            return findByTitle == null ? new List<MoviesModel>() : findByTitle;
        }

        public async Task<MoviesModel> FindById(int id)
        {
            var findMovieId = await _dataContext.Movies.AsNoTracking()
                                                 .Include(x => x.Category)
                                                 .Include(x => x.Users)
                                                 .FirstOrDefaultAsync(i => i.Id == id);

            return findMovieId; 
        }

        public async Task<int> Insert(MoviesModel moviesModel)
        {
            _dataContext.Movies.Add(moviesModel);
            await _dataContext.SaveChangesAsync();

            return moviesModel.Id; 
        }

        public async Task<MoviesModel> Update(MoviesModel moviesModel, int id)
        {
            var movieId = await FindById(id);

            if (movieId == null) throw new Exception($"The movie's id: {id} doesn't exist!");

            movieId.Id = moviesModel.Id;
            movieId.Title = moviesModel.Title;
            movieId.RegistrationDate = moviesModel.RegistrationDate;
            movieId.RunningTime = moviesModel.RunningTime;
            movieId.UserId = moviesModel.UserId;
            movieId.CategoryId = moviesModel.CategoryId;

            _dataContext.Movies.Update(movieId);
            await _dataContext.SaveChangesAsync();

            return movieId; 
        }

        public async Task<bool> Delete(int id)
        {
            var deleteId = await FindById(id);

            if (deleteId == null) throw new Exception($"The movie's id: {id} doesn't exist!");

            _dataContext.Movies.Remove(deleteId);
            await _dataContext.SaveChangesAsync();

            return true; 
        }
    }
}
