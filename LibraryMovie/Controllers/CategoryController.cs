using LibraryMovie.Models;
using LibraryMovie.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult<IList<CategoryModel>> FindAll()
        {
            var findAllCategorys = _categoryRepository.FindAll();

            if (findAllCategorys != null && findAllCategorys.Count > 0)
            {
                return Ok(findAllCategorys);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<CategoryModel> FindById([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest(); 
            } else
            {
                var findById = _categoryRepository.FindById(id);

                if(findById == null)
                {
                    return NotFound();
                } else
                {
                    return Ok(findById);
                }
            }
        }

        [HttpPost]
        public ActionResult<CategoryModel> Post([FromBody] CategoryModel categoryModel)
        {
            _categoryRepository.Insert(categoryModel);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoryModel> Put([FromRoute] int id, [FromBody] CategoryModel categoryModel)
        {
            if(id != categoryModel.Id)
            {
                return BadRequest();
            } 
            else
            {
                var isCategory = _categoryRepository.FindById(id);

                if(isCategory == null)
                {
                    return NotFound();
                } else
                {
                    _categoryRepository.Update(categoryModel);
                    return NoContent();
                }
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoryModel> Delete([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest();
            } 
            else
            {
                var findId = _categoryRepository.FindById(id);

                if(findId == null)
                {
                    return NotFound();
                } else
                {
                    _categoryRepository.Delete(id);
                    return NoContent();
                }
            }
        }
    }
}
