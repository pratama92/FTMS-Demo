using FTMS.Domain.Enums;
using System.ComponentModel;

namespace FTMS.Application.UseCases.VehicleManagement.CreateVehicle
{
    public class CreateVehicleRequest
    {
        public string VehicleCode { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string ChassisNumber { get; set; } = string.Empty;
        public string EngineNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Color { get; set; } = string.Empty;

        [DefaultValue(VehicleTypeEnum.MPV)]
        public VehicleTypeEnum VehicleType { get; set; }

        [DefaultValue(FuelTypeEnum.Gasoline)]
        public FuelTypeEnum FuelType { get; set; }

        [DefaultValue(DrivetrainEnum.FWD)]
        public DrivetrainEnum Drivetrain { get; set; }

        [DefaultValue(TransmissionTypeEnum.Automatic)]
        public TransmissionTypeEnum Transmission { get; set; }
        public int SeatCapacity { get; set; }
        public decimal CargoCapacity { get; set; }
    }
}
