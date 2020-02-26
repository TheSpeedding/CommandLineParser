using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals.Options
{
	internal class ShortOption : Option
	{
		public const string OptionPrefix = "-";

		public override string Prefix => OptionPrefix;

		public ShortOption(string id) : base(id)
		{
		}
	}
}
