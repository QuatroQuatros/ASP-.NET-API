using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoDeResiduos.Migrations
{
    public partial class CreateGarbageCollectedTable : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GARBAGE_COLLECTED",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "GARBAGE_COLLECTED_SEQ.NEXTVAL"),
                    COLLECTION_DAY_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AMOUNT = table.Column<float>(type: "BINARY_FLOAT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GARBAGE_COLLECTED", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GARBAGE_COLLECTED_COLLECTION_DAY_COLLECTION_DAY_ID",
                        column: x => x.COLLECTION_DAY_ID,
                        principalTable: "COLLECTION_DAY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
            

            migrationBuilder.CreateIndex(
                name: "IX_GARBAGE_COLLECTED_COLLECTION_DAY_ID",
                table: "GARBAGE_COLLECTED",
                column: "COLLECTION_DAY_ID");
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GARBAGE_COLLECTED");
        }
    }
}
