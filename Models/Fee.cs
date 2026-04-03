using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Fees")]
    public class Fee
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string feeName { get; set; } = string.Empty;

        public string? feeCategory { get; set; }

        public decimal amount { get; set; }

        public bool isRecurring { get; set; } = true;

        public string? recurrenceInterval { get; set; }
    }
}