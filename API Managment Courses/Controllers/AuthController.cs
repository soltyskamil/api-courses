using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API_Managment_Courses.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _services;


        public AuthController(IAuthServices services)
        {
            _services = services;

        }


        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> CreateUser([FromBody] UserDto dto)
        {
            try
            {
                var userDto = await _services.CreateUser(dto);
                return Ok(userDto);
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);

            }
        }

        [HttpPost("login")]

        public async Task<ActionResult<LoginResponseDto>> LoginUser(LoginUserDto dto)
        {
            try
            {
                LoginResponseDto resLoginDto = await _services.LoginUser(dto);
               
                Response.Cookies.Append("jwt_token", resLoginDto.Token);    

                return Ok(resLoginDto);
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);

            }
        }


        [Authorize]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            try
            {


                var users = await _services.GetAll();
                return Ok(users);

            }

            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
    }
}