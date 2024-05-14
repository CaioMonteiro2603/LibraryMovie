using LibraryMovie.Models;
using LibraryMovie.Repository.Interface;
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
    public class MovieController : ControllerBase
    {
        private readonly IMoviesRepository _moviesRepository;

        public MovieController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;  
        }

        [HttpGet]
        [ApiVersion("3.0")]
        public ActionResult<IList<dynamic>> FindByRegistrationDate([FromQuery]string registrationDate, [FromQuery]int height = 5) 
        {
            var date = (string.IsNullOrEmpty(registrationDate)) ? DateTime.UtcNow.AddYears(-20) :
                DateTime.ParseExact(registrationDate, "yyyy-MM-ddTHH:mm:ss:fffffff", null, System.Globalization.DateTimeStyles.RoundtripKind);

            var findBydate = _moviesRepository.FindByRegistrationDate(date, height);
            var newReferenceDate = findBydate.LastOrDefault().RegistrationDate.ToString("yyyy-MM-ddTHH:mm:ss:fffffff");

            var linkPage = $"api/movie?registrationDate={newReferenceDate}&height={height}";

            if(findBydate == null || findBydate.Count == 0)
            {
                return NoContent();
            }

            var findNew = new
            {
                findBydate,
                linkPage
            };

            return Ok(findNew);
        }

        [HttpGet]
        [ApiVersion("2.0", Deprecated = true)]
        public ActionResult<IList<MoviesModel>> FindAll()
        {
            var findAllMovies = _moviesRepository.FindAll();

            if (findAllMovies != null && findAllMovies.Count > 0)
            {
                return Ok(findAllMovies);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [ApiVersion("1.0", Deprecated = true)]
        public ActionResult<IList<MoviesModel>> FindByTitle(string title)
        {
            var findByTitle = _moviesRepository.FindByTitle(title);

            if (findByTitle == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(findByTitle);
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<MoviesModel> FindById([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest(); 

            }
            else
            {
                var findById = _moviesRepository.FindById(id);

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
        public ActionResult<MoviesModel> Post([FromBody] MoviesModel moviesModel)
        {
            try
            {
                _moviesRepository.Insert(moviesModel);
                var url = Request.GetEncodedUrl().EndsWith("/") ?
                                                Request.GetEncodedUrl() :
                                                Request.GetEncodedUrl() + "/";

                url += moviesModel.Id; 
                return Created(url, moviesModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<MoviesModel> Put([FromBody] MoviesModel moviesModel, [FromRoute] int id)
        {
            if((id == moviesModel.Id) || (!ModelState.IsValid))
            {
                return BadRequest(); 
            } else
            {
                var ismovie = _moviesRepository.FindById(id);

                if(ismovie == null)
                {
                    return NotFound();
                } else
                {
                    _moviesRepository.Update(moviesModel);
                    return NoContent(); 
                }
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<MoviesModel> Delete([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }else
            {
                var findMovieId = FindById(id);

                if(findMovieId == null)
                {
                    return NotFound();
                } else
                {
                    _moviesRepository.Delete(id);
                    return NoContent();
                }
            }
        }
    }
}
