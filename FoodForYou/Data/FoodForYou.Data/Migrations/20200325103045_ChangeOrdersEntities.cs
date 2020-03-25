using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodForYou.Data.Migrations
{
    public partial class ChangeOrdersEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "OrderProducts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrderProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_CreatorId",
                table: "OrderProducts",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_AspNetUsers_CreatorId",
                table: "OrderProducts",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_AspNetUsers_CreatorId",
                table: "OrderProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_CreatorId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderProducts");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
