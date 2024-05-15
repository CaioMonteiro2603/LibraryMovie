﻿using LibraryMovie.Models;
using LibraryMovie.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Authorize(Roles = "admin, operator")]
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
        [Authorize(Roles = "admin, operator")]
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
        [Authorize(Roles = "admin, operator")]
        public ActionResult<CategoryModel> Post([FromBody] CategoryModel categoryModel)
        {
            _categoryRepository.Insert(categoryModel);
            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin, operator")]
        public ActionResult Put([FromRoute] int id, [FromBody] CategoryModel categoryModel)
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
        [Authorize(Roles = "admin")]
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
