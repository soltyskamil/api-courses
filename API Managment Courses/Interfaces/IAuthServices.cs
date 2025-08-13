using API_Managment_Courses.Dtos;

namespace API_Managment_Courses.Interfaces
{
    public interface IAuthServices
    {
        Task<UserDto> CreateUser(UserDto userDto);

        Task<LoginResponseDto> LoginUser(LoginUserDto loginUserDto);

        Task<IEnumerable<UserDto>> GetAll();

        string CreateToken(User user);

    }
}
