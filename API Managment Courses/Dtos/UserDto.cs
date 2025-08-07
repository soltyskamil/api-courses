using System.ComponentModel.DataAnnotations;

namespace API_Managment_Courses.Dtos
{
    public class UserDto
    {

        [Required(ErrorMessage = "Adres email jest wymagany")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Data urodzenia jest wymagana")]

        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string password { get; set; }
    }
}
