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
		private readonly Dictionary<Option, Func<InputProcessor, bool>> _optionParsers 
			= new Dictionary<Option, Func<InputProcessor, bool>>();

		// A map from option to a its info.
		private readonly Dictionary<Option, IOptionInfo> _optionInfos
			= new Dictionary<Option, IOptionInfo>();

		public void RegisterParser<TParsedType>(Func<string, TParsedType> parser)
		{
			_parserMethods.RegisterParseMethod(parser);
		}

		public OptionSetupBuilder<TParsedType> SetupOption<TParsedType>(params Option[] option)
		{
			return new OptionSetupBuilder<TParsedType>(PrepareOptionSetups<TParsedType>(option));
		}

		public FlagOptionBuilder SetupOption(params Option[] option)
		{
			return new FlagOptionBuilder(PrepareOptionSetups<Void>(option));
		}

		public ICommandLineParser CreateParser()
		{
			return new CommandLineParser(_optionParsers, _optionInfos);
		}

		private IReadOnlyCollection<OptionSetup<T>> PrepareOptionSetups<T>(Option[] option)
		{
			var optionSetups = option.Select(x => new OptionSetup<T>(x, _parserMethods)).ToArray();

			foreach (var o in optionSetups)
			{
				_optionParsers.Add(o.OptionIdentifier, o.TryParse);
				_optionInfos.Add(o.OptionIdentifier, o);
			}

			return optionSetups;
		}
	}
}
