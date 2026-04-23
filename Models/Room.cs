using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        [Column("room_number")]
        public string roomNumber { get; set; } = string.Empty;

        [Column("building_id")]
        public int? buildingId { get; set; }

        [ForeignKey("buildingId")]
        public virtual Building? Building { get; set; }

        [Column("room_type_id")]
        public int? roomTypeId { get; set; }

        [ForeignKey("roomTypeId")]
        public virtual RoomType? RoomType { get; set; }

        [Range(0, 50, ErrorMessage = "Floor level cannot be negative")]
        [Column("floor_number")]
        public int? floorNumber { get; set; }

        [Range(1, 5, ErrorMessage = "Capacity must be between 1 and 5")]
        [Column("capacity")]
        public int? capacity { get; set; }

        [Column("current_occupancy")]
        public int? currentOccupancy { get; set; }

        [Column("status")]
        public string? status { get; set; }

        [Column("notes")]
        public string? notes { get; set; }

        [Column("has_bathroom")]
        public bool hasBathroom { get; set; }

        [Column("created_at")]
        public DateTime? createdAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? updatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Allocation> Allocations { get; set; } = new List<Allocation>();
        public virtual ICollection<MaintenanceRequest> MaintenanceRequests { get; set; } = new List<MaintenanceRequest>();
    }
}

