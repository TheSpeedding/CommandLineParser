using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals
{
	interface IParsable
	{
		bool TryParse(string input, [NotNullWhen(returnValue: true)] out string? parsed);
	}
}
