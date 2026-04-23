using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Student_documents")]
    public class StudentDocument
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("student_id")]
        public int studentId { get; set; }

        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        [Column("document_type")]
        public string documentType { get; set; } = "";

        [Column("document_name")]
        public string documentName { get; set; } = "";

        [Column("file_path")]
        public string filePath { get; set; } = "";

        [Column("file_size")]
        public int? fileSize { get; set; }

        [Column("uploaded_at")]
        public DateTime uploadedAt { get; set; } = DateTime.Now;
    }
}

