using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Managment_Courses.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _jwtSettings;

        public AuthServices(AppDbContext context, IMapper mapper, IConfiguration jwtSettings)
        {
            _context = context;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
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


        public async Task<LoginResponseDto> LoginUser(LoginUserDto dto)
        {
            User user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) throw new UnauthorizedAccessException("Nieprawidłowe dane logowania");
            bool verify = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            
            if(!verify)
                throw new UnauthorizedAccessException("Nieprawidłowe dane logowania");
            string token = CreateToken(user);

            LoginResponseDto resLoginDto = new LoginResponseDto { UserID = user.ID, RoleName = user.Role.Name, Token = token };
            return resLoginDto;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public string CreateToken(User user)
        {

            var token = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings["Jwt:key"]));
            var audience = _jwtSettings["Jwt:audience"];
            



            var claims = new List<Claim>
            {
                new Claim("userId", user.ID.ToString()),
                new Claim("role", user.Role.Name),
            };

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings["Jwt:issuer"],
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        }

    }
}
