using System;
using System.Collections.Generic;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action) {
        foreach (var element in source)
            action(element);

        return source;
    }
}