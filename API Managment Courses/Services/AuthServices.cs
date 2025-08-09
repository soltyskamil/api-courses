using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Managment_Courses.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public AuthServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<UserDto> CreateUser(UserDto dto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == dto.Email)) throw new Exception("Użytkownik już istnieje");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.password);

            var newUser = new User()
            {
                Email = dto.Email,
                PasswordHash = passwordHash,
                RoleID = 1,
            };

            _context.Users.Add(newUser);


            var newUserProfile = new UserProfile()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                DateOfBirth = dto.DateOfBirth,
                User = newUser,
            };

            _context.UserProfiles.Add(newUserProfile);

            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(newUser);
        }


        public async Task LoginUser(LoginUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) throw new Exception("Nieprawidłowe dane logowania");


            bool verified = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!verified)
                throw new Exception("Nieprawidłowe dane logowania");


        }





        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _context.Users.Include(u => u.Profile).ToListAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }



    }
}
