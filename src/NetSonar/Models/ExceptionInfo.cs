using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace NetSonar.Avalonia.Models;

public sealed record ExceptionInfo
{
    public string Type { get; init; } = string.Empty;
    public required string Message { get; init; } = string.Empty;
    public string Source { get; init; } = string.Empty;
    public string StackTrace { get; init; } = string.Empty;
    public ExceptionInfo? InnerException { get; init; }

    public ExceptionInfo() { }

    [SetsRequiredMembers]
    public ExceptionInfo(Exception exception, bool includeInnerException = true, bool includeStackTrace = true, bool handleAggregateExceptionAsLinkedLink = true)
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (handleAggregateExceptionAsLinkedLink && exception is AggregateException { InnerExceptions.Count: > 0 } aggregateException)
        {
            Type = aggregateException.InnerExceptions[0].GetType().FullName ?? string.Empty;
            Message = aggregateException.InnerExceptions[0].Message;
            Source = aggregateException.InnerExceptions[0].Source ?? string.Empty;
            StackTrace = includeStackTrace ? aggregateException.InnerExceptions[0].StackTrace ?? string.Empty : string.Empty;

            if (aggregateException.InnerExceptions.Count >= 2)
            {
                InnerException = new ExceptionInfo(new AggregateException(aggregateException.InnerExceptions.Skip(1)), includeInnerException, includeStackTrace);
            }
        }
        else
        {
            Type = exception.GetType().FullName ?? string.Empty;
            Message = exception.Message;
            Source = exception.Source ?? string.Empty;
            StackTrace = includeStackTrace ? exception.StackTrace ?? string.Empty : string.Empty;
            if (includeInnerException && exception.InnerException is not null)
            {
                InnerException = new ExceptionInfo(exception.InnerException, includeInnerException, includeStackTrace);
            }
        }

    }


    public IEnumerable<ExceptionInfo> TraverseExceptions()
    {
        var exception = this;
        do
        {
            yield return exception;
            exception = exception.InnerException;
        } while (exception is not null);
    }
}