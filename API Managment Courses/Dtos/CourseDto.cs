namespace API_Managment_Courses.Dtos
{
    public class CourseDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<Lesson> Lessons  { get; set; }

    }
}
