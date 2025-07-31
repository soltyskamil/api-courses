using API_Managment_Courses.Dtos;

namespace API_Managment_Courses.Interfaces
{
    public interface ICourseServices
    {
         Task<List<Course>> GetAll();

        Task<Course> GetSingle(int id);

        Task<Course> CreateCourse(CreateCourseDto dto);

        Task DeleteCourse(int courseId);

        Task AssignToUser(int courseId, int userId);



    }
}
