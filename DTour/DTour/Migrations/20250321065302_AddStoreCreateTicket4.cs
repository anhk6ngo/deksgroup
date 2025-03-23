using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTour.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreCreateTicket4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
