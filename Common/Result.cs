using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
namespace Common;

public class Result<TError> where TError : class
{
    [JsonIgnore]
    public bool Success { get; }

    [JsonIgnore]
    [MemberNotNullWhen(false, nameof(Success))]
    public TError Error { get; }

    public Result()
    {
        Success = true;
        Error = null!;
    }

    public Result(TError error)
    {
        Success = false;
        Error = error;
    }
}

public class Result<TValue, TError> where TValue : class where TError : class
{
    [JsonIgnore]
    public bool Success { get; }

    [JsonIgnore]
    [MemberNotNullWhen(true, nameof(Success))]
    public TValue? Value { get; }

    [JsonIgnore]
    [MemberNotNullWhen(false, nameof(Success))]
    public TError Error { get; }

    public Result(TValue value)
    {
        Success = true;
        Value = value;
        Error = null!;
    }

    public Result(TError error)
    {
        Success = false;
        Error = error;
    }
}
