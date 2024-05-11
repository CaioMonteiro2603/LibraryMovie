using LibraryMovie.Models;

namespace LibraryMovie.Repository.Interface
{
    public interface IUserRepository
    {
        public Task<IList<UsersModel>> FindAll();

        public Task<UsersModel> FindByEmail(string email);
        public Task<UsersModel> FindById(int id);

        public Task<int> Insert(UsersModel usersModel); 

        public void Update(UsersModel usersModel);

        public void Delete(int id);
    }
}
