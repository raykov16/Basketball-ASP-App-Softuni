﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasketballAppSoftuni.Data.Migrations
{
    public partial class AddedPossitionColumnToPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Players");
        }
    }
}
