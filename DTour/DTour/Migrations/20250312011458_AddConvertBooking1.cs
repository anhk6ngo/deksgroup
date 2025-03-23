using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTour.Migrations
{
    /// <inheritdoc />
    public partial class AddConvertBooking1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "storebookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    DepartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CommitAmount = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    BookingCode = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    BookingSessionId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TicketType = table.Column<int>(type: "integer", nullable: false),
                    From = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    To = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Adt = table.Column<int>(type: "integer", nullable: false),
                    Chd = table.Column<int>(type: "integer", nullable: false),
                    Inf = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(255)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storebookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "topups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestAmount = table.Column<double>(type: "double precision", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ApproveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApproveAmount = table.Column<double>(type: "double precision", nullable: false),
                    UserId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AccNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ApproveNote = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TranId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Gateway = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    IsDeposit = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(255)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "character varying(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "userbalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    DepositAmount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userbalances", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_storebookings_IsActive_Status_TicketType_UserId",
                table: "storebookings",
                columns: new[] { "IsActive", "Status", "TicketType", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_topups_IsActive_RequestDate_Status_UserId",
                table: "topups",
                columns: new[] { "IsActive", "RequestDate", "Status", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "storebookings");

            migrationBuilder.DropTable(
                name: "topups");

            migrationBuilder.DropTable(
                name: "userbalances");
        }
    }
}
