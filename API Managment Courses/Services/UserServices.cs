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
            User user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
            
            return _mapper.Map<UserDto>(user);
        }

        public async Task<ICollection<CourseEnrollment>> GetUserCourses(int id)
        {
            if (!await _context.Users.AnyAsync(u => u.ID == id)) throw new Exception($"Nie znaleziono usera o id {id}");
            User user = await _context.Users.FindAsync(id);
            return user.CourseEnrollments.ToList();
        }


    }
}
