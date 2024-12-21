using Microsoft.AspNetCore.Mvc;

namespace Backend.Services;

public abstract class ServiceBase
{
    protected ServiceResult Success() => new();
    protected ServiceResult<T> Success<T>(T value) where T : class => new(value);
    protected ServiceResult Failure(IActionResult errorResponse) => new(errorResponse);
    protected ServiceResult<T> Failure<T>(IActionResult errorResponse) where T : class => new(errorResponse);
}