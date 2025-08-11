using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API_Managment_Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseServices _services;


        public CoursesController(ICourseServices services)
        {
            _services = services;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            try
            {
                var courses = await _services.GetAll();
                if (courses == null) return BadRequest(new { message = "Tablica kursów jest pusta", success = false });


                return Ok(new { data = courses, success = true });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, success = false });
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<CourseDto>> GetSingle(int id)
        {
            try
            {
                var course = await _services.GetSingle(id);
                if (course == null) return BadRequest(new { message = $"Nie znaleziono kursu o id {id}", success = false });
                return Ok(new { data = course, success = true });
            }

            catch (Exception ex)
            {
                return StatusCode(404, new { message = ex.Message, success = false });
            }

        }


        [HttpPost]

        public async Task<ActionResult<Course>> CreateCourse(CreateCourseDto dto)
        {
            try
            {
                var course = await _services.CreateCourse(dto);
                return Ok(new { message = $"{course.Title} has been created", success = true });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, success = false });
            }
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {

                await _services.DeleteCourse(id);
                return Ok(new { message = $"Kurs o id {id} został pomyślnie usunięty", success = true });
            }

            catch (Exception ex)
            {
                return StatusCode(404, new { message = $"Nie znaleziono kursu o id {id}", success = false });
            }
        }

        [HttpPost("{courseId}/assign/{userId}")]

        public async Task<ActionResult> AssignToUser(int courseId, int userId)
        {
            try
            {


                await _services.AssignToUser(courseId, userId);
                return Ok(new { message = $"Użytkownik o id {userId} został pomyślnie zapisany do kursu o id {courseId}", success = true });
            }

            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "User not found":
                    case "Course not found":
                        return NotFound(new { message = ex.Message, success = false });
                    case "Already assigned":
                        return Conflict(new { message = ex.Message, success = false });
                    default:
                        return StatusCode(500, new { message = ex.Message, success = false });
                }


            }
        }


    }
}
