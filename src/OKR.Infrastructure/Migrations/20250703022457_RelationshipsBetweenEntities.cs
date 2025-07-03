using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OKR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipsBetweenEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_KeyResults_ObjectiveId",
                table: "KeyResults",
                column: "ObjectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ActionId",
                table: "Feedbacks",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Actions_KeyResultId",
                table: "Actions",
                column: "KeyResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actions_KeyResults_KeyResultId",
                table: "Actions",
                column: "KeyResultId",
                principalTable: "KeyResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Actions_ActionId",
                table: "Feedbacks",
                column: "ActionId",
                principalTable: "Actions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyResults_Objectives_ObjectiveId",
                table: "KeyResults",
                column: "ObjectiveId",
                principalTable: "Objectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actions_KeyResults_KeyResultId",
                table: "Actions");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Actions_ActionId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyResults_Objectives_ObjectiveId",
                table: "KeyResults");

            migrationBuilder.DropIndex(
                name: "IX_KeyResults_ObjectiveId",
                table: "KeyResults");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ActionId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Actions_KeyResultId",
                table: "Actions");
        }
    }
}
