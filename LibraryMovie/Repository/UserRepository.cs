using LibraryMovie.Data;
using LibraryMovie.Models;
using LibraryMovie.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IList<UsersModel>> FindAll(int pagina, int tamanhoPagina)
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

        public int Count()
        {
            return _dataContext.Users.Count();
        }

        public async Task<int> Insert(UsersModel usersModel)
        {
            _dataContext.Users.Add(usersModel);
            await _dataContext.SaveChangesAsync();

            return usersModel.Id;
        }

        public async Task<UsersModel> Update(UsersModel usersModel, int id)
        {
            UsersModel userId = await FindById(id);

            if(userId == null )
            {
                throw new Exception($"The user's id: {id} doesn't exist!");
            }

            userId.Name = usersModel.Name;
            userId.Email = usersModel.Email;
            userId.Role = usersModel.Role;
            userId.Password = usersModel.Password;
            
            _dataContext.Users.Update(userId);
            await _dataContext.SaveChangesAsync();

            return userId; 
        }

        public async Task<bool> Delete(int id)
        {
            var delete = await FindById(id);

            if (delete == null)
            {
                throw new Exception($"The user's id: {id} doesn't exist!"); 
            }

            _dataContext.Users.Remove(delete);
            await _dataContext.SaveChangesAsync(); 

            return true; 
        }
    }
}
