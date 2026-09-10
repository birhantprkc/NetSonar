namespace NetSonar.Avalonia.Cache;

public sealed class PropertyCache
{
    /// <summary>
    /// Gets or sets the name of the property.
    /// </summary>
    public required string PropertyName { get; init; }

    /// <summary>
    /// Gets or sets the initial value of the property.
    /// </summary>
    public object? InitialValue { get; set; }

    /// <summary>
    /// Gets or sets the current value of the property.
    /// </summary>
    public object? CurrentValue { get; set; }

    /// <summary>
    /// Gets a value indicating whether the property has changed.
    /// </summary>
    public bool Changed => !Equals(InitialValue, CurrentValue);

    public override string ToString()
    {
        return
            $"{nameof(PropertyName)}: {PropertyName}, {nameof(InitialValue)}: {InitialValue}, {nameof(CurrentValue)}: {CurrentValue}";
    }
}