using AutoMapper;
using API_Managment_Courses.Dtos;

namespace API_Managment_Courses.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourseEnrollment, CourseDto>()
                 .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Course.ID))
                 .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Course.Title))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Course.Description))
                 .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src => src.Course.Lessons));
            CreateMap<Course, CourseDto>();
            CreateMap<Lesson, LessonDto>();
            CreateMap<User, UserDto>()
                .ForMember(u => u.Name, c => c.MapFrom(p => p.Profile.Name))
                .ForMember(u => u.Surname, c => c.MapFrom(p => p.Profile.Surname))
                .ForMember(u => u.DateOfBirth, c => c.MapFrom(p => p.Profile.DateOfBirth))
                .ForMember(u => u.password, c => c.MapFrom(c => c.PasswordHash));

        }
    }
}