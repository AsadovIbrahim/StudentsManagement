using StudentManagement.Models.Abstracts;

namespace StudentManagement.Models.Concretes
{
    public class Grade : BaseEntity
    {
        public double Grades { get; set; }
        public DateTime EvaluatedAt { get; set; } = DateTime.Now;

        //Foreign Key
        public string StudentId { get; set; }
        public User Student { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }

    }
}
