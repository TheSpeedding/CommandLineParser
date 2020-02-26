using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals
{
	internal interface IParsable
	{
		bool TryParse(InputProcessor input, [NotNullWhen(returnValue: true)] out string? parsed);
	}
}
