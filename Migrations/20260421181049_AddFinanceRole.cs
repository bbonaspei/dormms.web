using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddFinanceRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6171), new DateTime(2026, 2, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6172) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "id", "description", "roleName" },
                values: new object[] { 5, "Mali İşler Personeli", "Finance" });

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
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6059), new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6060) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6063), new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6063) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6065), new DateTime(2026, 4, 21, 21, 10, 48, 761, DateTimeKind.Local).AddTicks(6066) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 20, 20, 44, 3, 270, DateTimeKind.Local).AddTicks(54), new DateTime(2026, 2, 20, 20, 44, 3, 270, DateTimeKind.Local).AddTicks(57) });

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
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9939), new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9940) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9943), new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9943) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9945), new DateTime(2026, 4, 20, 20, 44, 3, 269, DateTimeKind.Local).AddTicks(9945) });
        }
    }
}
