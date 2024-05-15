using LibraryMovie.Models;

namespace LibraryMovie.Repository.Interface
{
    public interface IUserRepository
    {
        public Task<IList<UsersModel>> FindAll();

        public Task<UsersModel> FindByEmail(string email);
        public Task<UsersModel> FindById(int id);

        public Task<int> Insert(UsersModel usersModel); 

        public Task<UsersModel> Update(UsersModel usersModel, int id);

        public Task<bool> Delete(int id);
    }
}
