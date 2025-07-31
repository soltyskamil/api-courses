using AutoMapper;
using API_Managment_Courses.Dtos;

namespace API_Managment_Courses.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Course, CourseDto>();
            CreateMap<User, UserDto>()
                .ForMember(u => u.Name, c => c.MapFrom(p => p.Profile.Name))
                .ForMember(u => u.Surname, c => c.MapFrom(p => p.Profile.Surname))
                .ForMember(u => u.DateOfBirth, c => c.MapFrom(p => p.Profile.DateOfBirth));
        }
    }
}
