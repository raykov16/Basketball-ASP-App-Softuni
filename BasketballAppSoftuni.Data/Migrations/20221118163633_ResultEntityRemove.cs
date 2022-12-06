using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballAppSoftuni.Data.Migrations
{
    public partial class ResultEntityRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Results_ResultId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Matches_ResultId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "ResultId",
                table: "Matches",
                newName: "HomeTeamPoints");

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamPoints",
                table: "Matches",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwayTeamPoints",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "HomeTeamPoints",
                table: "Matches",
                newName: "ResultId");

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamPoints = table.Column<int>(type: "int", nullable: false),
                    HomeTeamPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ResultId",
                table: "Matches",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_MatchId",
                table: "Results",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Results_ResultId",
                table: "Matches",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id");
        }
    }
}
