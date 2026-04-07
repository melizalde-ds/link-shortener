using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace link_shortner.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkTableAndSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "LinkSequence",
                startValue: 0L,
                minValue: 0L,
                maxValue: 9223372036854775807L);

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    OriginalUrl = table.Column<string>(type: "text", nullable: false),
                    ShortUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_ShortUrl",
                table: "Links",
                column: "ShortUrl",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropSequence(
                name: "LinkSequence");
        }
    }
}
