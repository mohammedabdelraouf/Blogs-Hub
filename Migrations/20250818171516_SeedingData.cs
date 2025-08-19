using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogsAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "Email", "JoinDate", "Name" },
                values: new object[,]
                {
                    { 1, "Tech enthusiast and blogger.", "ahmed@example.com", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ahmed Youssef" },
                    { 2, "Writes about lifestyle.", "sara@example.com", new DateTime(2023, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sara El-Masry" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedDate", "Title", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, "A beginner's guide to Docker.", new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Getting Started with Docker", new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, "Tips for a balanced lifestyle.", new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Healthy Habits", new DateTime(2023, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedDate", "PostId" },
                values: new object[,]
                {
                    { 1, 2, "Great article, very helpful!", new DateTime(2023, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 1, "I love these tips, thanks!", new DateTime(2023, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
