using LibraryMovie.Data;
using LibraryMovie.Models;
using LibraryMovie.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryMovie.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext; 
        }
        public IList<CategoryModel> FindAll()
        {
            var findAllCategorys = _dataContext.Category.AsNoTracking().Include(u => u.User).ToList();
                

            return findAllCategorys; 
        }

        public IList<CategoryModel> FindByTheme(string theme)
        {
            var findByTheme = _dataContext.Category.AsNoTracking()
                                                   .Where(t => t.Theme.ToLower().Contains(theme.ToLower()))
                                                   .ToList();

            return findByTheme;                                        
        }

        public CategoryModel FindById(int id)
        {
            var findById = _dataContext.Category.AsNoTracking()
                                                .Include(u => u.User)
                                                .FirstOrDefault(i => i.Id == id);

            return findById; 
        }

        public int Insert(CategoryModel categoryModel)
        {
            _dataContext.Category.Add(categoryModel);
            _dataContext.SaveChanges();

            return categoryModel.Id;
        }

        public void Update(CategoryModel categoryModel)
        {
            _dataContext.Category.Update(categoryModel);
            _dataContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var deleteCategory = new CategoryModel()
            {
                Id = id
            };

            _dataContext.Category.Remove(deleteCategory);
            _dataContext.SaveChanges();
        }
    }
}
