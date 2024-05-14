using LibraryMovie.Models;

namespace LibraryMovie.Repository.Interface
{
    public interface ICategoryRepository
    {
        public IList<CategoryModel> FindAll();

        public IList<CategoryModel> FindByTheme(string theme);

        public CategoryModel FindById(int id);

        public int Insert(CategoryModel categoryModel);

        public void Update(CategoryModel categoryModel);

        public void Delete(int id);
    }
}
