using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class LinkRoomTypeToFee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fee_id",
                table: "RoomTypes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6844), new DateTime(2026, 2, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6846) });

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6853), new DateTime(2026, 3, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6854) });

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6856), new DateTime(2026, 4, 13, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6857) });

            migrationBuilder.UpdateData(
                table: "Audit_logs",
                keyColumn: "id",
                keyValue: 1,
                column: "createdAt",
                value: new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6936));

            migrationBuilder.UpdateData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 1,
                column: "request_date",
                value: new DateTime(2026, 4, 21, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6894));

            migrationBuilder.UpdateData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 2,
                column: "request_date",
                value: new DateTime(2026, 4, 18, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6897));

            migrationBuilder.UpdateData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "completed_date", "request_date" },
                values: new object[] { new DateTime(2026, 4, 15, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6900), new DateTime(2026, 4, 13, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6899) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "id",
                keyValue: 1,
                column: "payment_date",
                value: new DateTime(2026, 4, 18, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6918));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "id",
                keyValue: 2,
                column: "payment_date",
                value: new DateTime(2026, 4, 20, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6921));

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 1,
                column: "fee_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 2,
                column: "fee_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 3,
                column: "fee_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 4,
                column: "fee_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "RoomTypes",
                keyColumn: "id",
                keyValue: 5,
                column: "fee_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6739), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6743) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6749), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6750) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6821), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6821) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6823), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6824) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6826), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6643), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6644) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6646), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6647) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6649), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6650) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6652), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6652) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6654), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6655) });

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 1, 1 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6671));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 4, 2 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6673));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 3 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6674));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 4 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6675));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 5 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6676));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 6 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6677));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 7 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6678));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6594), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6594) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6598), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6599) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6601), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6602) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6605), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6605) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6608), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6608) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6611), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6611) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6614), new DateTime(2026, 4, 23, 3, 32, 58, 687, DateTimeKind.Local).AddTicks(6614) });

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_fee_id",
                table: "RoomTypes",
                column: "fee_id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTypes_Fees_fee_id",
                table: "RoomTypes",
                column: "fee_id",
                principalTable: "Fees",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomTypes_Fees_fee_id",
                table: "RoomTypes");

            migrationBuilder.DropIndex(
                name: "IX_RoomTypes_fee_id",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "fee_id",
                table: "RoomTypes");

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4801), new DateTime(2026, 2, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4802) });

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4810), new DateTime(2026, 3, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4811) });

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4812), new DateTime(2026, 4, 12, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4813) });

            migrationBuilder.UpdateData(
                table: "Audit_logs",
                keyColumn: "id",
                keyValue: 1,
                column: "createdAt",
                value: new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4916));

            migrationBuilder.UpdateData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 1,
                column: "request_date",
                value: new DateTime(2026, 4, 20, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4855));

            migrationBuilder.UpdateData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 2,
                column: "request_date",
                value: new DateTime(2026, 4, 17, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4857));

            migrationBuilder.UpdateData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "completed_date", "request_date" },
                values: new object[] { new DateTime(2026, 4, 14, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4860), new DateTime(2026, 4, 12, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4859) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "id",
                keyValue: 1,
                column: "payment_date",
                value: new DateTime(2026, 4, 17, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4875));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "id",
                keyValue: 2,
                column: "payment_date",
                value: new DateTime(2026, 4, 19, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4877));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4767), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4775), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4775) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4777), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4778) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4780), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4782), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4782) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4662), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4662) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4664), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4665) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4666), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4667) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4668), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4669) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4670), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4671) });

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 1, 1 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4689));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 4, 2 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4691));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 3 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4692));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 4 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4692));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 5 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4693));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 6 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4694));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 7 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4694));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4590), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4591) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4594), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4594) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4597), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4597) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4627), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4627) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4629), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4630) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4632), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4632) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4634), new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4634) });
        }
    }
}
