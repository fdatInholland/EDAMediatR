using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers__customerId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "_deliveryTime",
                table: "Orders",
                newName: "DeliveryTime");

            migrationBuilder.RenameColumn(
                name: "_customerId",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders__customerId",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "Orders",
                newName: "_deliveryTime");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "_customerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders__customerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers__customerId",
                table: "Orders",
                column: "_customerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
