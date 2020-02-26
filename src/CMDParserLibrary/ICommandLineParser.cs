using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public interface ICommandLineParser
	{
		IStructuralizedHelp Help { get; }

		IEnumerable<string> Parse(string[] args);
	}
}
