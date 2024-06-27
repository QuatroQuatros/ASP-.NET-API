using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoDeResiduos.Migrations
{
    /// <inheritdoc />
    public partial class CreateCollectionDaysTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COLLECTION_DAY",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "COLLECTION_DAY_SEQ.NEXTVAL"),
                    STREET_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GARBAGE_COLLECTION_TYPE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SCHEDULE_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    COLLECTION_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    STATUS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COLLECTION_DAY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COLLECTION_DAY_GARBAGE_COLLECTION_TYPES_GARBAGE_COLLECTION_TYPE_ID",
                        column: x => x.GARBAGE_COLLECTION_TYPE_ID,
                        principalTable: "GARBAGE_COLLECTION_TYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COLLECTION_DAY_STREETS_STREET_ID",
                        column: x => x.STREET_ID,
                        principalTable: "STREETS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COLLECTION_DAY_GARBAGE_COLLECTION_TYPE_ID",
                table: "COLLECTION_DAY",
                column: "GARBAGE_COLLECTION_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COLLECTION_DAY_STREET_ID",
                table: "COLLECTION_DAY",
                column: "STREET_ID");
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COLLECTION_DAY");
        }
    }
}
