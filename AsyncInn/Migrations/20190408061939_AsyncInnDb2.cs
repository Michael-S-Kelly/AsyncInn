using Microsoft.EntityFrameworkCore.Migrations;

namespace AsyncInn.Migrations
{
    public partial class AsyncInnDb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoomAmenities_AmenitiesID",
                table: "RoomAmenities");

            migrationBuilder.DropIndex(
                name: "IX_RoomAmenities_RoomID",
                table: "RoomAmenities");

            migrationBuilder.DropIndex(
                name: "IX_HotelRoom_RoomID",
                table: "HotelRoom");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_AmenitiesID",
                table: "RoomAmenities",
                column: "AmenitiesID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoom_RoomID",
                table: "HotelRoom",
                column: "RoomID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoomAmenities_AmenitiesID",
                table: "RoomAmenities");

            migrationBuilder.DropIndex(
                name: "IX_HotelRoom_RoomID",
                table: "HotelRoom");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_AmenitiesID",
                table: "RoomAmenities",
                column: "AmenitiesID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomAmenities_RoomID",
                table: "RoomAmenities",
                column: "RoomID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HotelRoom_RoomID",
                table: "HotelRoom",
                column: "RoomID",
                unique: true);
        }
    }
}
