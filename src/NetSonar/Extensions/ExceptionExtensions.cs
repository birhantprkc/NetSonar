using Cysharp.Diagnostics;
using System;

namespace NetSonar.Avalonia.Extensions;

public static class ExceptionExtensions
{
    public static string GetOutputError(this ProcessErrorException ex)
    {
        return ex.ErrorOutput.Length == 0 
            ? string.Empty 
            : string.Join(Environment.NewLine, ex.ErrorOutput);
    }
}