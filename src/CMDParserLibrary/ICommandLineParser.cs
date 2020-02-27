using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public interface ICommandLineParser
	{
		IStructuralizedHelp Help { get; }

		IReadOnlyList<string> Parse(string[] args);
	}
}
