using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals.Options
{
	internal class ShortOption : Option
	{
		protected override string Prefix => "-";

		public ShortOption(string id) : base(id)
		{
		}
	}
}
