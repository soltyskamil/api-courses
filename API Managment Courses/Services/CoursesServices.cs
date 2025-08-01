using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using AutoMapper;
namespace API_Managment_Courses.Services
{
    public class CoursesServices : ICourseServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public CoursesServices(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<CourseDto>> GetAll()
        {
            IEnumerable<Course> courses = await _context.Courses
                .Include(c => c.Lessons)
                .ToListAsync();
            return _mapper.Map<IEnumerable<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetSingle(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Lessons)
                .FirstOrDefaultAsync(c => c.ID == id);
            return _mapper.Map<CourseDto>(course);

        }


        public async Task<Course> CreateCourse(CreateCourseDto dto)
        {
            Course course = new Course
            {
                Title = dto.Title,
                Description = dto.Description
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task DeleteCourse(int courseId)
        {
            if (!await _context.Courses.AnyAsync(c => c.ID == courseId)) throw new Exception("Course not found");
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.ID == courseId);

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            

        }

        public async Task AssignToUser(int courseId, int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == userId);


            if (!await _context.Users.AnyAsync(u => u.ID == userId))
                throw new Exception("User not found");


            if (!await _context.Courses.AnyAsync(c => c.ID == courseId))
                throw new Exception("Course not found");


            if (await _context.CourseEnrollments.AnyAsync(c => (c.UserID == userId && c.CourseID == courseId)))
                throw new Exception("Already assigned");


            _context.CourseEnrollments.Add(new CourseEnrollment { UserID = userId, CourseID = courseId });
            await _context.SaveChangesAsync();
        }
    }
}
