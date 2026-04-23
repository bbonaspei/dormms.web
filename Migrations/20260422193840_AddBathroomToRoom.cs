using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddBathroomToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "has_bathroom",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "created_at", "has_bathroom", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4767), false, new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "has_bathroom", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4775), false, new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4775) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "has_bathroom", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4777), false, new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4778) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "has_bathroom", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4780), false, new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4780) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "has_bathroom", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4782), false, new DateTime(2026, 4, 22, 22, 38, 40, 163, DateTimeKind.Local).AddTicks(4782) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "has_bathroom",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5715), new DateTime(2026, 2, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5716) });

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5723), new DateTime(2026, 3, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5724) });

            migrationBuilder.UpdateData(
                table: "Allocations",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "start_date" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5726), new DateTime(2026, 4, 12, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5726) });

            migrationBuilder.UpdateData(
                table: "Audit_logs",
                keyColumn: "id",
                keyValue: 1,
                column: "createdAt",
                value: new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5804));

            migrationBuilder.UpdateData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 1,
                column: "request_date",
                value: new DateTime(2026, 4, 20, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5764));

            migrationBuilder.UpdateData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 2,
                column: "request_date",
                value: new DateTime(2026, 4, 17, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5766));

            migrationBuilder.UpdateData(
                table: "Maintenance_requests",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "completed_date", "request_date" },
                values: new object[] { new DateTime(2026, 4, 14, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5769), new DateTime(2026, 4, 12, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5768) });

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "id",
                keyValue: 1,
                column: "payment_date",
                value: new DateTime(2026, 4, 17, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5786));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "id",
                keyValue: 2,
                column: "payment_date",
                value: new DateTime(2026, 4, 19, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5788));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5683), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5685) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5690), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5690) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5693), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5693) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5695), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5695) });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5697), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5697) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5566), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5567) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5569), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5569) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5570), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5571) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5572), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5573) });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5574), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5574) });

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
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 4 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5596));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 5 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5597));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 6 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5597));

            migrationBuilder.UpdateData(
                table: "User_roles",
                keyColumns: new[] { "roleId", "userId" },
                keyValues: new object[] { 3, 7 },
                column: "assignedAt",
                value: new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5598));

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
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5536), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5536) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5538), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5538) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 5,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5540), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5540) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 6,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5542), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5543) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 7,
                columns: new[] { "createdAt", "updatedAt" },
                values: new object[] { new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5544), new DateTime(2026, 4, 22, 21, 21, 14, 501, DateTimeKind.Local).AddTicks(5545) });
        }
    }
}
