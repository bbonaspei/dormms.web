using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormMS.Web.Models
{
    [Table("Buildings")]
    public class Building
    {
        [Key]
        public int id { get; set; }
        public string buildingName { get; set; }
        public string buildingCode { get; set; }
        public string? address { get; set; }
        public int totalFloors { get; set; }
        public bool hasElevator { get; set; }
        public string status { get; set; }
    }
}