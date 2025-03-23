using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTour.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentType1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_storebookings_IsActive_Status_TicketType_UserId",
                table: "storebookings");

            migrationBuilder.CreateIndex(
                name: "IX_storebookings_IsActive_CreatedOn_Status_TicketType_UserId",
                table: "storebookings",
                columns: new[] { "IsActive", "CreatedOn", "Status", "TicketType", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_storebookings_IsActive_CreatedOn_Status_TicketType_UserId",
                table: "storebookings");

            migrationBuilder.CreateIndex(
                name: "IX_storebookings_IsActive_Status_TicketType_UserId",
                table: "storebookings",
                columns: new[] { "IsActive", "Status", "TicketType", "UserId" });
        }
    }
}
