using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using API_Managment_Courses.Models.Api;
using API_Managment_Courses.Filters;
namespace API_Managment_Courses.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonsServices _services;

        public LessonsController(ILessonsServices services)
        {
            _services = services;
        }

        [HttpPost]

        public async Task<ActionResult> CreateLesson([FromBody] CreateLessonDto dto)
        {

            try
            {
                await _services.CreateLesson(dto);
                return Ok(new ApiResponse<object>
                {
                    data = dto,
                    success = true,
                    message = $"Lekcja została pomyślnie dodana do kursu {dto.CourseID}",
                    errors = null
                });

            }
            catch(Exception ex)
            {
                return StatusCode(404, new ApiResponse<object>
                {
                    data = dto,
                    success = false,
                    message = ex.Message,
                    errors = null
                });
            }
        }

        [HttpDelete("{lessonID}")]

        public async Task<ActionResult> DeleteLesson(int lessonID)
        {
            try
            {
                await _services.DeleteLesson(lessonID);
                return Ok(new {message = $"Lekcja {lessonID} została pomyślnie usunięta", success = true });
            }

            catch (Exception ex)
            {
                return StatusCode(404, new {message = ex.Message, success = false});
            }
        }

        [HttpGet("{lessonID}")]

        public async Task<ActionResult<LessonDto>> GetLessonById(int lessonID)
        {
            try
            {
                var lesson = await _services.GetLessonById(lessonID);
                return Ok(new {message = $"{lessonID} została pomyślnie dodana", success = true});
            }

            catch (Exception ex)
            {
                return StatusCode(404, new { message = ex.Message, success = false });
            }

        }
    }


}

