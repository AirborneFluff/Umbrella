using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class OraganisationsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organisations_OwnedOrganisationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_AspNetUsers_OwnerId",
                table: "Organisations");

            migrationBuilder.DropTable(
                name: "OrganisationMember");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_OwnerId",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OwnedOrganisationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OwnedOrganisationId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrganisationId",
                table: "AspNetUsers",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organisations_OrganisationId",
                table: "AspNetUsers",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organisations_OrganisationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OrganisationId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "OwnedOrganisationId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrganisationMember",
                columns: table => new
                {
                    MemberId = table.Column<string>(type: "TEXT", nullable: false),
                    OrganisationId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganisationMember", x => new { x.MemberId, x.OrganisationId });
                    table.ForeignKey(
                        name: "FK_OrganisationMember_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganisationMember_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_OwnerId",
                table: "Organisations",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OwnedOrganisationId",
                table: "AspNetUsers",
                column: "OwnedOrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganisationMember_OrganisationId",
                table: "OrganisationMember",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organisations_OwnedOrganisationId",
                table: "AspNetUsers",
                column: "OwnedOrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_AspNetUsers_OwnerId",
                table: "Organisations",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
