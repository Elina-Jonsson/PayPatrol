using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayPatrol.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSubscriptionRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_ServiceCatalogs_ServiceCatalogId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceCatalogId",
                table: "Subscriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CategoryId",
                table: "Subscriptions",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Categories_CategoryId",
                table: "Subscriptions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_ServiceCatalogs_ServiceCatalogId",
                table: "Subscriptions",
                column: "ServiceCatalogId",
                principalTable: "ServiceCatalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Categories_CategoryId",
                table: "Subscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_ServiceCatalogs_ServiceCatalogId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_CategoryId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceCatalogId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_ServiceCatalogs_ServiceCatalogId",
                table: "Subscriptions",
                column: "ServiceCatalogId",
                principalTable: "ServiceCatalogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
