using System.Collections.Generic;

namespace NetSpec {
    internal static class LinqExtensions {
        internal static List<T> Reversed<T>(this List<T> enumerable) {
            enumerable.Reverse();
            return enumerable;
        }
    }
}