using Microsoft.AspNetCore.Mvc;
using DormMS.Web.Interfaces;
using DormMS.Web.Models;

namespace DormMS.Web.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository _repo;
        private readonly IAuditService _audit; // YENİ: Denetim servisi eklendi

        // Constructor güncellendi: Artık Audit servisini de içeri alıyor
        public BuildingService(IBuildingRepository repo, IAuditService audit)
        {
            _repo = repo;
            _audit = audit;
        }

        public async Task<IEnumerable<Building>> GetBuildingsListAsync() => await _repo.GetAllAsync();

        public async Task CreateBuildingAsync(Building building)
        {
            // 1. Binayı varsayılan olarak aktif yap (Eğer kullanıcı seçmemişse)
            if (string.IsNullOrEmpty(building.status))
            {
                building.status = "Active";
            }

            // 2. Binayı veritabanına kaydet
            await _repo.AddAsync(building);

            // 3. KRİTİK ADIM: Yapılan işlemi Audit tablosuna otomatik logla
            // Hoca sorduğunda: "Sistemde izlenebilirliği (traceability) sağlamak için her CREATE işlemini logluyoruz" dersin.
            await _audit.LogActionAsync(
                "CREATE",           // Yapılan işlem türü
                "Building",         // Hangi tabloda yapıldı
                building.id,        // Değişen kaydın ID'si
                null,               // Eski değer (yeni kayıt olduğu için null)
                building.buildingName // Yeni değer (Eklenen binanın adı)
            );
        }
        public async Task<Building?> GetBuildingByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id); // Repository üzerinden çağırır
        }

        public async Task UpdateBuildingAsync(Building building)
        {
            await _repo.UpdateAsync(building); // Repository üzerinden günceller
        }

        public async Task DeleteBuildingAsync(int id)
        {
            var building = await _repo.GetByIdAsync(id);
            if (building != null)
            {
                await _repo.DeleteAsync(id);

                // AUDIT LOG: Silme işlemini kaydet
                await _audit.LogActionAsync(
                    "DELETE",
                    "Building",
                    id,
                    building.buildingName,
                    null
                );
            }
        }
    }
}