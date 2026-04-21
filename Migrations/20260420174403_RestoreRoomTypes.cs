using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DormMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class RestoreRoomTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    documentName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    filePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    documentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    uploadedByUserId = table.Column<int>(type: "int", nullable: true),
                    uploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_Documents_Users_uploadedByUserId",
                        column: x => x.uploadedByUserId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SystemSettings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    settingKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    settingValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemSettings", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 20, 20, 44, 3, 270, DateTimeKind.Local).AddTicks(54), new DateTime(2026, 2, 20, 20, 44, 3, 270, DateTimeKind.Local).AddTicks(57) });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "base_price_per_month", "description" },
                values: new object[] { 1200m, "Premium privacy and high-end comfort." });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "base_price_per_month", "description" },
                values: new object[] { 950m, "Perfect for friends and shared living." });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "id", "base_price_per_month", "bed_count", "capacity", "description", "has_air_conditioner", "has_bathroom", "type_name" },
                values: new object[,]
                {
                    { 3, 750m, 3, 3, "Spacious shared living for groups.", false, true, "Triple Shared" },
                    { 4, 600m, 4, 4, "Economical shared housing.", false, false, "Standard Quad" },
                    { 5, 450m, 6, 6, "Affordable community living.", false, false, "Budget Shared" }
                });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 20, 20, 44, 3, 270, DateTimeKind.Local).AddTicks(35), new DateTime(2026, 4, 20, 20, 44, 3, 270, DateTimeKind.Local).AddTicks(38) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9964), new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9964) });

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 1, 1 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9978));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 4, 2 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9980));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 3 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9981));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "createdAt", "passwordHash", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9939), "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9940) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "passwordHash", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9943), "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9943) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "createdAt", "passwordHash", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9945), "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9945) });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_uploadedByUserId",
                table: "Documents",
                column: "uploadedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "SystemSettings");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1850), new DateTime(2026, 2, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1852) });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "base_price_per_month", "description" },
                values: new object[] { 850m, "Premium privacy" });

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "base_price_per_month", "description" },
                values: new object[] { 600m, "Shared comfort" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1808), new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1813) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1674), new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1674) });

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 1, 1 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1694));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 4, 2 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1699));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 3 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1699));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "createdAt", "passwordHash", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1610), "123", new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1611) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "passwordHash", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1615), "123", new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1615) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "createdAt", "passwordHash", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1617), "123", new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1618) });
        }
    }
}
