
public class Course
{
    public int ID { get; set; }
    public string Title{ get; set; }
    public string Description{ get; set; }

    public IEnumerable<Lesson> Lessons { get; set; } = new List<Lesson>();

    public IEnumerable<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

}

