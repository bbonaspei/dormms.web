using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Buildings")]
    public class Building
    {
        [Key][Column("id")] public int id { get; set; }
        [Column("building_name")] public string buildingName { get; set; } = string.Empty;
        [Column("building_code")] public string buildingCode { get; set; } = string.Empty;
        public string? address { get; set; }
        [Column("total_floors")] public int totalFloors { get; set; }
        [Column("has_elevator")] public bool hasElevator { get; set; }
        public string status { get; set; } = "Active";
    }
}