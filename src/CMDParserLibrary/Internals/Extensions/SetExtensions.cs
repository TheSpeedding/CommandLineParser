using System.Collections.Generic;

namespace CMDParser.Internals.Extensions
{
	/// <summary>
	/// Convenience methods for <see cref="ISet{T}"/>.
	/// </summary>
	internal static class SetExtensions
	{
		/// <summary>
		/// Adds all <paramref name="items"/> to the <paramref name="set"/> and
		/// returns <see langword="false"/> if some item was not added; otherwise
		/// returns <see langword="true"/>.
		/// </summary>
		public static bool AddAll<T>(this ISet<T> set, IEnumerable<T> items)
		{
			var notAllAdded = false;

			foreach (var item in items)
				notAllAdded |= !set.Add(item);

			return !notAllAdded;
		}

		/// <summary>
		/// Removes all <paramref name="items"/> to the <paramref name="set"/> and
		/// returns <see langword="false"/> if some item was not removed; otherwise
		/// returns <see langword="true"/>.
		/// </summary>
		public static bool RemoveAll<T>(this ISet<T> set, IEnumerable<T> items)
		{
			var notAllRemoved = false;

			foreach (var item in items)
				notAllRemoved |= !set.Remove(item);

			return !notAllRemoved;
		}
	}
}
