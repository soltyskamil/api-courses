using System.ComponentModel.DataAnnotations;

namespace API_Managment_Courses.Dtos
{
    public class CreateCourseDto
    {
        [Required(ErrorMessage = "Tytuł kursu jest wymagany")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Opis kursu jest wymagany")]
        public string Description { get; set; }

    }
}
