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

        [HttpGet]
        [Authorize(Roles = "admin, operator")]
        public async Task<ActionResult<IList<UserResponseVM>>> FindAllAsync()
        {
            var findAllUsers = await _userRepository.FindAll();

            if(findAllUsers != null && findAllUsers.Count > 0)
            {
                var response = _mapper.Map<List<UserResponseVM>>(findAllUsers);
                return Ok(response);
            } else
            {
                return NoContent();
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin, operator, user")]
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

        [HttpPost]
        [Authorize(Roles = "admin, operator")]
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

        [HttpPut("{id:int}")]
        [Authorize(Roles = "admin, operator")]
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

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
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

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
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
