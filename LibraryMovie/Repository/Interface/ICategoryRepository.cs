using LibraryMovie.Models;

namespace LibraryMovie.Repository.Interface
{
    public interface ICategoryRepository
    {
        public Task<IList<CategoryModel>> FindAll();

        public Task<IList<CategoryModel>> FindByTheme(string theme);

        public Task<CategoryModel> FindById(int id);

        public Task<int> Insert(CategoryModel categoryModel);

        public void Update(CategoryModel categoryModel);

        public void Delete(int id);
    }
}
