using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public int id { get; set; }

        public int userId { get; set; }
        [ForeignKey("userId")]
        public virtual User? User { get; set; }

        [Required]
        public string studentId { get; set; } // Okul No

        public DateTime? dateOfBirth { get; set; }
        public string? gender { get; set; }
        public string? university { get; set; }
        public string? course { get; set; } // Bölüm
        public int? yearOfStudy { get; set; }

        // Acil Durum İletişimi (Diyagramda var)
        public string? emergencyContactName { get; set; }
        public string? emergencyContactPhone { get; set; }

        public string? status { get; set; } = "Active";
        public DateTime createdAt { get; set; } = DateTime.Now;
        // ODA BAĞLANTISINI BURAYA EKLİYORUZ (Hatanın çözümü)
        public int? roomId { get; set; }
        [ForeignKey("roomId")]
        public virtual Room? Room { get; set; }
    }
}