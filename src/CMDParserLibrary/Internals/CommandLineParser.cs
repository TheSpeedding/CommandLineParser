using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace CMDParser.Internals
{
	class CommandLineParser : ICommandLineParser
	{
		private const string ArgumentsDelimiter = "--";

		private readonly IReadOnlyDictionary<Option, Func<InputProcessor, bool>> _optionParsers;
		private readonly IReadOnlyDictionary<Option, IOptionInfo> _optionInfos;

		public CommandLineParser(IReadOnlyDictionary<Option, Func<InputProcessor, bool>> optionParsers,
			IReadOnlyDictionary<Option, IOptionInfo> optionInfos)
		{
			_optionParsers = optionParsers;
			_optionInfos = optionInfos;

			Help = new StructuralizedHelp(optionInfos.Values);
		}

		public IStructuralizedHelp Help { get; }

		public IReadOnlyList<string> Parse(string[] args)
		{
			var input = new InputProcessor(args);

			// Collect mandatory options.
			var mandatoryOptions = _optionInfos.Values
				.Where(x => x.OptionAppearance == Appearance.Required)
				.Select(x => x.OptionIdentifier)
				.ToHashSet();

			// Auxilary set for options that were already parsed (to prevent parsing the same option multiple times).
			var alreadyParsed = new HashSet<Option>();

			// Denotes whether the delimiter for arguments has already been reached or not.
			var delimiterReached = false;

			var parsedArguments = new List<string>();

			while (input.AnyInputLeft)
			{
				if (input.CurrentToken == ArgumentsDelimiter)
				{
					delimiterReached = true;
				}
				if (TryParseOption(input, mandatoryOptions, out var parsedOption))
				{
					if (alreadyParsed.Contains(parsedOption))
					{
						throw new IncorrectInputException(
							$"The option { input.CurrentToken } cannot be parsed multiple times.");
					}
					else
					{
						alreadyParsed.Add(parsedOption);
					}
				}
				else if (TryParseArgument(input, delimiterReached, out var argument))
				{
					parsedArguments.Add(argument);
				}
				else
				{
					throw new IncorrectInputException(
						$"The option { input.CurrentToken } cannot be parsed by any of the configured parsers.");
				}

			}

			if (mandatoryOptions.Count > 0)
				throw new IncorrectInputException("Not all the mandatory options were parsed. These are: " +
					string.Join(", ", mandatoryOptions.Select(x => "\"" + x.Identifier + "\"")));

			return parsedArguments;
		}

		private bool TryParseArgument(InputProcessor input, bool delimiterReached, [NotNullWhen(true)] out string? argument)
		{
			if (delimiterReached
				|| !input.CurrentToken.StartsWith(ShortOption.OptionPrefix)
				|| !input.CurrentToken.StartsWith(LongOption.OptionPrefix))
			{
				argument = input.CurrentToken;
				input.MoveNext();
				return true;
			}
			else
			{
				argument = null;
				return false;
			}
		}

		private bool TryParseOption(InputProcessor input, ISet<Option> mandatoryOptions, [NotNullWhen(true)] out Option? parsedOption)
		{
			foreach (var (option, optionParser) in _optionParsers)
			{
				if (optionParser(input))
				{
					// We have found a parser which can parse this option (and it has parsed successfully).
					// Remove it from a set of mandatory options (to mark that we have already parsed it).
					mandatoryOptions.Remove(option);
					parsedOption = option;
					return true;
				}
			}

			parsedOption = null;
			return false;
		}
	}
}
