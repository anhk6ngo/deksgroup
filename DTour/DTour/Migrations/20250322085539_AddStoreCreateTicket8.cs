using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTour.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreCreateTicket8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pnr",
                table: "storebookings",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pnr",
                table: "storebookings");
        }
    }
}
