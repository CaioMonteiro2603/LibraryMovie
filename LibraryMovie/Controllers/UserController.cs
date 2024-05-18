using AutoMapper;
using LibraryMovie.Models;
using LibraryMovie.Repository.Interface;
using LibraryMovie.Services;
using LibraryMovie.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Find all users in a list
        /// </summary>
        /// <returns>All users in the database</returns>
        /// <response code="400">Validation Error</response>
        /// <response code="404">Category not found in the database</response>
        /// <response code="200">Sucess</response>
        [HttpGet]
        [Authorize(Roles = "admin, operator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<UserResponseVM>>> FindAllAsync()
        {
            var findAllUsers = await _userRepository.FindAll();

            if(findAllUsers.Count == 0)
            {
                return BadRequest(); 
            }

            if(findAllUsers == null)
            {
                return NotFound();
            }
            
            var response = _mapper.Map<List<UserResponseVM>>(findAllUsers);
            return Ok(response);
            
        }

        /// <summary>
        /// Find an user by your ID
        /// </summary>
        /// <param name="id">Identification of the function</param>
        /// <returns>The user's id</returns>
        /// <response code="400">Validation error</response>
        /// <response code="200">Sucess</response>
        /// <response code="404">Object not found in the database</response>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin, operator, user")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserResponseVM>> FindByIdAsync(int id)
        {
            if(id == 0)
            {
                return BadRequest(); 
            } else
            {
                var findById = await _userRepository.FindById(id);

                if (findById != null)
                {
                    var response = _mapper.Map<UserResponseVM>(findById);
                    return Ok(response);
                }
                else
                {
                    return NotFound();
                }
            }    
        }

        /// <summary>
        /// User's creation
        /// </summary>
        /// <remarks>
        /// {}
        /// </remarks>
        /// <param name="userModel">Identification if the object</param>
        /// <returns>The user's creation response</returns>
        /// <response code="400">Validation Error</response>
        /// <response code="201">Created</response>
        [HttpPost]
        [Authorize(Roles = "admin, operator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<UsersModel>> Post([FromBody] UsersModel userModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userRepository.Insert(userModel);

            var url = Request.GetEncodedUrl().EndsWith("/") ?
                        Request.GetEncodedUrl() :
                        Request.GetEncodedUrl() + "/";

            url += userModel.Id; 

            return Created(url, userModel);
        }

        /// <summary>
        /// User's edition
        /// </summary>
        /// <remarks>
        /// {}
        /// </remarks>
        /// <param name="id">Identification of the function by route</param>
        /// <param name="userModel">Identification of the function by body</param>
        /// <returns>The category's edition response</returns>
        /// <response code="400">Validation Error</response>
        /// <response code="404">Object not found in the database</response>
        /// <response code="204">No content</response>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin, operator")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put([FromRoute] int id, [FromBody] UsersModel userModel)
        {
            if((!ModelState.IsValid) || (id != userModel.Id))
            {
                return BadRequest();
            } else
            {
                var isUser = _userRepository.FindById(id); 
                if(isUser == null)
                {
                    return NotFound(); 
                } else
                {
                    _userRepository.Update(userModel, id);
                    return NoContent();
                }
               
            }
        }

        /// <summary>
        /// User's remove
        /// </summary>
        /// <param name="id">Identification of the function by route</param>
        /// <returns>The category's exclusion response</returns>
        /// <response code="400">Validation error</response>
        /// <response code="200">Ok</response>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UsersModel>> Delete([FromRoute] int id)
        {
            if(id == 0)
            {
                return BadRequest();
            } else
            {
                bool Deleted = await _userRepository.Delete(id);
                return Ok(); 
            }     
        }

        /// <summary>
        /// User's login
        /// </summary>
        /// <param name="loginRequestVM">return only necessary information about user</param>
        /// <returns>User's acess</returns>
        /// <response code="404">User's email not found</response>
        /// <response code="200">Ok</response>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<LoginResponseVM>> Login([FromBody] LoginRequestVM loginRequestVM)
        {
            var Login = await _userRepository.FindByEmail(loginRequestVM.UserEmail);

            if(Login == null)
            {
                return NotFound(); 
            } else
            {
                // token to acess API
                var token = AuthenticationService.GetToken(Login);

                var loginResponseVM = _mapper.Map<LoginResponseVM>(Login);

                loginResponseVM.Token = token;

                return Ok(loginResponseVM); 
            }
        }
    }
}
