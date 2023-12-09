using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internet_Market_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class createdsale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SalePrice",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Products");
        }
    }
}
