
public class User
{
    public int ID { get; set; }
    public string Email { get; set; }

    public UserProfile Profile { get; set; }

    public IEnumerable<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();


}

