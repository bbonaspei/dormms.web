using System;

namespace DormMS.Web.Models
{
    public class StudentDashboardViewModel
    {
        public string StudentFirstName { get; set; } = string.Empty;

        public string RoomNumber { get; set; } = string.Empty;
        public string BuildingName { get; set; } = string.Empty;
        public int? Floor { get; set; }
        public string RoomType { get; set; } = string.Empty;
        public DateTime? CheckInDate { get; set; }
        public DateTime? LeaseEndDate { get; set; }
        public bool HasActiveLease { get; set; }

        public decimal TotalUpcomingDue { get; set; }
        public int DaysUntilDue { get; set; }
        public bool IsOverdue { get; set; }

        public int ActiveRequestCount { get; set; }
        public string? ProfilePicture { get; set; }
    }
}

