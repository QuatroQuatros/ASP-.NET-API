using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoDeResiduos.Migrations
{
    /// <inheritdoc />
    public partial class CreateRegionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "REGIONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "REGIONS_SEQ.NEXTVAL"),
                    STATE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REGIONS_STATES_STATE_ID",
                        column: x => x.STATE_ID,
                        principalTable: "STATES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_REGIONS_STATE_ID",
                table: "REGIONS",
                column: "STATE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REGIONS");
            
        }
    }
}
