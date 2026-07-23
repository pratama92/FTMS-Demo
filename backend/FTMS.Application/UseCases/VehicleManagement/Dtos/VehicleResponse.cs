namespace FTMS.Application.UseCases.VehicleManagement.Dtos
{
    public class VehicleResponse
    {
        public Guid VehicleId { get; set; }
        public string VehicleCode { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string ChassisNumber { get; set; } = string.Empty;
        public string EngineNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string FuelType { get; set; } = string.Empty;
        public string Drivetrain { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public int SeatCapacity { get; set; }
        public decimal CargoCapacity { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid? OrganizationId { get; set; }
    }
}
