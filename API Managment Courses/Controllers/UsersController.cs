using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace API_Managment_Courses.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _services;

        public UsersController(IUserServices services)
        {
            _services = services;
        }

        [HttpGet]

        public async Task<ActionResult<UserDto>> GetAll()
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

        [HttpGet("{id}")]

        public async Task<ActionResult<UserDto>> GetSingle(int id)
        {
            try
            {
                var user = await _services.GetSingle(id);
                return Ok(user);

            }

            catch (Exception ex)
            {
                return StatusCode(404, new { message = $"Nie znaleziono uzytkownia o id {id}" });
            }
        }

        [HttpGet("{id}/courses")]

        public async Task<ActionResult<ICollection<CourseEnrollment>>> GetUserCourses(int id)
        {
            try
            {
                var userCourses = _services.GetUserCourses(id);
                return Ok(userCourses);
            }

            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }


        }
    }
}