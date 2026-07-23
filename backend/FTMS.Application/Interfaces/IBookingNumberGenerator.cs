namespace FTMS.Application.Interfaces
{
    public interface IBookingNumberGenerator
    {
        Task<string> GenerateAsync();
    }
}
