using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMDParser.Internals
{
	class CommandLineParser : ICommandLineParser
	{
		private readonly IReadOnlyDictionary<Option, Func<InputProcessor, bool>> _options;
		private readonly IReadOnlyDictionary<Option, Func<Appearance>> _optionsAppearance;

		public CommandLineParser(IReadOnlyDictionary<Option, Func<InputProcessor, bool>> options, 
			IReadOnlyDictionary<Option, Func<Appearance>> optionsAppearance)
		{
			_options = options;
			_optionsAppearance = optionsAppearance;
		}

		public void Parse(string[] args)
		{
			var input = new InputProcessor(args);

			// Collect mandatory options.
			var mandatoryOptions = _optionsAppearance.Where(x => x.Value() == Appearance.Required).Select(x => x.Key).ToHashSet();

			while (input.AnyInputLeft)
				if (!TryParseOption(input, mandatoryOptions))
					throw new IncorrectInputException($"The option { input.CurrentToken } cannot be parsed by any of the configured parsers."); 

			if (mandatoryOptions.Count > 0)
				throw new IncorrectInputException("Not all the mandatory options were parsed. These are: " +
					string.Join(", ", mandatoryOptions.Select(x => "\"" + x.Identifier + "\"")));
		}

		private bool TryParseOption(InputProcessor input, ISet<Option> mandatoryOptions)
		{
			foreach (var (option, optionParser) in _options)
			{
				if (optionParser(input))
				{
					// We have found a parser which can parse this option (and it has parsed successfully).
					// Remove it from a set of mandatory options (to mark that we have already parsed it).
					mandatoryOptions.Remove(option);
					return true;
				}
			}
			return false;
		}
	}
}
