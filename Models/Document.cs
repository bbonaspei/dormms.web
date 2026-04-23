using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    public class Document
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string documentName { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string filePath { get; set; } = string.Empty;

        [StringLength(100)]
        public string? documentType { get; set; }

        public int? uploadedByUserId { get; set; }

        public DateTime uploadedAt { get; set; } = DateTime.Now;

        [ForeignKey("uploadedByUserId")]
        public virtual User? Uploader { get; set; }
    }
}

