using CMDParser.Internals;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public class CommandLineParserBuilder
	{
		private readonly ParserMethodsCollection _parsers = new ParserMethodsCollection();

		public void RegisterParser<TParsedType>(Func<string, TParsedType> parser)
		{
			_parsers.RegisterParseMethod(parser);
		}

		public OptionSetupBuilder<TParsedType> SetupOption<TParsedType>(params Option[] option)
		{
			throw new NotImplementedException();
		}

		public ICommandLineParser CreateParser()
		{
			return new CommandLineParser(_parsers);
		}
	}
}
