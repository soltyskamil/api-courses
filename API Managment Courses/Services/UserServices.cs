using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using AutoMapper;

namespace API_Managment_Courses.Services
{
    public class UserServices : IUserServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public UserServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUser(UserDto dto)
        {
            User newUser = new User
            {
                Email = dto.Email
            };
            _context.Users.Add(newUser);

            UserProfile newUserProfile = new UserProfile
            {
                Name = dto.Name,
                Surname = dto.Surname,
                DateOfBirth = dto.DateOfBirth,
                User = newUser,
                ID = newUser.ID
            };
            _context.UserProfiles.Add(newUserProfile);

            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(newUser);
        }


        public async Task<IEnumerable<UserDto>> GetAll()
        {
            IEnumerable<User> users = await _context.Users
                .Include(u => u.Profile)
                .ToListAsync();


            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetSingle(int id)
        {
            if (!await _context.Users.AnyAsync(u => u.ID == id)) throw new Exception($"Nie znaleziono usera o id {id}");
            var user = await _context.Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.ID == id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<CourseDto>> GetUserCourses(int id)
        {
            if (!await _context.Users.AnyAsync(u => u.ID == id)) throw new Exception($"Nie znaleziono usera o id {id}");
            var user = await _context.Users
                                .Include(u => u.CourseEnrollments)
                                .ThenInclude(c => c.Course)
                                .ThenInclude(c => c.Lessons)
                                .AsSingleQuery()
                                .FirstOrDefaultAsync(u => u.ID == id);


            IEnumerable<CourseEnrollment> userCourses = user.CourseEnrollments;


            return _mapper.Map<IEnumerable<CourseDto>>(userCourses);
        }

        public async Task DeleteUser(int id)
        {
            if (!await _context.Users.AnyAsync(u => u.ID == id)) throw new KeyNotFoundException($"Nie znaleziono usera o id {id}");

            User user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }
    }
}
