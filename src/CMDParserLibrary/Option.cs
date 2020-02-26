using CMDParser.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser
{
	public abstract class Option : IParsable, IEquatable<Option>
	{
		public abstract string Prefix { get; }

		public string Identifier { get; }

		protected Option(string id)
		{
			Identifier = id;
		}

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

		public virtual bool Equals(Option? other)
		{
			return other != null && Identifier == other.Identifier && Prefix == other.Prefix;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Option);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Identifier, Prefix);
		}
	}
}
