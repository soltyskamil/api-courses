
using API_Managment_Courses.Models.Entities;

public class User
{
    public int ID { get; set; }
    public string Email { get; set; }

    public int RoleID { get; set; }

    public Role Role { get; set; }

    public string PasswordHash { get; set; }

    public UserProfile Profile { get; set; }

    public IEnumerable<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();


}

