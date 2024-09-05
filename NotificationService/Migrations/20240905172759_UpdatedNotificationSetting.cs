using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedNotificationSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReservationResponseNotificationEnabled",
                table: "NotificationSettings",
                newName: "IsReservationRejectedNotificationEnabled");

            migrationBuilder.RenameColumn(
                name: "IsReservationCancellationNotificationEnabled",
                table: "NotificationSettings",
                newName: "IsReservationDeletedNotificationEnabled");

            migrationBuilder.AddColumn<bool>(
                name: "IsReservationCancelledNotificationEnabled",
                table: "NotificationSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReservationConfirmedNotificationEnabled",
                table: "NotificationSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReservationCancelledNotificationEnabled",
                table: "NotificationSettings");

            migrationBuilder.DropColumn(
                name: "IsReservationConfirmedNotificationEnabled",
                table: "NotificationSettings");

            migrationBuilder.RenameColumn(
                name: "IsReservationRejectedNotificationEnabled",
                table: "NotificationSettings",
                newName: "IsReservationResponseNotificationEnabled");

            migrationBuilder.RenameColumn(
                name: "IsReservationDeletedNotificationEnabled",
                table: "NotificationSettings",
                newName: "IsReservationCancellationNotificationEnabled");
        }
    }
}
