using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
                return Ok($"Lekcja została dodana");
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{lessonID}")]

        public async Task<ActionResult> DeleteLesson(int lessonID)
        {
            try
            {
                await _services.DeleteLesson(lessonID);
                return Ok($"Lekcja o id {lessonID} została usunięta");
            }

            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("{lessonID}")]

        public async Task<ActionResult<LessonDto>> GetLessonById(int lessonID)
        {
            try
            {
                var lesson = await _services.GetLessonById(lessonID);
                return Ok(lesson);
            }

            catch (Exception ex)
            {
                return StatusCode(404, new { message = $"Nie znaleziono lekcji o id {lessonID}" });
            }

        }
    }


}

