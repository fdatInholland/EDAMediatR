using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "_customerId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "_deliveryTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Orders__customerId",
                table: "Orders",
                column: "_customerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers__customerId",
                table: "Orders",
                column: "_customerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers__customerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders__customerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "_customerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "_deliveryTime",
                table: "Orders");
        }
    }
}
