using StudentManagement.Models.Abstracts;

namespace StudentManagement.Models.Concretes
{
    public class Task : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public double MaxGradePoint { get; set; }
        public DateTime Deadline { get; set; } = DateTime.Now;


        //Foreign Key
        public int MaterialId { get; set; }

        public Material Material { get; set; }
    }
}
