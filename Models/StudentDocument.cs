using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Student_documents")] // SQL'deki tablo adıyla birebir eşlendi
    public class StudentDocument
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("student_id")] // SQL'deki kolon adıyla eşlendi
        public int studentId { get; set; }

        [ForeignKey("studentId")]
        public virtual Student? Student { get; set; }

        [Column("document_type")] // SQL: document_type
        public string documentType { get; set; } = ""; // ID, Contract, Insurance vb.

        [Column("document_name")] // SQL: document_name
        public string documentName { get; set; } = ""; // Dosyanın adı

        [Column("file_path")] // SQL: file_path
        public string filePath { get; set; } = "";    // Dosyanın sunucudaki yolu

        // --- RAPORA GÖRE EKLENEN EKSİK ALAN (Sayfa 13) ---
        [Column("file_size")]
        public int? fileSize { get; set; } // Bayt cinsinden dosya boyutu

        [Column("uploaded_at")] // SQL: uploaded_at
        public DateTime uploadedAt { get; set; } = DateTime.Now;
    }
}