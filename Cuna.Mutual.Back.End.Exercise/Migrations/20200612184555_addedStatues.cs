using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cuna.Mutual.Back.End.Exercise.Api.Migrations
{
    public partial class addedStatues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTimeOffset>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    MacGuffinId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Status_MacGuffin_MacGuffinId",
                        column: x => x.MacGuffinId,
                        principalTable: "MacGuffin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Status_MacGuffinId",
                table: "Status",
                column: "MacGuffinId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
