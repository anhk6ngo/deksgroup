using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTour.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreCreateTicket6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "RefSaveId",
                table: "storebookings",
                type: "character varying(36)",
                maxLength: 36,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateCode",
                table: "storebookings");

            migrationBuilder.DropColumn(
                name: "CreateStatus",
                table: "storebookings");

            migrationBuilder.DropColumn(
                name: "RefSaveId",
                table: "storebookings");
        }
    }
}
