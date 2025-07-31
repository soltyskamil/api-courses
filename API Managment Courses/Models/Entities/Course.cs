
public class Course
{
    public int ID { get; set; }
    public string Title{ get; set; }
    public string Description{ get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

}

