using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Helpers
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        [NotMapped]
        public List<IFormFile> Attachments { get; set; }
    }
}
