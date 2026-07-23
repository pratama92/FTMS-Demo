namespace FTMS.Application.Common
{
    public class ErrorResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public string? Code { get; set; }
    }
}
