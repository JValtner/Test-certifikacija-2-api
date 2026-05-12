using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam.App.Migrations
{
    /// <inheritdoc />
    public partial class SkilsRefferenceFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjectSkills_Projects_ProjectId1",
                table: "UserProjectSkills");

            migrationBuilder.DropIndex(
                name: "IX_UserProjectSkills_ProjectId1",
                table: "UserProjectSkills");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "UserProjectSkills");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                table: "UserProjectSkills",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectSkills_ProjectId1",
                table: "UserProjectSkills",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjectSkills_Projects_ProjectId1",
                table: "UserProjectSkills",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
