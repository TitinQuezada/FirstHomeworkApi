namespace Core.Contracts
{
    public interface IOperationResult<T>
    {
        bool Success { get; }

        string Message { get; }

        string MessageDetail { get; }

        T Entity { get; }
    }
}
