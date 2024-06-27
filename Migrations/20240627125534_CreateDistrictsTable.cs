using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoDeResiduos.Migrations
{
    /// <inheritdoc />
    public partial class CreateDistrictsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DISTRICTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "DISTRICTS_SEQ.NEXTVAL"),
                    REGION_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISTRICTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DISTRICTS_REGION_ID",
                        column: x => x.REGION_ID,
                        principalTable: "REGIONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DISTRICTS_REGION_ID",
                table: "DISTRICTS",
                column: "REGION_ID");
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DISTRICTS");
            
        }
    }
}
