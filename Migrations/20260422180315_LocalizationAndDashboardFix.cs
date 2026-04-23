using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class LocalizationAndDashboardFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(372), new DateTime(2026, 2, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(374) });

            migrationBuilder.UpdateData(
                table: "Buildings",
                keyColumn: "id",
                keyValue: 1,
                column: "building_name",
                value: "A Blok (Merkez)");

            migrationBuilder.UpdateData(
                table: "Fees",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "fee_category", "fee_name" },
                values: new object[] { "Konaklama", "Aylık Kira" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 1,
                column: "description",
                value: "Sistem Yöneticisi");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 2,
                column: "description",
                value: "Yurt Müdürü");

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "description", "type_name" },
                values: new object[] { "Yüksek konfor ve gizlilik.", "VİP Tek Kişilik" });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "description", "type_name" },
                values: new object[] { "Arkadaşlar için ideal paylaşımlı yaşam.", "Standart Çift Kişilik" });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "description", "type_name" },
                values: new object[] { "Geniş ve sosyal oda seçeneği.", "Üç Kişilik Oda" });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "description", "type_name" },
                values: new object[] { "Ekonomik paylaşımlı konaklama.", "Dört Kişilik Oda" });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "description", "type_name" },
                values: new object[] { "Bütçe dostu toplu yaşam.", "Ekonomik Koğuş" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(349), new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(354) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(233), new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(233) });

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 1, 1 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(247));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 4, 2 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(249));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 3 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(249));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "createdAt", "firstName", "lastName", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(204), "Sistem", "Yöneticisi", new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(204) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "email", "firstName", "lastName", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(206), "personel@dorm.com", "Mehmet", "Tekniker", new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(207) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "createdAt", "email", "firstName", "lastName", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(209), "ogrenci@dorm.com", "Ayşe", "Demir", new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(209) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6171), new DateTime(2026, 2, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6172) });

            migrationBuilder.UpdateData(
                table: "Buildings",
                keyColumn: "id",
                keyValue: 1,
                column: "building_name",
                value: "Alpha Block");

            migrationBuilder.UpdateData(
                table: "Fees",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "fee_category", "fee_name" },
                values: new object[] { "Accommodation", "Monthly Rent" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 1,
                column: "description",
                value: "Sistem yöneticisi");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 2,
                column: "description",
                value: "Yurt müdürü");

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "description", "type_name" },
                values: new object[] { "Premium privacy and high-end comfort.", "Executive Single" });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "description", "type_name" },
                values: new object[] { "Perfect for friends and shared living.", "Collaborative Twin" });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "description", "type_name" },
                values: new object[] { "Spacious shared living for groups.", "Triple Shared" });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "description", "type_name" },
                values: new object[] { "Economical shared housing.", "Standard Quad" });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "description", "type_name" },
                values: new object[] { "Affordable community living.", "Budget Shared" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6151), new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6154) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6083), new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6083) });

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 1, 1 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6095));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 4, 2 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6097));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 3 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6097));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "createdAt", "firstName", "lastName", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6059), "System", "Admin", new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "email", "firstName", "lastName", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6063), "staff@dorm.com", "John", "Staff", new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6063) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "createdAt", "email", "firstName", "lastName", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6065), "student@dorm.com", "Emily", "Resident", new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6066) });
        }
    }
}
