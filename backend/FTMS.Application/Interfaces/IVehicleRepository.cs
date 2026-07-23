using FTMS.Domain.Entities;

namespace FTMS.Application.Interfaces
{
    public interface IVehicleRepository
    {
        Task AddAsync(Vehicle vehicle);
        Task<Vehicle?> GetByIdAsync(Guid vehicleId);
        Task<IEnumerable<Vehicle>> GetAllAsync(Guid? organizationId = null, bool ? isDeleted = null);
        Task UpdateAsync(Vehicle vehicle);
        Task<bool> ExistsAsync(Guid vehicleId);
        Task<bool> ExistsByLicensePlateAsync(string licensePlate);
        Task<bool> ExistsByLicensePlateAsync(string licensePlate, Guid excludeVehicleId);
        Task<bool> ExistsByChassisNumberAsync(string chassisNumber);
        Task<bool> ExistsByEngineNumberAsync(string engineNumber);
        Task<int> GetSeatCapacityAsync(Guid vehicelId);
    }
}
