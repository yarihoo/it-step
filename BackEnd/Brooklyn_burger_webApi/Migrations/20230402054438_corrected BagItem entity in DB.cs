using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Internet_Market_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class correctedBagItementityinDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "BagItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "BagItems");
        }
    }
}
