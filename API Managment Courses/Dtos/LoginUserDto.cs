using System.ComponentModel.DataAnnotations;

namespace API_Managment_Courses.Dtos
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Adres email jest wymagany")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Hasło jest wymagane")]
        public string Password { get; set; }

    }
}
