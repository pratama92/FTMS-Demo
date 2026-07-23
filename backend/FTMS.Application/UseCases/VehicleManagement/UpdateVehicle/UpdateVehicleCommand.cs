namespace FTMS.Application.UseCases.VehicleManagement.UpdateVehicle
{
    public class UpdateVehicleCommand
    {
        public Guid VehicleId { get; set; }
        public string VehicleCode { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int SeatCapacity { get; set; }
        public decimal CargoCapacity { get; set; }
    }
}
