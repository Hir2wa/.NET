using System;

namespace E_LearningPlatform
{
    public class Course
    {
        public string CourseID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public int Credits { get; set; }
        public string Instructor { get; set; }
        public string Prerequisites { get; set; }
        public string Department { get; set; }
        public string Room { get; set; }
        public int MaxStudents { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        public Course()
        {
            CreatedDate = DateTime.Now;
            Status = "Active";
        }

        public override string ToString()
        {
            return $"{CourseCode} - {CourseName}";
        }
    }
}
// Enhanced on 2025-10-19 - Commit 1
