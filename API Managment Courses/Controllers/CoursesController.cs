using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<ActionResult<Course>> GetCourses()
        {
            try
            {
                var courses = await _services.GetAll();
                return Ok(courses);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Course>> GetSingle(int id)
        {
            try
            {
                var course = await _services.GetSingle(id);
                return Ok(course);
            }

            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }

        }

        [HttpPost]

        public async Task<ActionResult<Course>> CreateCourse(CreateCourseDto dto)
        {
            try
            {
                var course = await _services.CreateCourse(dto);
                return Ok(new { message = $"{course.Title} has been created" });
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            try
            {
                await _services.DeleteCourse(id);
                return Ok("Course has been deleted");
            }

            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
        // api/courses/courseId?=4
        [HttpPost("{courseId}/assign/{userId}")]

        public async Task<ActionResult<Course>> AssignToUser(int courseId, int userId)
        {
            try
            {
                await _services.AssignToUser(courseId, userId);
                return Ok(new { message = $"{userId} has been assigned to course {courseId}" });
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }


    }
}
