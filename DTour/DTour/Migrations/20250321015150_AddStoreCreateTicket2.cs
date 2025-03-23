using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTour.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreCreateTicket2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_storecreatetickets_SessionId_TypeData",
                table: "storecreatetickets");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "storecreatetickets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "storecreatetickets",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_storecreatetickets_SessionId_TypeData",
                table: "storecreatetickets",
                columns: new[] { "SessionId", "TypeData" });
        }
    }
}
