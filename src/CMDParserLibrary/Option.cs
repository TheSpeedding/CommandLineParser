using CMDParser.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser
{
	public abstract class Option : IParsable
	{
		protected abstract string Prefix { get; }

		public string Identifier { get; }

		protected Option(string id)
		{
			Identifier = id;
		}

		public bool TryParse(string input, [NotNullWhen(true)] out string? parsed)
		{
			if (input == Prefix + Identifier)
			{
				parsed = Prefix + Identifier;
				return true;
			}
			else
			{
				parsed = null;
				return false;
			}
		}
	}
}
