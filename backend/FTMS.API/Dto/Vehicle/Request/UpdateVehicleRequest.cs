namespace FTMS.API.Dto.Vehicle.Request
{
    public class UpdateVehicleRequest
    {
        public string VehicleCode { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int SeatCapacity { get; set; }
        public decimal CargoCapacity { get; set; }
    }
}
