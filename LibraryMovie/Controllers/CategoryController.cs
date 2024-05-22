using AutoMapper;
using LibraryMovie.DTOs;
using LibraryMovie.Models;
using LibraryMovie.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;   
        }

        /// <summary>
        /// Find all categorys in a list
        /// </summary>
        /// <returns>All categorys in the database</returns>
        /// <response code="400">Validation Error</response>
        /// <response code="404">Category not found in the database</response>
        /// <response code="200">Sucess</response>
        [HttpGet]
        [Authorize(Roles = "admin, operator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<CategoryDto>>> FindAllAsync()
        {
            var findAllCategorys = await _categoryRepository.FindAll();

            if(findAllCategorys.Count == 0)
            {
                return BadRequest();
            } 
            if (findAllCategorys == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<List<CategoryDto>>(findAllCategorys); 
            return Ok(response);   
        }

        /// <summary>
        /// Find a category by your ID
        /// </summary>
        /// <param name="id">Identification of the function</param>
        /// <returns>The category's id</returns>
        /// <response code="400">Validation error</response>
        /// <response code="404">Object not found in the database</response>
        /// <response code="200">Sucess</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin, operator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryDto>> FindById([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest(); 

            } else
            {
                var findById = await _categoryRepository.FindById(id);

                if(findById == null)
                {
                    return NotFound();
                } else
                {
                    var response = _mapper.Map<CategoryDto>(findById);
                    return Ok(response);
                }
            }
        }

        /// <summary>
        /// Category's creation
        /// </summary>
        /// <remarks>
        /// {}
        /// </remarks>
        /// <param name="categoryModel">Identification if the object</param>
        /// <returns>The category's creation response</returns>
        /// <response code="400">Validation Error</response>
        /// <response code="201">Created</response>
        [HttpPost]
        [Authorize(Roles = "admin, operator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CategoryDto>> Post([FromBody] CategoryModel categoryModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _categoryRepository.Insert(categoryModel);

            var response = _mapper.Map<CategoryDto>(categoryModel);

            var url = Request.GetEncodedUrl().EndsWith("/") ?
                        Request.GetEncodedUrl() :
                        Request.GetEncodedUrl() + "/";

            url += categoryModel.MovieCategoryId;

            return Created(url, response); 
        }

        /// <summary>
        /// Category's edition
        /// </summary>
        /// <remarks>
        /// {}
        /// </remarks>
        /// <param name="id">Identification of the function by route</param>
        /// <param name="categoryModel">Identification of the function by body</param>
        /// <returns>The category's edition response</returns>
        /// <response code="400">Validation Error</response>
        /// <response code="404">Object not found in the database</response>
        /// <response code="204">No content</response>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin, operator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] CategoryModel categoryModel)
        {
            if((id != categoryModel.MovieCategoryId) || (!ModelState.IsValid))
            {
                return BadRequest();
            } 
            else
            {
                var isCategory = await _categoryRepository.FindById(id);

                if(isCategory == null)
                {
                    return NotFound();

                } else
                {
                    await _categoryRepository.Update(categoryModel, id);
                    return NoContent();
                }
            }
        }

        /// <summary>
        /// Category's remove
        /// </summary>
        /// <param name="id">Identification of the function by route</param>
        /// <returns>The category's exclusion response</returns>
        /// <response code="400">Validation error</response>
        /// <response code="200">Ok</response>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryModel>> Delete([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest();
            } 
            else
            {
                bool delete = await _categoryRepository.Delete(id);
                return Ok(); 
            }
        }
    }
}
