using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMDParser.Internals
{
	class CommandLineParser : ICommandLineParser
	{
		private readonly IReadOnlyCollection<Func<InputProcessor, bool>> _options;
		private readonly IReadOnlyDictionary<Option, Func<OptionAppearance>> _optionsAppearance;

		public CommandLineParser(IReadOnlyCollection<Func<InputProcessor, bool>> options, 
			IReadOnlyDictionary<Option, Func<OptionAppearance>> optionsAppearance)
		{
			_options = options;
			_optionsAppearance = optionsAppearance;
		}

		public void Parse(string[] args)
		{
			var input = new InputProcessor(args);

			// Collect mandatory options.
			var mandatoryOptions = _optionsAppearance.Where(x => x.Value() == OptionAppearance.Required).Select(x => x.Key);

			while (input.AnyInputLeft)
				if (!_options.Any(optionParser => optionParser(input)))
					throw new IncorrectInputException("The option (TODO: which option?) cannot be parsed by any of the configured parsers."); // TODO
		}
	}
}
