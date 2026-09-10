using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using Utf8StringInterpolation;

namespace NetSonar.Avalonia.Extensions;

public partial class ObservableValidatorExtended : ObservableValidator
{
    [ObservableProperty]
    public partial string ValidationErrors { get; protected set; } = string.Empty;

    protected List<string> CustomErrors { get; } = [];

    public new bool HasErrors => base.HasErrors || CustomErrors.Count > 0;

    /// <summary>
    /// Validates the object and returns a string with all the errors.
    /// </summary>
    /// <returns></returns>
    protected virtual bool ValidateInternal()
    {
        return CustomErrors.Count == 0;
    }

    /// <summary>
    /// Validates the object.
    /// </summary>
    /// <returns></returns>
    public bool Validate()
    {
        ValidateAllProperties();
        CustomErrors.Clear();
        var internalValidation = ValidateInternal();
        if (HasErrors || !internalValidation)
        {
            OnPropertyChanged(nameof(HasErrors));
            ValidationErrors = GetErrorsFormatted();
            return false;
        }
        else
        {
            ValidationErrors = string.Empty;
            return true;
        }
    }

    public string GetErrorsRaw()
    {
        if (!HasErrors) return string.Empty;
        using var writer = Utf8String.CreateWriter(out var zsb);
        foreach (var error in GetErrors())
        {
            if (string.IsNullOrWhiteSpace(error.ErrorMessage)) continue;
            zsb.AppendLine(error.ErrorMessage);
        }
        foreach (var error in CustomErrors)
        {
            if (string.IsNullOrWhiteSpace(error)) continue;
            zsb.AppendLine(error);
        }

        zsb.Flush();
        return writer.ToString().Trim();
    }

    public string GetErrorsFormatted()
    {
        if (!HasErrors) return string.Empty;
        using var writer = Utf8String.CreateWriter(out var zsb);
        foreach (var error in GetErrors())
        {
            if (string.IsNullOrWhiteSpace(error.ErrorMessage)) continue;
            zsb.AppendLine($"- {error.ErrorMessage}");
        }
        foreach (var error in CustomErrors)
        {
            if (string.IsNullOrWhiteSpace(error)) continue;
            zsb.AppendLine($"- {error}");
        }

        zsb.Flush();
        return writer.ToString().Trim();
    }
}