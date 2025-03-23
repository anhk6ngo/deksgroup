using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTour.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreCreateTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "storebookings",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "storecreatetickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Data = table.Column<string>(type: "text", nullable: true),
                    TypeData = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_storecreatetickets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_storecreatetickets_SessionId",
                table: "storecreatetickets",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "storecreatetickets");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "storebookings");
        }
    }
}
