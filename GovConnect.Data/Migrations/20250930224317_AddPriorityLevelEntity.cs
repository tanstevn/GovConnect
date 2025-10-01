using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GovConnect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPriorityLevelEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_CommentedBy",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "RequestedBy",
                table: "Requests",
                newName: "RequestedByUserId");

            migrationBuilder.AddColumn<long>(
                name: "PriorityLevelId",
                table: "Requests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "PriorityLevels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_PriorityLevelId",
                table: "Requests",
                column: "PriorityLevelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestedByUserId",
                table: "Requests",
                column: "RequestedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_CommentedBy",
                table: "Comments",
                column: "CommentedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_PriorityLevels_PriorityLevelId",
                table: "Requests",
                column: "PriorityLevelId",
                principalTable: "PriorityLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_RequestedByUserId",
                table: "Requests",
                column: "RequestedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_CommentedBy",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_PriorityLevels_PriorityLevelId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_RequestedByUserId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "PriorityLevels");

            migrationBuilder.DropIndex(
                name: "IX_Requests_PriorityLevelId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestedByUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "PriorityLevelId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "RequestedByUserId",
                table: "Requests",
                newName: "RequestedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_CommentedBy",
                table: "Comments",
                column: "CommentedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
