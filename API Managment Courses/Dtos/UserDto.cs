﻿namespace API_Managment_Courses.Dtos
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
