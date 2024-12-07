using StudentManagement.Models.Abstracts;

namespace StudentManagement.Models.Concretes
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Schedule { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string GoneLimit { get; set; }




        //Foreign Key
        public string TeacherId { get; set; }
        public User Teacher { get; set; }

    }
}
