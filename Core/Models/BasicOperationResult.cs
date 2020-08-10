using Core.Contracts;

namespace Core.Models
{
    public sealed class BasicOperationResult<T> : IOperationResult<T>
    {
        private BasicOperationResult(string message, bool success, T entity, string detail)
        {
            Message = message;
            Success = success;
            Entity = entity;
            MessageDetail = detail;
        }

        public string Message { get; }

        public string MessageDetail { get; }

        public bool Success { get; }

        public T Entity { get; }

        public static BasicOperationResult<T> Ok()
            => new BasicOperationResult<T>("", true, default(T), "");

        public static BasicOperationResult<T> Ok(T entity)
            => new BasicOperationResult<T>("", true, entity, "");

        public static BasicOperationResult<T> Fail(string message)
            => new BasicOperationResult<T>(message, false, default(T), "");

        public static BasicOperationResult<T> Fail(string message, string detail)
         => new BasicOperationResult<T>(message, false, default(T), detail);
        public static BasicOperationResult<T> Fail(string message, T entity)
         => new BasicOperationResult<T>(message, false, entity, "");
    }
}
