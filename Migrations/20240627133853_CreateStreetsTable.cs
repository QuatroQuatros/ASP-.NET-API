using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoDeResiduos.Migrations
{
    /// <inheritdoc />
    public partial class CreateStreetsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STREETS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "STREETS_SEQ.NEXTVAL"),
                    DISTRICT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STREETS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STREETS_DISTRICTS_DISTRICT_ID",
                        column: x => x.DISTRICT_ID,
                        principalTable: "DISTRICTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });
            
            migrationBuilder.CreateIndex(
                name: "IX_STREETS_DISTRICT_ID",
                table: "STREETS",
                column: "DISTRICT_ID");
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STREETS");

        }
    }
}
