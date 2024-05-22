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
            var findAllCategorys = await _dataContext.Category.AsNoTracking().Include(u => u.Users).ToListAsync();
                

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
                                                .Include(u => u.Users)
                                                .FirstOrDefaultAsync(i => i.MovieCategoryId == id);

            return findById; 
        }

        public async Task<int> Insert(CategoryModel categoryModel)
        {
            _dataContext.Category.Add(categoryModel);
            await _dataContext.SaveChangesAsync();

            return categoryModel.MovieCategoryId;
        }

        public async Task<CategoryModel> Update(CategoryModel categoryModel, int id)
        {
            var categoryId = await FindById(id);

            if (categoryId == null) throw new Exception($"The category's id: {id} doesn't exist!"); 

            categoryId.MovieCategoryId = categoryModel.MovieCategoryId;
            categoryId.Theme = categoryModel.Theme;

            _dataContext.Category.Update(categoryId);
            await _dataContext.SaveChangesAsync();

            return categoryId; 
        }

        public async Task<bool> Delete(int id)
        {
            var deleteId = await FindById(id);

            if (deleteId == null) throw new Exception($"The category's id: {id} doesn't exist!");

            _dataContext.Category.Remove(deleteId);
            await _dataContext.SaveChangesAsync();

            return true; 
        }
    }
}
