// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global

using Argo.Shop.Domain.Common.Extensions;

namespace Argo.Shop.Application.Features
{
    public enum ResultStatus
    {
        Success = 1,
        NotFound = 2,
        Invalid = 3,
        Gone = 4,
        Error = 5
        //Duplicate,
        //Unauthorized,
    }

    public record Result
    {
        public ResultStatus Status { get; } = ResultStatus.Success;
        
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
        
        public static Result Success()
        {
            return new Result(ResultStatus.Success);
        }

        public static Result NotFound()
        {
            return new Result(ResultStatus.NotFound);
        }

        public static Result Invalid(params string[] messages)
        {
            return new Result(ResultStatus.Invalid, messages);
        }

        public static Result Error(IEnumerable<string> messages)
        {
            return new Result(ResultStatus.Error, messages.ToArray());
        }

        public static Result Error(params string[] messages)
        {
            return new Result(ResultStatus.Error, messages);
        }

        public static Result<TData> Success<TData>(TData data)
        {
            return new Result<TData>(data);
        }

        public static Result<TData> NotFound<TData>()
        {
            return new Result<TData>(ResultStatus.NotFound);
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

    public static class ResultExtensions
    {
        public static bool IsOfTypeResult(this Type type)
        {
            return type.IsNonGenericResultType() || type.IsGenericResultType();
        }

        public static bool IsNonGenericResultType(this Type type)
        {
            return type.IsAssignableTo(typeof(Result));
        }

        public static bool IsGenericResultType(this Type type)
        {
            return type.IsAssignableToGenericType(typeof(Result<>));
        }
    }
}
