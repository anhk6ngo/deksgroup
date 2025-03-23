using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DTour.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreCreateTicket1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_storecreatetickets_SessionId",
                table: "storecreatetickets");

            migrationBuilder.CreateIndex(
                name: "IX_storecreatetickets_SessionId_TypeData",
                table: "storecreatetickets",
                columns: new[] { "SessionId", "TypeData" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_storecreatetickets_SessionId_TypeData",
                table: "storecreatetickets");

            migrationBuilder.CreateIndex(
                name: "IX_storecreatetickets_SessionId",
                table: "storecreatetickets",
                column: "SessionId");
        }
    }
}
