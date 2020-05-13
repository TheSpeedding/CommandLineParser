using CMDParser.Internals;
using CMDParser.Internals.Options;
using System;

namespace CMDParser
{
	/// <summary>
	/// A common class for the options.
	/// </summary>
	/// <remarks>
	/// Extends <see cref="IOption"/> with common abstract constructor
	/// and implementation of <see cref="IEquatable{T}"/>. Also explicitly
	/// implements <see cref="IParsable"/> (we don't want to see those methods
	/// from public API).
	/// </remarks>
	public abstract class Option : IOption, IEquatable<Option>
	{
		/// <inheritdoc/>
		public abstract string Prefix { get; }

		/// <inheritdoc/>
		public string Identifier { get; }

		/// <summary>
		/// Creates a new <see cref="Option"/> instance.
		/// </summary>
		/// <param name="id">Identifier of the option.</param>
		protected Option(string id)
		{
			Identifier = id;
		}

		/// <inheritdoc/>
		bool IParsable.TryParse(InputProcessor input)
		{
			if (input.CurrentToken == Prefix + Identifier)
			{
				input.MoveNext();
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <inheritdoc/>
		public virtual bool Equals(Option? other)
		{
			return other != null && Identifier == other.Identifier && Prefix == other.Prefix;
		}

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			return Equals(obj as Option);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return HashCode.Combine(Identifier, Prefix);
		}

		/// <inheritdoc/>
		public override string ToString() => Prefix + Identifier;
	}
}
