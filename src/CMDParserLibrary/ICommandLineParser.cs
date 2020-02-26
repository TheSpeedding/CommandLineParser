using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public interface ICommandLineParser
	{
		IEnumerable<string> Parse(string[] args);

		string PrintHelp();
	}
}
