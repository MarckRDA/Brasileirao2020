using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class TimesAndTabelaEstatistica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Times_Fk_Time",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Times_TimeId1",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_Times_TabelasEstatistica_TabelaId",
                table: "Times");

            migrationBuilder.DropIndex(
                name: "IX_Times_TabelaId",
                table: "Times");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TabelasEstatistica",
                table: "TabelasEstatistica");

            migrationBuilder.DropIndex(
                name: "IX_Jogadores_TimeId1",
                table: "Jogadores");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TabelasEstatistica");

            migrationBuilder.DropColumn(
                name: "TimeId1",
                table: "Jogadores");

            migrationBuilder.RenameColumn(
                name: "TabelaId",
                table: "Times",
                newName: "JogadoresId");

            migrationBuilder.RenameColumn(
                name: "Fk_Time",
                table: "Jogadores",
                newName: "TimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Jogadores_Fk_Time",
                table: "Jogadores",
                newName: "IX_Jogadores_TimeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "TimeId",
                table: "Jogadores",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TabelasEstatistica",
                table: "TabelasEstatistica",
                column: "TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Times_TimeId",
                table: "Jogadores",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TabelasEstatistica_Times_TimeId",
                table: "TabelasEstatistica",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogadores_Times_TimeId",
                table: "Jogadores");

            migrationBuilder.DropForeignKey(
                name: "FK_TabelasEstatistica_Times_TimeId",
                table: "TabelasEstatistica");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TabelasEstatistica",
                table: "TabelasEstatistica");

            migrationBuilder.RenameColumn(
                name: "JogadoresId",
                table: "Times",
                newName: "TabelaId");

            migrationBuilder.RenameColumn(
                name: "TimeId",
                table: "Jogadores",
                newName: "Fk_Time");

            migrationBuilder.RenameIndex(
                name: "IX_Jogadores_TimeId",
                table: "Jogadores",
                newName: "IX_Jogadores_Fk_Time");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TabelasEstatistica",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Fk_Time",
                table: "Jogadores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TimeId1",
                table: "Jogadores",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TabelasEstatistica",
                table: "TabelasEstatistica",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Times_TabelaId",
                table: "Times",
                column: "TabelaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_TimeId1",
                table: "Jogadores",
                column: "TimeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Times_Fk_Time",
                table: "Jogadores",
                column: "Fk_Time",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogadores_Times_TimeId1",
                table: "Jogadores",
                column: "TimeId1",
                principalTable: "Times",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Times_TabelasEstatistica_TabelaId",
                table: "Times",
                column: "TabelaId",
                principalTable: "TabelasEstatistica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
