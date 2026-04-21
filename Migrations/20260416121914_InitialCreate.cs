using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DormMS.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    building_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    building_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    total_floors = table.Column<int>(type: "int", nullable: false),
                    has_elevator = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fee_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fee_category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_recurring = table.Column<bool>(type: "bit", nullable: false),
                    recurrence_interval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    bed_count = table.Column<int>(type: "int", nullable: false),
                    has_bathroom = table.Column<bool>(type: "bit", nullable: false),
                    has_air_conditioner = table.Column<bool>(type: "bit", nullable: false),
                    base_price_per_month = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    profilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    lastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role_permissions",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int", nullable: false),
                    permissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_permissions", x => new { x.roleId, x.permissionId });
                    table.ForeignKey(
                        name: "FK_Role_permissions_Permissions_permissionId",
                        column: x => x.permissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_permissions_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    room_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    building_id = table.Column<int>(type: "int", nullable: true),
                    room_type_id = table.Column<int>(type: "int", nullable: true),
                    floor_number = table.Column<int>(type: "int", nullable: true),
                    capacity = table.Column<int>(type: "int", nullable: true),
                    current_occupancy = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.id);
                    table.ForeignKey(
                        name: "FK_Rooms_Buildings_building_id",
                        column: x => x.building_id,
                        principalTable: "Buildings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_room_type_id",
                        column: x => x.room_type_id,
                        principalTable: "RoomTypes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Audit_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    entityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    entityId = table.Column<int>(type: "int", nullable: false),
                    oldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ipAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Audit_logs_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_read = table.Column<bool>(type: "bit", nullable: false),
                    target_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_roles",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    assignedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_roles", x => new { x.userId, x.roleId });
                    table.ForeignKey(
                        name: "FK_User_roles_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_roles_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    student_id_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    university = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    course = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    year_of_study = table.Column<int>(type: "int", nullable: true),
                    emergency_contact_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emergency_contact_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    emergency_contact_relationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    room_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.id);
                    table.ForeignKey(
                        name: "FK_Students_Rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "Rooms",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Students_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Allocations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    room_id = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    actual_check_in = table.Column<DateTime>(type: "datetime2", nullable: true),
                    actual_check_out = table.Column<DateTime>(type: "datetime2", nullable: true),
                    is_current = table.Column<bool>(type: "bit", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    security_deposit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    key_card_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allocations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Allocations_Rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "Rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allocations_Students_student_id",
                        column: x => x.student_id,
                        principalTable: "Students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance_requests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    request_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    student_id = table.Column<int>(type: "int", nullable: true),
                    room_id = table.Column<int>(type: "int", nullable: true),
                    issue_category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    assigned_to = table.Column<int>(type: "int", nullable: true),
                    request_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    completed_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    student_rating = table.Column<int>(type: "int", nullable: true),
                    student_feedback = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance_requests", x => x.id);
                    table.ForeignKey(
                        name: "FK_Maintenance_requests_Rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "Rooms",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Maintenance_requests_Students_student_id",
                        column: x => x.student_id,
                        principalTable: "Students",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Maintenance_requests_Users_assigned_to",
                        column: x => x.assigned_to,
                        principalTable: "Users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    transaction_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    received_by = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Payments_Students_student_id",
                        column: x => x.student_id,
                        principalTable: "Students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    penalty_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    applied_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.id);
                    table.ForeignKey(
                        name: "FK_Penalties_Students_student_id",
                        column: x => x.student_id,
                        principalTable: "Students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student_documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    document_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    document_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_size = table.Column<int>(type: "int", nullable: true),
                    uploaded_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_Student_documents_Students_student_id",
                        column: x => x.student_id,
                        principalTable: "Students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student_fees",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_id = table.Column<int>(type: "int", nullable: false),
                    fee_id = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student_fees", x => x.id);
                    table.ForeignKey(
                        name: "FK_Student_fees_Fees_fee_id",
                        column: x => x.fee_id,
                        principalTable: "Fees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Student_fees_Students_student_id",
                        column: x => x.student_id,
                        principalTable: "Students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "id", "address", "building_code", "building_name", "has_elevator", "status", "total_floors" },
                values: new object[] { 1, null, "A", "Alpha Block", true, "Active", 4 });

            migrationBuilder.InsertData(
                table: "Fees",
                columns: new[] { "id", "amount", "description", "fee_category", "fee_name", "is_recurring", "recurrence_interval" },
                values: new object[] { 1, 850m, null, "Accommodation", "Monthly Rent", true, null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "id", "description", "roleName" },
                values: new object[,]
                {
                    { 1, "Sistem yöneticisi", "Admin" },
                    { 2, "Yurt müdürü", "DormManager" },
                    { 3, "Öğrenci", "Student" },
                    { 4, "Personel / Tekniker", "Staff" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "id", "base_price_per_month", "bed_count", "capacity", "description", "has_air_conditioner", "has_bathroom", "type_name" },
                values: new object[,]
                {
                    { 1, 850m, 1, 1, "Premium privacy", false, true, "Executive Single" },
                    { 2, 600m, 2, 2, "Shared comfort", false, true, "Collaborative Twin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "createdAt", "email", "firstName", "isActive", "lastLogin", "lastName", "passwordHash", "phone", "profilePicture", "updatedAt", "username" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1610), "admin@dorm.com", "System", true, null, "Admin", "123", null, null, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1611), "admin" },
                    { 2, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1615), "staff@dorm.com", "John", true, null, "Staff", "123", null, null, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1615), "staff" },
                    { 3, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1617), "student@dorm.com", "Emily", true, null, "Resident", "123", null, null, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1618), "student" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "id", "building_id", "capacity", "created_at", "current_occupancy", "floor_number", "notes", "room_number", "room_type_id", "status", "updated_at" },
                values: new object[] { 1, 1, 1, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1808), 1, 1, null, "101", 1, "Occupied", new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1813) });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "id", "address", "city", "course", "created_at", "date_of_birth", "emergency_contact_name", "emergency_contact_phone", "emergency_contact_relationship", "gender", "nationality", "room_id", "status", "student_id_number", "university", "updated_at", "user_id", "year_of_study" },
                values: new object[] { 1, null, null, null, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1674), null, null, null, null, null, null, null, "Active", "STU001", null, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1674), 3, null });

            migrationBuilder.InsertData(
                table: "User_roles",
                columns: new[] { "roleId", "userId", "assignedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1694) },
                    { 4, 2, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1699) },
                    { 3, 3, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1699) }
                });

            migrationBuilder.InsertData(
                table: "Allocations",
                columns: new[] { "id", "actual_check_in", "actual_check_out", "created_at", "created_by", "end_date", "is_current", "key_card_number", "room_id", "security_deposit", "start_date", "status", "student_id" },
                values: new object[] { 1, null, null, new DateTime(2026, 4, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1850), null, null, true, null, 1, 1000m, new DateTime(2026, 2, 16, 15, 19, 14, 233, DateTimeKind.Local).AddTicks(1852), "Checked-In", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Allocations_room_id",
                table: "Allocations",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Allocations_student_id",
                table: "Allocations",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Audit_logs_userId",
                table: "Audit_logs",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_requests_assigned_to",
                table: "Maintenance_requests",
                column: "assigned_to");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_requests_room_id",
                table: "Maintenance_requests",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_requests_student_id",
                table: "Maintenance_requests",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_user_id",
                table: "Notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_student_id",
                table: "Payments",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_student_id",
                table: "Penalties",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Role_permissions_permissionId",
                table: "Role_permissions",
                column: "permissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_building_id",
                table: "Rooms",
                column: "building_id");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_room_type_id",
                table: "Rooms",
                column: "room_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_documents_student_id",
                table: "Student_documents",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_fees_fee_id",
                table: "Student_fees",
                column: "fee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Student_fees_student_id",
                table: "Student_fees",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_room_id",
                table: "Students",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_user_id",
                table: "Students",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_roles_roleId",
                table: "User_roles",
                column: "roleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allocations");

            migrationBuilder.DropTable(
                name: "Audit_logs");

            migrationBuilder.DropTable(
                name: "Maintenance_requests");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Penalties");

            migrationBuilder.DropTable(
                name: "Role_permissions");

            migrationBuilder.DropTable(
                name: "Student_documents");

            migrationBuilder.DropTable(
                name: "Student_fees");

            migrationBuilder.DropTable(
                name: "User_roles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Fees");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
