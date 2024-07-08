using LibraryMovie.Models;

namespace LibraryMovie.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IList<UsersModel>> FindAll(int pagina, int tamanhoPagina);

        Task<UsersModel> FindByEmail(string email);
        Task<UsersModel> FindById(int id);

        int Count();

        Task<int> Insert(UsersModel usersModel); 

        Task<UsersModel> Update(UsersModel usersModel, int id);

        Task<bool> Delete(int id);
    }
}
