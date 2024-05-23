using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pocus.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class StatisticEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Statistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeComplete = table.Column<int>(type: "int", nullable: false),
                    PlansComplete = table.Column<int>(type: "int", nullable: false),
                    IsIgnore = table.Column<bool>(type: "bit", nullable: false),
                    TimeProductivityByHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlansProductivityByHours = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistic", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistic");
        }
    }
}
