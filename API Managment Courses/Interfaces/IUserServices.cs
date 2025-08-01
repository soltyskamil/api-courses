using API_Managment_Courses.Dtos;


namespace API_Managment_Courses.Interfaces
{
    public interface IUserServices
    {
        Task<UserDto> CreateUser(UserDto dto);
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetSingle(int id);

        Task<IEnumerable<CourseDto>> GetUserCourses(int id);

        Task DeleteUser(int id);

    }
}
