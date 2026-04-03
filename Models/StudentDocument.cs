using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Student_Documents")] // SQL'deki isimle birebir aynı yaptık (Büyük D)
    public class StudentDocument
    {
        [Key]
        public int id { get; set; }

        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        public string documentType { get; set; } = ""; // ID, Contract, Insurance vb.
        public string documentName { get; set; } = ""; // Dosyanın adı
        public string filePath { get; set; } = "";    // Dosyanın sunucudaki yolu
        public DateTime uploadedAt { get; set; } = DateTime.Now;
    }
}