using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCartElectrioncs.Web.Migrations
{
    /// <inheritdoc />
    public partial class lastdance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrakingNumber",
                table: "OrderHeaders",
                newName: "TrakcingNumber");

            migrationBuilder.RenameColumn(
                name: "PaymentStauts",
                table: "OrderHeaders",
                newName: "PaymentStatus");

            migrationBuilder.RenameColumn(
                name: "OrderStauts",
                table: "OrderHeaders",
                newName: "OrderStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrakcingNumber",
                table: "OrderHeaders",
                newName: "TrakingNumber");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "OrderHeaders",
                newName: "PaymentStauts");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "OrderHeaders",
                newName: "OrderStauts");
        }
    }
}
