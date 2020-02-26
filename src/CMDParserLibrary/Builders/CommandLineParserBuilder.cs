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
		private readonly Dictionary<Option, Func<InputProcessor, bool>> _options = new Dictionary<Option, Func<InputProcessor, bool>>();

		// A map from option to a function which returns option appearance.
		// The value must be a function because we want to be able to set the appearance from
		// `OptionSetupBuilder`, but we cannot provide any backward reference reflecting dynamic setting of the property.
		private readonly Dictionary<Option, Func<Appearance>> _optionsApperance = new Dictionary<Option, Func<Appearance>>();

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
			return new CommandLineParser(_options, _optionsApperance);
		}

		private IReadOnlyCollection<OptionSetup<T>> PrepareOptionSetups<T>(Option[] option)
		{
			var optionSetups = option.Select(x => new OptionSetup<T>(x, _parserMethods)).ToArray();

			foreach (var o in optionSetups)
			{
				_options.Add(o.OptionIdentifier, o.TryParse);
				_optionsApperance.Add(o.OptionIdentifier, () => o.OptionAppearance);
			}

			return optionSetups;
		}
	}
}
