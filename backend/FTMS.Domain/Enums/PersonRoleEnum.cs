namespace FTMS.Domain.Enums
{
    /// <summary>
    /// Represents business capabilities of a Person.
    /// Uses Flags to allow multiple roles on one person.
    /// Each role must use a unique power of two value.
    /// Future roles should use the next values: 4, 8, 16, 32, and so on.
    /// </summary>

    [Flags]
    public enum PersonRoleEnum
    {

        /// <summary>
        /// Can participate as a passenger.
        /// </summary>
        Passenger = 1,

        /// <summary>
        /// Can operate a vehicle.
        /// </summary>
        Driver = 2,
    }
}
