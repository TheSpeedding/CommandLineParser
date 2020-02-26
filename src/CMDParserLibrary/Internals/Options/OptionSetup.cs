using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals.Options
{
	class OptionSetup : IOptionSetup
	{
		public Option OptionIdentifier { get; set; }

		public ParameterAppearance ParameterOptions { get; set; }

		public OptionAppearance Appearance { get; set; }

		public OptionSetup(Option option)
		{
			OptionIdentifier = option;
		}

		public bool TryParse(string input, [NotNullWhen(true)] out string? parsed)
		{
			throw new NotImplementedException();
		}
	}
}
