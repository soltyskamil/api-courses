namespace API_Managment_Courses.Dtos
{
    public class CourseDto
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<LessonDto> Lessons  { get; set; }

    }
}
