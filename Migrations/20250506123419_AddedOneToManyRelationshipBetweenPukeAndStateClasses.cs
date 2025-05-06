using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PukesMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddedOneToManyRelationshipBetweenPukeAndStateClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StateId",
                table: "Pukes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Pukes_StateId",
                table: "Pukes",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pukes_States_StateId",
                table: "Pukes",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pukes_States_StateId",
                table: "Pukes");

            migrationBuilder.DropIndex(
                name: "IX_Pukes_StateId",
                table: "Pukes");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Pukes");
        }
    }
}
