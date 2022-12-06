using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballAppSoftuni.Data.Migrations
{
    public partial class UserMatchesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_AspNetUsers_MyUserId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_MyUserId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MyUserId",
                table: "Matches");

            migrationBuilder.CreateTable(
                name: "UserMatch",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMatch", x => new { x.UserId, x.MatchId });
                    table.ForeignKey(
                        name: "FK_UserMatch_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMatch_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMatch_MatchId",
                table: "UserMatch",
                column: "MatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMatch");

            migrationBuilder.AddColumn<string>(
                name: "MyUserId",
                table: "Matches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MyUserId",
                table: "Matches",
                column: "MyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_AspNetUsers_MyUserId",
                table: "Matches",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
