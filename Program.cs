using DormMS.Web.Data;
using DormMS.Web.Interfaces;
using DormMS.Web.Repositories; // Eksik olan buydu
using DormMS.Web.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. ADIM: MVC Servislerini ekle
builder.Services.AddControllersWithViews();

// 2. ADIM: Veritabaný Baðlantýsýný Tanýmla
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. ADIM: "GÖREV - KÝÞÝ" EÞLEÞTÝRMELERÝ (Dependency Injection)
// Burada kural þudur: <GörevÝsmi, GerçekKod>

// ROOM MODÜLÜ
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();

// BUILDING MODÜLÜ (Hata buradaydý, düzelttim)
builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IBuildingService, BuildingService>();

// ALLOCATION MODÜLÜ (Hata buradaydý, düzelttim)
builder.Services.AddScoped<IAllocationService, AllocationService>();

builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IRoomTypeService, RoomTypeService>();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

// Program.cs içine eklenecek satýrlar:
builder.Services.AddScoped<IAllocationRepository, AllocationRepository>();
builder.Services.AddScoped<IAllocationService, AllocationService>();

builder.Services.AddScoped<IFinancialService, FinancialService>();

builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();

// 1. HTTP bilgilerine (IP, Tarayýcý vb.) eriþmek için gereken anahtar servis
builder.Services.AddHttpContextAccessor();

// 2. Kendi yazdýðýmýz Audit servisini sisteme tanýtýyoruz
builder.Services.AddScoped<DormMS.Web.Interfaces.IAuditService, DormMS.Web.Services.AuditService>();

builder.Services.AddScoped<INotificationService, NotificationService>();

builder.Services.AddScoped<IDocumentService, DocumentService>();

var app = builder.Build();

// 4. ADIM: HTTP Ýstek Hattý Ayarlarý
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Baþlangýç sayfasý: Home -> Landing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Landing}/{id?}");

app.Run();