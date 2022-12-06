using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballAppSoftuni.Data.Migrations
{
    public partial class TicketPriceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TicketPrice",
                table: "Matches",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Matches");
        }
    }
}
