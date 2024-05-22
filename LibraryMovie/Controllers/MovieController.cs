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
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0", Deprecated = true)]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMapper _mapper; 

        public MovieController(IMoviesRepository moviesRepository, IMapper mapper)
        {
            _moviesRepository = moviesRepository;  
            _mapper = mapper;
        }

        /// <summary>
        /// Find all movies in a list
        /// </summary>
        /// <returns>All movies in the database</returns>
        /// <response code="400">Validation error</response>
        /// <response code="404">Movie not found</response>
        /// <response code="200">Sucess</response>
        [HttpGet]
        [ApiVersion("3.0")]
        [Authorize(Roles = "admin, operator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<MoviesDto>>> FindAll()
        {
            var findAllMovies = await _moviesRepository.FindAll();

            if(findAllMovies.Count == 0)
            {
                return BadRequest(); 
            }

            if (findAllMovies == null)
            {
                return NotFound();
               
            }

            var response = _mapper.Map<List<MoviesDto>>(findAllMovies);
            return Ok(response);
        }

        /// <summary>
        /// Find a movie by your registration date
        /// </summary>
        /// <param name="registrationDate">Identification of the movie</param>
        /// <param name="height">Page's height</param>
        /// <returns>The movie's registration date</returns>
        /// <response code="404">Movie not found</response>
        /// <response code="400">Validation error</response>
        /// <response code="200">Sucess</response>
        [HttpGet]
        [ApiVersion("2.0", Deprecated = true)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<dynamic>>> FindByRegistrationDate([FromQuery]string registrationDate, [FromQuery]int height = 5) 
        {
            var date = (string.IsNullOrEmpty(registrationDate)) ? DateTime.UtcNow.AddYears(-20) :
                DateTime.ParseExact(registrationDate, "yyyy-MM-ddTHH:mm:ss:fffffff", null, System.Globalization.DateTimeStyles.RoundtripKind);

            var findBydate = await _moviesRepository.FindByRegistrationDate(date, height);
            var newReferenceDate = findBydate.LastOrDefault().RegistrationDate.ToString("yyyy-MM-ddTHH:mm:ss:fffffff");

            var linkPage = $"api/movie?registrationDate={newReferenceDate}&height={height}";

            if(findBydate == null)
            {
                return NotFound();
            }

            if(findBydate.Count == 0)
            {
                return BadRequest();
            }

            var findNew = new
            {
                findBydate,
                linkPage
            };

            
            return Ok(findNew);
        }

        /// <summary>
        /// Find a movie by your title
        /// </summary>
        /// <param name="title">Identification of the movie</param>
        /// <returns>The movie's title</returns>
        /// <response code="404">Movie not found</response>
        /// <response code="400">Validation error</response>
        /// <response code="200">Sucess</response>
        [HttpGet]
        [ApiVersion("1.0", Deprecated = true)]
        public async Task<ActionResult<IList<MoviesDto>>> FindByTitle(string title)
        {
            var findByTitle = await _moviesRepository.FindByTitle(title);

            if (findByTitle == null)
            {
                return NotFound();
            }
            if(findByTitle.Count == 0)
            {
                return BadRequest();
            }

            var response = _mapper.Map<List<MoviesDto>>(findByTitle);
            return Ok(response); 
        }

        /// <summary>
        /// Find a movie by your ID
        /// </summary>
        /// <param name="id">Identification of the function</param>
        /// <returns>The movie's id</returns>
        /// <response code="400">Validation error</response>
        /// <response code="404">Object not found in the database</response>
        /// <response code="200">Sucess</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin, operator, user")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MoviesDto>> FindById([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest(); 

            }
            else
            {
                var findById = await _moviesRepository.FindById(id);

                if(findById == null)
                {
                    return NotFound();
                } else
                {
                    var response = _mapper.Map<MoviesDto>(findById);
                    return Ok(response); 
                }
            }
        }

        /// <summary>
        /// Movie's creation
        /// </summary>
        /// <remarks>
        /// {}
        /// </remarks>
        /// <param name="moviesModel">Identification of the function</param>
        /// <returns>The movie's creation response</returns>
        /// <response code="400">Validation Error</response>
        /// <response code="201">Created</response>
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<MoviesDto>> Post([FromBody] MoviesModel moviesModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }else
                {

                    await _moviesRepository.Insert(moviesModel);

                    var response = _mapper.Map<MoviesDto>(moviesModel); 

                    var url = Request.GetEncodedUrl().EndsWith("/") ?
                                                    Request.GetEncodedUrl() :
                                                    Request.GetEncodedUrl() + "/";

                    url += moviesModel.Id;
                    return Created(url, response);
                }
               
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        /// <summary>
        /// Movie's edition
        /// </summary>
        /// <remarks>
        /// {}
        /// </remarks>
        /// <param name="id">Identification of the function by route</param>
        /// <param name="moviesModel">Identification of the function by body</param>
        /// <returns>The movie's edition response</returns>
        /// <response code="400">Validation Error</response>
        /// <response code="404">Object not found in the database</response>
        /// <response code="204">No content</response>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin, operator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put([FromBody] MoviesModel moviesModel, [FromRoute] int id)
        {
            if((id != moviesModel.Id) || (!ModelState.IsValid))
            {
                return BadRequest(); 
            } else
            {
                var ismovie = await _moviesRepository.FindById(id);

                if(ismovie == null)
                {
                    return NotFound();
                } else
                {
                    await _moviesRepository.Update(moviesModel, id);
                    return NoContent(); 
                }
            }
        }

        /// <summary>
        /// Category's remove
        /// </summary>
        /// <param name="id">Identification of the function by route</param>
        /// <returns>The movie's exclusion response</returns>
        /// <response code="400">Validation error</response>
        /// <response code="200">Ok</response>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            bool deleted = await _moviesRepository.Delete(id);
            return NoContent();
        }
    }
}
