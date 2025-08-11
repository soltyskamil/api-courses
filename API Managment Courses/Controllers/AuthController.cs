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
        private readonly IConfiguration _config;

        public AuthController(IAuthServices services, IConfiguration config)
        {
            _services = services;
            _config = config;
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

        public async Task<ActionResult> LoginUser(LoginUserDto dto)
        {
            try
            {
                await _services.LoginUser(dto);

                var token = _services.CreateJwtToken(dto.Email, _config);

                Response.Cookies.Append("jwt-token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });

                return Ok(new { token });
            }

            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);

            }
        }

        [HttpPost("logout")]

        public async Task<ActionResult> Logout()
        {
            try
            {
                Response.Cookies.Delete("jwt-token");

                return Ok(new { message = "Pomyślnie wylogowano" });
            }

            catch (Exception ex)
            {
                return StatusCode(400, new { message = "Błąd serwera" });

            }
        }


        [HttpGet]
        [Authorize]

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