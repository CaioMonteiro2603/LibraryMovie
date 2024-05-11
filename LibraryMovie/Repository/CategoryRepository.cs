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
        public async Task<IList<CategoryModel>> FindAll()
        {
            var findAllCategorys = await _dataContext.Category.AsNoTracking().ToListAsync();

            return findAllCategorys; 
        }

        public async Task<IList<CategoryModel>> FindByTheme(string theme)
        {
            var findByTheme = await _dataContext.Category.AsNoTracking()
                                                   .Where(t => t.Theme.ToLower().Contains(theme.ToLower()))
                                                   .ToListAsync();

            return findByTheme;                                        
        }

        public async Task<CategoryModel> FindById(int id)
        {
            var findById = await _dataContext.Category.AsNoTracking()
                                                      .FirstOrDefaultAsync(i => i.Id == id);

            return findById; 
        }

        public async Task<int> Insert(CategoryModel categoryModel)
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
