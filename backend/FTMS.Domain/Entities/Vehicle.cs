using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Domain.Entities
{
    public class Vehicle
    {
        public Guid VehicleId { get; private set; }
        public string VehicleCode { get; private set; } = string.Empty;
        // Nomor Polisi
        public string LicensePlate { get; private set; } = string.Empty;
        // VIN / Nomor Rangka
        public string ChassisNumber { get; private set; } = string.Empty;
        // Nomor Mesin
        public string EngineNumber { get; private set; } = string.Empty;
        public string Brand { get; private set; } = string.Empty;
        public string Model { get; private set; } = string.Empty;
        public int Year { get; private set; }
        public string Color { get; private set; } = string.Empty;
        public VehicleTypeEnum VehicleType { get; private set; }
        public FuelTypeEnum FuelType { get; private set; }
        public DrivetrainEnum Drivetrain { get; private set; }
        public TransmissionTypeEnum Transmission { get; private set; }
        public int SeatCapacity { get; private set; }
        public decimal CargoCapacity { get; private set; }
        public VehicleStatusEnum Status { get; private set; }
        public Guid OrganizationId { get; private set; }
        public Organization Organization { get; private set; } = null!;
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }

        private Vehicle()
        {
        }

        public static Vehicle Create(
            string vehicleCode,
            string licensePlate,
            string chassisNumber,
            string engineNumber,
            string brand,
            string model,
            int year,
            string color,
            int seatCapacity,
            decimal cargoCapacity,
            VehicleTypeEnum vehicleType,
            TransmissionTypeEnum transmission,
            DrivetrainEnum drivetrain,
            FuelTypeEnum fuelType,
            Guid organizationId)
        {
            if (string.IsNullOrWhiteSpace(vehicleCode))
                throw new BusinessException("Vehicle code is required.", ErrorCodes.VehicleCodeRequired);

            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new BusinessException("License plate is required.", ErrorCodes.VehicleLicenseRequired);

            if (string.IsNullOrWhiteSpace(chassisNumber))
                throw new BusinessException("Chasis number is required.", ErrorCodes.VehicleChassisNumberRequired);

            if (string.IsNullOrWhiteSpace(engineNumber))
                throw new BusinessException("Engine number is required.", ErrorCodes.VehicleEngineNumberRequired);

            if (seatCapacity <= 0)
                throw new BusinessException("Seat Capacity must be greater than zero.", ErrorCodes.VehicleSeatInvalid);

            if (year > DateTime.UtcNow.Year)
                throw new BusinessException("Year cannot be in the future.", ErrorCodes.VehicleYearInvalid);

            if (organizationId == Guid.Empty)
                throw new BusinessException("Organization is required.", ErrorCodes.OrganizationRequired);

            if (string.IsNullOrWhiteSpace(brand))
                throw new BusinessException("Brand is required.", ErrorCodes.VehicleBrandRequired);

            if (string.IsNullOrWhiteSpace(model))
                throw new BusinessException("Model is required.", ErrorCodes.VehicleModelRequired);

            if (string.IsNullOrWhiteSpace(color))
                throw new BusinessException("Color is required.", ErrorCodes.VehicleColorRequired);

            if (cargoCapacity < 0)
                throw new BusinessException("Cargo capacity cannot be negative.", ErrorCodes.VehicleCargoCapacityInvalid);

            return new Vehicle
            {
                VehicleId = Guid.NewGuid(),
                VehicleCode = vehicleCode.Trim().ToUpper(),
                LicensePlate = licensePlate.Trim().ToUpper(),
                ChassisNumber = chassisNumber.Trim(),
                EngineNumber = engineNumber.Trim(),
                Brand = brand.Trim(),
                Model = model.Trim(),
                Year = year,
                Color = color.Trim(),
                SeatCapacity = seatCapacity,
                CargoCapacity = cargoCapacity,
                OrganizationId = organizationId,
                Status = VehicleStatusEnum.Available,
                VehicleType = vehicleType,
                FuelType = fuelType,
                Drivetrain = drivetrain,
                Transmission = transmission,
                IsDeleted = false,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };
        }

        public void UpdateInformation(
            string vehicleCode,
            string licensePlate,
            string color,
            int seatCapacity,
            decimal cargoCapacity)
        {

            EnsureNotDeleted();

            if (string.IsNullOrWhiteSpace(vehicleCode))
                throw new BusinessException("Vehicle code is required.", ErrorCodes.VehicleCodeRequired);

            if (string.IsNullOrWhiteSpace(licensePlate))
                throw new BusinessException("License plate is required.", ErrorCodes.VehicleLicenseRequired);

            if (seatCapacity <= 0)
                throw new BusinessException("Capacity must be greater than zero.", ErrorCodes.VehicleSeatInvalid);

            if (cargoCapacity < 0)
                throw new BusinessException("Cargo capacity cannot be negative.", ErrorCodes.VehicleCargoCapacityInvalid);

            if (string.IsNullOrWhiteSpace(color))
                throw new BusinessException("Color is required.", ErrorCodes.VehicleColorRequired);

            VehicleCode = vehicleCode.Trim().ToUpper();
            LicensePlate = licensePlate.Trim().ToUpper();
            Color = color.Trim();
            SeatCapacity = seatCapacity;
            CargoCapacity = cargoCapacity;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Activate()
        {
            EnsureNotDeleted();

            Status = VehicleStatusEnum.Available;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void SetMaintenance()
        {
            EnsureNotDeleted();

            Status = VehicleStatusEnum.Maintenance;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Retire()
        {
            EnsureNotDeleted();

            Status = VehicleStatusEnum.Retired;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void Delete()
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeletedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void EnsureAvailable()
        {
            if (Status != VehicleStatusEnum.Available)
            {
                throw new BusinessException("Vehicle is unavailable.", ErrorCodes.VehicleUnAvailable);
            }
        }

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
                throw new BusinessException("Vehicle has been deleted.", ErrorCodes.VehicleDeleted);
        }


    }
}