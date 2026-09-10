using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ObservableCollections;
using ZLinq;

namespace NetSonar.Avalonia.Extensions;

public static class CollectionsExtensions
{
    /*public static void AddRange<T>(this IList<T> list, IEnumerable other)
    {
        foreach (var item in other)
        {
            if (item is T tItem) list.Add(tItem);
        }
    }*/

    public static void RemoveRange<T>(this IList<T> list, IEnumerable other)
    {
        var enumerable = other as object[] ?? other.AsValueEnumerable().ToArray();
        foreach (var item in enumerable)
        {
            if (item is T tItem) list.Remove(tItem);
        }
    }

    /// <summary>
    /// Ensure the collection have at most <paramref name="maxItemCount"/> items, exceeding items will be removed from the head/tail of the collection.
    /// </summary>
    /// <typeparam name="T">Collection type</typeparam>
    /// <param name="collection">Collection to restrict the number of items for</param>
    /// <param name="maxItemCount">The maximum number of items to keep in this <paramref name="collection"/>.</param>
    /// <param name="side"></param>
    public static void RemoveExceedingAt<T>(this List<T> collection, int maxItemCount, CollectionSide side)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxItemCount);

        var exceed = collection.Count - maxItemCount;
        if (exceed <= 0) return;

        switch (side)
        {
            case CollectionSide.Head:
                collection.RemoveRange(0, exceed);
                break;
            case CollectionSide.Tail:
                collection.RemoveRange(maxItemCount, exceed);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(side), side, null);
        }
    }

    /// <summary>
    /// Ensure the collection have at most <paramref name="maxItemCount"/> items, exceeding items will be removed from the head/tail of the collection.
    /// </summary>
    /// <typeparam name="T">Collection type</typeparam>
    /// <param name="collection">Collection to restrict the number of items for</param>
    /// <param name="maxItemCount">The maximum number of items to keep in this <paramref name="collection"/>.</param>
    /// <param name="side"></param>
    public static void RemoveExceedingAt<T>(this ObservableList<T> collection, int maxItemCount, CollectionSide side)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxItemCount);

        var exceed = collection.Count - maxItemCount;
        if (exceed <= 0) return;

        switch (side)
        {
            case CollectionSide.Head:
                collection.RemoveRange(0, exceed);
                break;
            case CollectionSide.Tail:
                collection.RemoveRange(maxItemCount, exceed);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(side), side, null);
        }
    }

    public static bool HasSingleItem<T>(this IEnumerable<T> sequence)
    {
        if (sequence is ICollection<T> collection)
        {
            return collection.Count == 1;
        }

        using var iter = sequence.GetEnumerator();
        return iter.MoveNext() && !iter.MoveNext();
    }

    public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueFactory)
        where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        ArgumentNullException.ThrowIfNull(valueFactory);

        ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, key, out var exists);
        if (!exists)
        {
            value = valueFactory(key);
        }

        return value!;
    }
}