using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int id { get; set; }

        // SQL'deki ismi "roomNumber" olduğu için böyle olmalı
        public string roomNumber { get; set; }

        public int buildingId { get; set; }
        [ForeignKey("buildingId")]
        public virtual Building? Building { get; set; }

        public int roomTypeId { get; set; }
        [ForeignKey("roomTypeId")]
        public virtual RoomType? RoomType { get; set; }

        [Range(0, 50, ErrorMessage = "Floor level cannot be negative")]
        public int floorNumber { get; set; } // floor_number değil

        [Range(1,5, ErrorMessage = "Capacity must be between 1 and 5")]
        public int capacity { get; set; }
        public int currentOccupancy { get; set; } // current_occupancy değil
        public string status { get; set; }
    }
}