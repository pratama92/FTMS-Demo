using FTMS.Domain.Enums;

namespace FTMS.API.Dto.Vehicle.Request
{
    public class ChangeVehicleStatusRequest
    {
        public VehicleStatusEnum VehicleStatus { get; set; }
    }
}
