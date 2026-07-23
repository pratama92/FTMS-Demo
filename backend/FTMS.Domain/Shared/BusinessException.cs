namespace FTMS.Domain.Shared
{
    public class BusinessException : Exception
    {
        public string Code { get; }

        public BusinessException(
            string message,
            string code = "BUSINESS_ERROR")
            : base(message)
        {
            Code = code;
        }
    }

    public class NotFoundException : BusinessException
    {
        public NotFoundException(string message, string code = "NOT_FOUND")
            : base(message, code)
        {
        }
    }

}
