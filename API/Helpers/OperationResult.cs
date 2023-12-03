namespace API.Helpers;

public class OperationResult
{
    public object? Value { get; set; }
    
    public bool Success { get; protected set; }
    public bool Failure => !this.Success;

    public Exception? Exception { get; protected set; }
    public string? FailureMessage { get; protected set; }
    public string? ExceptionMessage { get => Exception?.Message; }

    public static OperationResult SuccessResult(object? value)
    {
        return new OperationResult
        {
            Success = true,
            Value = value
        };
    }
    
    public static OperationResult SuccessResult()
    {
        return new OperationResult
        {
            Success = true
        };
    }
    
    public static OperationResult FailureResult(string message)
    {
        return new OperationResult
        {
            Success = false,
            FailureMessage = message
        };
    }
    
    public static OperationResult ExceptionResult(Exception ex)
    {
        return new OperationResult
        {
            Success = false,
            Exception = ex
        };
    }
}