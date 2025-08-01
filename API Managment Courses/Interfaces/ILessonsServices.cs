using API_Managment_Courses.Dtos;

namespace API_Managment_Courses.Interfaces
{
    public interface ILessonsServices
    {
        Task CreateLesson(CreateLessonDto dto);
        
        Task DeleteLesson(int id);

        Task<LessonDto> GetLessonById(int id);


    }
}
