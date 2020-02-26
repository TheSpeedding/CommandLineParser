using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals.Options
{
	internal class LongOption : Option
	{
		public const string OptionPrefix = "--";
		public const string AssignmentOperator = "=";

		protected override string Prefix => OptionPrefix;

		public LongOption(string id) : base(id)
		{
		}
	}
}
