using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API_Managment_Courses.Models.Api
{
    public class ApiResponse<T>
    {
        public Boolean success { get; set; }

        public T data { get; set; }

        public string message { get; set; }

        public Dictionary<string, string[]> errors { get; set; }
    }
}
