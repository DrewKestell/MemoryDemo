namespace VesperAPI.Services
{
    public class OperationResult<T>(OperationStatusCode statusCode, T? value = null, string? error = null) where T : class
    {
        public OperationStatusCode StatusCode { get; } = statusCode;

        public T? Value { get; } = value;

        public string? Error { get; } = error;
    }

    public class OperationResult(OperationStatusCode statusCode, string? error = null)
    {
        public OperationStatusCode StatusCode { get; } = statusCode;

        public string? Error { get; } = error;
    }
}
