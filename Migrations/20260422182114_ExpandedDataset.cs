using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DormMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class ExpandedDataset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5715), new DateTime(2026, 2, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5716) });

            migrationBuilder.InsertData(
                table: "Audit_logs",
                columns: new[] { "id", "action", "createdAt", "entityId", "entityType", "ipAddress", "newValues", "oldValues", "userAgent", "userId" },
                values: new object[] { 1, "SYSTEM", new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5804), 1, "Database", null, "Seeded", "None", null, null });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "id", "address", "building_code", "building_name", "has_elevator", "status", "total_floors" },
                values: new object[] { 2, null, "B", "B Blok (Ek Bina)", false, "Active", 3 });

            migrationBuilder.UpdateData(
                table: "Fees",
                keyColumn: "id",
                keyValue: 1,
                column: "amount",
                value: 1200m);

            migrationBuilder.InsertData(
                table: "Fees",
                columns: new[] { "id", "amount", "description", "fee_category", "fee_name", "is_recurring", "recurrence_interval" },
                values: new object[] { 2, 450m, null, "Beslenme", "Yemek Bedeli", true, null });

            migrationBuilder.InsertData(
                table: "Maintenance_requests",
                columns: new[] { "id", "assigned_to", "completed_date", "description", "issue_category", "priority", "request_date", "request_number", "room_id", "status", "student_feedback", "student_id", "student_rating" },
                values: new object[] { 1, null, null, "Klima kumandası çalışmıyor.", "Elektrik", "High", new DateTime(2026, 4, 20, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5764), "MR-001", 1, "Pending", null, 1, null });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "id", "amount", "notes", "payment_date", "payment_method", "payment_reference", "received_by", "status", "student_id", "transaction_id" },
                values: new object[] { 1, 1200m, null, new DateTime(2026, 4, 17, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5786), "Credit Card", "", null, "Completed", 1, "TXN1001" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5683), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5685) });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "id", "building_id", "capacity", "created_at", "current_occupancy", "floor_number", "notes", "room_number", "room_type_id", "status", "updated_at" },
                values: new object[,]
                {
                    { 2, 1, 2, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5690), 1, 1, null, "102", 2, "Available", new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5690) },
                    { 3, 1, 3, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5693), 0, 1, null, "103", 3, "Available", new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5693) }
                });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5566), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5567) });

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 1, 1 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5591));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 4, 2 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5594));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 3 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5595));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5530), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5530) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5533), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5534) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "createdAt", "email", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5536), "ayse@email.com", new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5536) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "createdAt", "email", "firstName", "isActive", "lastLogin", "lastName", "passwordHash", "phone", "profilePicture", "updatedAt", "username" },
                values: new object[,]
                {
                    { 4, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5538), "ahmet@email.com", "Ahmet", true, null, "Yılmaz", "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", null, null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5538), "ahmet" },
                    { 5, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5540), "fatma@email.com", "Fatma", true, null, "Kaya", "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", null, null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5540), "fatma" },
                    { 6, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5542), "can@email.com", "Can", true, null, "Yıldız", "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", null, null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5543), "can" },
                    { 7, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5544), "elif@email.com", "Elif", true, null, "Şahin", "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", null, null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5545), "elif" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "id", "building_id", "capacity", "created_at", "current_occupancy", "floor_number", "notes", "room_number", "room_type_id", "status", "updated_at" },
                values: new object[,]
                {
                    { 4, 2, 2, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5695), 1, 2, null, "201", 2, "Available", new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5695) },
                    { 5, 2, 4, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5697), 0, 2, null, "202", 4, "Available", new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5697) }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "id", "address", "city", "course", "created_at", "date_of_birth", "emergency_contact_name", "emergency_contact_phone", "emergency_contact_relationship", "gender", "nationality", "room_id", "status", "student_id_number", "university", "updated_at", "user_id", "year_of_study" },
                values: new object[,]
                {
                    { 2, null, null, null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5569), null, null, null, null, null, null, null, "Active", "STU002", null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5569), 4, null },
                    { 3, null, null, null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5570), null, null, null, null, null, null, null, "Active", "STU003", null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5571), 5, null },
                    { 4, null, null, null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5572), null, null, null, null, null, null, null, "Active", "STU004", null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5573), 6, null },
                    { 5, null, null, null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5574), null, null, null, null, null, null, null, "Active", "STU005", null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5574), 7, null }
                });

            migrationBuilder.InsertData(
                table: "User_roles",
                columns: new[] { "roleId", "userId", "assignedAt" },
                values: new object[,]
                {
                    { 3, 4, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5596) },
                    { 3, 5, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5597) },
                    { 3, 6, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5597) },
                    { 3, 7, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5598) }
                });

            migrationBuilder.InsertData(
                table: "Allocations",
                columns: new[] { "id", "actual_check_in", "actual_check_out", "created_at", "created_by", "end_date", "is_current", "key_card_number", "room_id", "security_deposit", "start_date", "status", "student_id" },
                values: new object[,]
                {
                    { 2, null, null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5723), null, null, true, null, 2, 800m, new DateTime(2026, 3, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5724), "Checked-In", 2 },
                    { 3, null, null, new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5726), null, null, true, null, 4, 800m, new DateTime(2026, 4, 12, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5726), "Checked-In", 3 }
                });

            migrationBuilder.InsertData(
                table: "Maintenance_requests",
                columns: new[] { "id", "assigned_to", "completed_date", "description", "issue_category", "priority", "request_date", "request_number", "room_id", "status", "student_feedback", "student_id", "student_rating" },
                values: new object[,]
                {
                    { 2, null, null, "Dolap kapağı sallanıyor.", "Mobilya", "Medium", new DateTime(2026, 4, 17, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5766), "MR-002", 2, "In Progress", null, 2, null },
                    { 3, null, new DateTime(2026, 4, 14, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5769), "Musluk sökülmüş.", "Tesisat", "Low", new DateTime(2026, 4, 12, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5768), "MR-003", 4, "Completed", null, 3, null }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "id", "amount", "notes", "payment_date", "payment_method", "payment_reference", "received_by", "status", "student_id", "transaction_id" },
                values: new object[] { 2, 950m, null, new DateTime(2026, 4, 19, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5788), "Bank Transfer", "", null, "Completed", 2, "TXN1002" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Audit_logs",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Fees",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 5 });

            migrationBuilder.DeleteData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Buildings",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(372), new DateTime(2026, 2, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(374) });

            migrationBuilder.UpdateData(
                table: "Fees",
                keyColumn: "id",
                keyValue: 1,
                column: "amount",
                value: 850m);

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
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(204), new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(204) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(206), new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(207) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "createdAt", "email", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(209), "ogrenci@dorm.com", new DateTime(2026, 4, 22, 21, 3, 14, 736, DateTimeKind.Local).AddTicks(209) });
        }
    }
}
