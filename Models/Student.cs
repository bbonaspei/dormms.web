using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Column("user_id")] // Veritabanındaki 'user_id' ile eşleşir
        public int userId { get; set; }

        [ForeignKey("userId")]
        public virtual User? User { get; set; }

        [Required, StringLength(20)]
        [Column("student_id_number")] // Rapor Sayfa 12'deki gerçek isim
        public string studentId { get; set; } = string.Empty; // Okul No

        [Column("date_of_birth")]
        public DateTime? dateOfBirth { get; set; }

        [Column("gender")]
        public string? gender { get; set; }

        [Column("nationality")]
        public string? nationality { get; set; }

        [Column("university")]
        public string? university { get; set; }

        [Column("course")]
        public string? course { get; set; }

        [Column("year_of_study")]
        public int? yearOfStudy { get; set; }

        [Column("emergency_contact_name")]
        public string? emergencyContactName { get; set; }

        [Column("emergency_contact_phone")]
        public string? emergencyContactPhone { get; set; }

        [Column("emergency_contact_relationship")]
        public string? emergencyContactRelationship { get; set; }

        [Column("address")]
        public string? address { get; set; }

        [Column("city")]
        public string? city { get; set; }

        [Column("status")]
        public string? status { get; set; } = "Active";

        [Column("created_at")]
        public DateTime createdAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime updatedAt { get; set; } = DateTime.Now;

        [Column("room_id")] // Veritabanındaki 'room_id' ile eşleşir
        public int? roomId { get; set; }

        [ForeignKey("roomId")]
        public virtual Room? Room { get; set; }
    }
}