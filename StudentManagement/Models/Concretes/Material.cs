using StudentManagement.Models.Abstracts;

namespace StudentManagement.Models.Concretes
{
    public class Material : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;



        //Foreign Key
        public string TaskId { get; set; }
        public Task Task { get; set; }

    }
}
