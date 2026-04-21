using DormMS.Web.Models;

namespace DormMS.Web.Interfaces
{
    public interface IRoomService
    {
        // Listeleme ve Detay
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
        Task<Room> GetRoomDetailsAsync(int id); // Eskiden vardı, koruduk

        // Ekleme, Güncelleme, Silme
        Task CreateRoomAsync(Room room);
        Task UpdateRoomAsync(Room room); // EKLENDİ: Düzenleme hatasını çözer
        Task DeleteRoomAsync(int id);   // EKLENDİ: Tam CRUD için şart

        // Yardımcı Metodlar (Dropdownlar için)
        Task<IEnumerable<Building>> GetBuildingsAsync();
        Task<IEnumerable<RoomType>> GetRoomTypesAsync();
    }
}