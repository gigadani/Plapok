using Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Backend;

public class ServiceResult : Result<IActionResult> 
{
    public ServiceResult() : base() { }
    public ServiceResult(IActionResult error) : base(error) { }
}

public class ServiceResult<T> : Result<T, IActionResult> where T : class
{
    public ServiceResult(T value) : base(value) { }
    public ServiceResult(IActionResult error) : base(error) { }
}

public static class ServiceResultExtensions
{
    public static bool Succeeded(this ServiceResult result) => result.Success;
    public static bool Succeeded<T>(this ServiceResult<T> result) where T : class => result.Success;
    public static bool Failed(this ServiceResult result) => !result.Success;
    public static bool Failed<T>(this ServiceResult<T> result) where T : class => !result.Success;

    public static bool Succeeded(this ServiceResult result, out IActionResult error)
    {
        error = result.Error;
        return result.Success;
    }

    public static bool Succeeded<T>(this ServiceResult<T> result, out T value, out IActionResult error) where T : class
    {
        error = result.Error;
        value = result.Value!;
        return result.Success;
    }
}
