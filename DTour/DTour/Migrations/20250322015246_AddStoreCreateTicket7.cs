using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTour.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreCreateTicket7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "storecreatetickets");

            migrationBuilder.DropColumn(
                name: "CreateCode",
                table: "storebookings");

            migrationBuilder.DropColumn(
                name: "CreateStatus",
                table: "storebookings");

            migrationBuilder.RenameColumn(
                name: "RefSaveId",
                table: "storebookings",
                newName: "ApiProviderId");

            migrationBuilder.AddColumn<string>(
                name: "SaveObject",
                table: "storebookings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TicketPdf",
                table: "storebookings",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaveObject",
                table: "storebookings");

            migrationBuilder.DropColumn(
                name: "TicketPdf",
                table: "storebookings");

            migrationBuilder.RenameColumn(
                name: "ApiProviderId",
                table: "storebookings",
                newName: "RefSaveId");

            migrationBuilder.AddColumn<int>(
                name: "CreateCode",
                table: "storebookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreateStatus",
                table: "storebookings",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "storecreatetickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TypeData = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storecreatetickets", x => x.Id);
                });
        }
    }
}
