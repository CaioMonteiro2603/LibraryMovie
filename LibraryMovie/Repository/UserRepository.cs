using LibraryMovie.Data;
using LibraryMovie.Models;
using LibraryMovie.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace LibraryMovie.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IList<UsersModel>> FindAll()
        {
            var findAllUsers = await _dataContext.Users.AsNoTracking().ToListAsync();

            return findAllUsers; 
        }

        public async Task<UsersModel> FindByEmail(string email)
        {
            var findUserEmail = await _dataContext.Users.AsNoTracking()
                                                        .FirstOrDefaultAsync(e => e.Email == email);

            return findUserEmail; 
        }

        public async Task<UsersModel> FindById(int id)
        {
            var findUserId = await _dataContext.Users.AsNoTracking()
                                                     .FirstOrDefaultAsync(i => i.Id == id);

            return findUserId; 
        }

        public async Task<int> Insert(UsersModel usersModel)
        {
            _dataContext.Users.Add(usersModel);
            _dataContext.SaveChanges();

            return usersModel.Id;
        }

        public async void Update(UsersModel usersModel)
        {
            _dataContext.Users.Update(usersModel);
            _dataContext.SaveChanges(); 
            
        }

        public async void Delete(int id)
        {
            var deleteUser = new UsersModel()
            {
                Id = id
            }; 

            _dataContext.Users.Remove(deleteUser);
            _dataContext.SaveChanges();
        }
    }
}
