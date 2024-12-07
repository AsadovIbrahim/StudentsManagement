using StudentManagement.Models.Abstracts;

namespace StudentManagement.Models.Concretes
{
    public class Enrollment : BaseEntity
    {
        public DateTime EnrolledAt { get; set; } = DateTime.Now;


        //Foreign Key

        public string StudentId { get; set; }
        public User Student { get; set; }

        public string CourseId { get; set; }
        public Course Course { get; set; }

    }
}
