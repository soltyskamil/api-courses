using API_Managment_Courses.Dtos;


namespace API_Managment_Courses.Interfaces
{
    public interface IUserServices
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetSingle(int id);

        Task<ICollection<CourseEnrollment>> GetUserCourses(int id);




    }
}
