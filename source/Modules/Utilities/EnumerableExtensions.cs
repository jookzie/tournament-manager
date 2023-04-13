namespace Modules.Utilities
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Filters a sequence based on whether its elements' <typeparamref name="string"/> 
        /// properties contain the given <paramref name="keyword"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="keyword"></param>
        /// <returns>An <typeparamref name="IEnumerable"/> of type <typeparamref name="T"/>.</returns>
        public static IEnumerable<T> FilterByKeyword<T>(this IEnumerable<T> source, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return source;
            var filteredEnumerable = source
                .Where(item => item.ToString().IndexOf(keyword, StringComparison.InvariantCultureIgnoreCase) != -1);
            if(filteredEnumerable.Count() != 0)
                return filteredEnumerable;
            var properties = typeof(T).GetProperties();
            return source.Where(item =>
                    properties.Any(property =>
                        property.PropertyType == typeof(string) &&
                        property.GetValue(item)
                            .ToString()
                            .IndexOf(keyword, StringComparison.InvariantCultureIgnoreCase) != -1));
        }
        /// <summary>
        /// Filters the elements of a sequence that occur more than once.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>
        ///     A dictionary of elements of type <typeparamref name="T"/> and
        ///     <paramref name="int"/> number of times they occur.
        /// </returns>
        public static IDictionary<T, int> FindDuplicates<T>(this IEnumerable<T> source)
        {
            return source
                .GroupBy(source => source)
                .Where(g => g.Count() > 1)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
    }
}
