using System.ComponentModel.DataAnnotations;

namespace DormMS.Web.Models
{
    public class SystemSetting
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string settingKey { get; set; } = string.Empty;

        [Required]
        public string settingValue { get; set; } = string.Empty;

        [StringLength(255)]
        public string? description { get; set; }

        [StringLength(50)]
        public string? category { get; set; }

        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}

