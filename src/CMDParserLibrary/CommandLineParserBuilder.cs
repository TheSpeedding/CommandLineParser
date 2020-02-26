using CMDParser.Internals;
using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMDParser
{
	public class CommandLineParserBuilder
	{
		private readonly ParserMethodsCollection _parsers = new ParserMethodsCollection();
		private readonly ISet<IOptionSetup> _options = new HashSet<IOptionSetup>();

		public void RegisterParser<TParsedType>(Func<string, TParsedType> parser)
		{
			_parsers.RegisterParseMethod(parser);
		}

		public OptionSetupBuilder<TParsedType> SetupOption<TParsedType>(params Option[] option)
		{
			var optionSetups = option.Select(x => new OptionSetup(x));

			foreach (var o in optionSetups)
				_options.Add(o);

			return new OptionSetupBuilder<TParsedType>(optionSetups);

		}

		public ICommandLineParser CreateParser()
		{
			return new CommandLineParser(_parsers);
		}
	}
}
