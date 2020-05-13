using CMDParser.Internals.Options;
using System;
using System.Linq;

namespace CMDParser
{
	/// <summary>
	/// A factory class for the <see cref="Option"/> inheritors.
	/// </summary>
	public static class OptionFactory
	{
		/// <summary>
		/// Creates a short <see cref="Option"/> with given <paramref name="identifier"/>.
		/// </summary>
		/// <exception cref="ArgumentException">Thrown when option <paramref name="identifier"/> is whitespace.</exception>
		public static Option Short(char identifier)
		{
			if (char.IsWhiteSpace(identifier))
				throw new ArgumentException("Name of the option cannot be empty.");

			return new ShortOption(identifier.ToString());
		}

		/// <summary>
		/// Creates a long <see cref="Option"/> with given <paramref name="identifier"/>.
		/// </summary>
		/// <exception cref="ArgumentException">Thrown when option <paramref name="identifier"/> is whitespace, 
		/// or it contains some whitespace.</exception>
		public static Option Long(string identifier)
		{
			if (string.IsNullOrWhiteSpace(identifier))
				throw new ArgumentException("Name of the option cannot be empty.");

			if (identifier.Any(char.IsWhiteSpace))
				throw new ArgumentException("Name of the option cannot contain any whitespace.");

			return new LongOption(identifier);
		}
	}
}
