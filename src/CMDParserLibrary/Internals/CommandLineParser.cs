using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals
{
	class CommandLineParser : ICommandLineParser
	{
		private readonly IParserMethodsView _parsers;

		public CommandLineParser(IParserMethodsView parsers)
		{
			_parsers = parsers;
		}

		public void Parse(string[] args)
		{
			throw new NotImplementedException();
		}
	}
}
