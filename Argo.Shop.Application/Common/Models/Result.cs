// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global

namespace Argo.Shop.Application.Common.Models
{
    public enum ResultStatus
    {
        Ok = 200,
        NotFound = 404,
        Invalid = 400,
        Gone = 410,
        Error = 500,
        //Duplicate,
        Unauthorized = 401
    }

    public record Result
    {
        public ResultStatus Status { get; } = ResultStatus.Ok;

        public IList<string> Messages { get; } = new List<string>();

        protected Result()
        {
        }

        public Result(ResultStatus status)
        {
            Status = status;
        }

        public Result(ResultStatus status, params string[] messages)
        {
            Status = status;
            Messages = messages;
        }

        public static Result CreateGenericInstance(
            Type resultType,
            ResultStatus status,
            params string[] messages)
        {
            var definition = typeof(Result<>).GetGenericTypeDefinition();
            var constructed = definition.MakeGenericType(resultType);

            var result = Activator.CreateInstance(
                constructed,
                status,
                messages);

            return (Result)result!;
        }

        public static Result Ok()
        {
            return new Result(ResultStatus.Ok);
        }

        public static Result NotFound(params string[] messages)
        {
            return new Result(ResultStatus.NotFound, messages);
        }

        public static Result Invalid(params string[] messages)
        {
            return new Result(ResultStatus.Invalid, messages);
        }

        public static Result Error(params string[] messages)
        {
            return new Result(ResultStatus.Error, messages);
        }

        public static Result Unauthorized(params string[] messages)
        {
            return new Result(ResultStatus.Unauthorized, messages);
        }

        public static Result<TData> Ok<TData>(TData data)
        {
            return new Result<TData>(data);
        }

        public static Result<TData> NotFound<TData>(params string[] messages)
        {
            return new Result<TData>(ResultStatus.NotFound, messages);
        }

        public static Result<TData> Invalid<TData>(params string[] messages)
        {
            return new Result<TData>(ResultStatus.Invalid, messages);
        }

        public static Result<TData> Error<TData>(params string[] messages)
        {
            return new Result<TData>(ResultStatus.Error, messages);
        }

        public static Result<TData> Gone<TData>(params string[] messages)
        {
            return new Result<TData>(ResultStatus.Gone, messages);
        }

        public static Result<TData> Unauthorized<TData>(params string[] messages)
        {
            return new Result<TData>(ResultStatus.Unauthorized, messages);
        }
    }

    public record Result<TResult> : Result
    {
        public TResult? Data { get; }

        public Result(ResultStatus status)
            : base(status)
        {
        }

        public Result(ResultStatus status, params string[] messages)
            : base(status, messages)
        {
        }

        public Result(TResult data)
        {
            Data = data;
        }
    }
}
