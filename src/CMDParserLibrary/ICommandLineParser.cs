using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public interface ICommandLineParser
	{
		void Parse(string[] args);
	}
}
