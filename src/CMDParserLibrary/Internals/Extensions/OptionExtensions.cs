using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals.Extensions
{
	internal static class OptionExtensions
	{
		// We need this because `Option` implements `IParsable` explicitly.
		public static IParsable AsParsable(this Option option) => option;
	}
}
