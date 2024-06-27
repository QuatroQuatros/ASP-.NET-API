using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoDeResiduos.Migrations
{
    /// <inheritdoc />
    public partial class AddEstadoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "ESTADOS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValueSql: "ESTADOS_SEQ.NEXTVAL",
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldDefaultValueSql: "ESTADOS_SEQ.NEXTVAL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "ESTADOS",
                type: "NUMBER(10)",
                nullable: false,
                defaultValueSql: "ESTADOS_SEQ.NEXTVAL",
                oldClrType: typeof(int),
                oldType: "NUMBER(10)",
                oldDefaultValueSql: "ESTADOS_SEQ.NEXTVAL");
        }
    }
}
