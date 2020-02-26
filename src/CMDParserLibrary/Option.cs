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
	}
}
