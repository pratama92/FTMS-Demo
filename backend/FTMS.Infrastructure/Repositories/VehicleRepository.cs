using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FTMS.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid vehicleId)
        {
            return await _context.Vehicles.AnyAsync(v => v.VehicleId == vehicleId);
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(Guid? organizationId = null, bool ? isDeleted = null)
        {
            IQueryable<Vehicle> query = _context.Vehicles.AsNoTracking();

            if (organizationId.HasValue && organizationId != Guid.Empty)
            {
                query = query.Where(p => p.OrganizationId == organizationId.Value);
            }

            if (isDeleted.HasValue)
            {
                query = query.Where(p => p.IsDeleted == isDeleted.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(Guid vehicleId)
        {
            return await _context.Vehicles.Where(x => x.VehicleId == vehicleId).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetSeatCapacityAsync(Guid vehicleId)
        {
            return await _context.Vehicles.Where(x => x.VehicleId == vehicleId).Select(x => x.SeatCapacity).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsByLicensePlateAsync(string licensePlate)
        {
            return await _context.Vehicles.AnyAsync(x => x.LicensePlate == licensePlate);
        }

        public async Task<bool> ExistsByLicensePlateAsync(string licensePlate, Guid excludeVehicleId)
        {
            return await _context.Vehicles.AnyAsync(x => x.LicensePlate == licensePlate && x.VehicleId != excludeVehicleId);
        }

        public async Task<bool> ExistsByChassisNumberAsync(string chassisNumber)
        {
            return await _context.Vehicles.AnyAsync(x => x.ChassisNumber == chassisNumber);
        }

        public async Task<bool> ExistsByEngineNumberAsync(string engineNumber)
        {
            return await _context.Vehicles.AnyAsync(x => x.EngineNumber == engineNumber);
        }
    }
}
