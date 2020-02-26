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
		// For example, `int.Parse(System.String)` can appear in this collection.
		private readonly ParserMethodsCollection _parserMethods = new ParserMethodsCollection();

		// A collection of method that can parse options. Returns `true` when parsed successfully.
		private readonly HashSet<Func<InputProcessor, bool>> _options = new HashSet<Func<InputProcessor, bool>>();

		public void RegisterParser<TParsedType>(Func<string, TParsedType> parser)
		{
			_parserMethods.RegisterParseMethod(parser);
		}

		public OptionSetupBuilder<TParsedType> SetupOption<TParsedType>(params Option[] option)
		{
			var optionSetups = option.Select(x => new OptionSetup<TParsedType>(x, _parserMethods)).ToArray();

			foreach (var o in optionSetups)
				_options.Add(o.TryParse);

			return new OptionSetupBuilder<TParsedType>(optionSetups);

		}

		public ICommandLineParser CreateParser()
		{
			return new CommandLineParser(_parserMethods, _options);
		}
	}
}
