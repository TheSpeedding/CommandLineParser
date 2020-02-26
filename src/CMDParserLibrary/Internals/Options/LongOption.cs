using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals.Options
{
	internal class LongOption : Option
	{
		protected override string Prefix => "--";

		public LongOption(string id) : base(id)
		{
		}
	}
}
