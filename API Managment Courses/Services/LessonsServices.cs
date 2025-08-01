using API_Managment_Courses.Dtos;
using API_Managment_Courses.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_Managment_Courses.Services
{
    public class LessonsServices : ILessonsServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LessonsServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateLesson(CreateLessonDto dto)
        {
            Lesson newLesson = new Lesson
            {
                Description = dto.Description,
                Title = dto.Title,
                CourseID = dto.CourseID,
                Course = await _context.Courses.FirstOrDefaultAsync(c => c.ID == dto.CourseID)
            };

            _context.Lessons.Add(newLesson);
            await _context.SaveChangesAsync();
        } 

        public async Task DeleteLesson(int lessonID)
        {
            if (!await _context.Lessons.AnyAsync(l => l.ID == lessonID)) return;
            Lesson lesson = await _context.Lessons.FirstOrDefaultAsync(l => l.ID == lessonID);

            _context.Lessons.Remove(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task<LessonDto> GetLessonById(int lessonID)
        {
            if (!await _context.Lessons.AnyAsync(l => l.ID == lessonID)) throw new KeyNotFoundException($"Nie znaleziono lekcji o id {lessonID}");
            Lesson searchedLesson = await _context.Lessons.FirstOrDefaultAsync(l => l.ID == lessonID);

            return _mapper.Map<LessonDto>(searchedLesson);
        }


    }
}
