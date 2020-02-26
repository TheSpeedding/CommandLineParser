using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public static class OptionFactory
	{
		public static Option Short(char identifier)
		{
			return new ShortOption(identifier.ToString());
		}

		public static Option Long(string identifier)
		{
			return new LongOption(identifier);
		}
	}
}
