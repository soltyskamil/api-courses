using System.ComponentModel.DataAnnotations;

namespace API_Managment_Courses.Dtos
{
    public class CreateLessonDto
    {
        [Required(ErrorMessage = "Tytuł lekcji jest wymagany")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Opis lekcji jest wymagany")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ID kursu jest wymagane")]
        public int CourseID { get; set; }
    }
}
