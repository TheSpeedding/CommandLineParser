using CMDParser.Internals.Extensions;
using CMDParser.Internals.Options;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CMDParser.Internals
{
	/// <inheritdoc/>
	internal class CommandLineParser : ICommandLineParser
	{
		/// <summary>
		/// A delimiter between the options and the command arguments.
		/// </summary>
		private const string ArgumentsDelimiter = "--";

		// Maps option identifier to a method that can parse given option argument.
		// Parse methods are intentionally seperated because flag options do not have any parsers.
		// Additionally, `IOptionInfo` does not implement `IParsable`, only `IOptionInfo<T>` does.
		private readonly IReadOnlyDictionary<IOption, IParsable> _optionParsers;

		// Maps option identifier to more detailed information about given option.
		private readonly IReadOnlyDictionary<IOption, IOptionInfo> _optionInfos;

		// Maps option to all its aliases, inlcuding self.
		private readonly IReadOnlyDictionary<IOption, IEnumerable<IOption>> _optionAliases;

		/// <summary>
		/// Creates a new <see cref="CommandLineParser"/> instance.
		/// </summary>
		/// <param name="optionParsers">A mapping from option identifier to the function which can parse the option.</param>
		/// <param name="optionInfos">A mapping from option identifier to more detailed information for the option.</param>
		/// <param name="optionAliases">A mapping from option identifier to all its aliases.</param>
		/// <param name="commandName">Name of the command.</param>
		public CommandLineParser(
			IReadOnlyDictionary<IOption, IParsable> optionParsers,
			IReadOnlyDictionary<IOption, IOptionInfo> optionInfos,
			IReadOnlyDictionary<IOption, IEnumerable<IOption>> optionAliases,
			string commandName)
		{
			_optionParsers = optionParsers;
			_optionInfos = optionInfos;
			_optionAliases = optionAliases;

			Help = new StructuralizedHelp(optionInfos.Values, commandName);
		}

		/// <inheritdoc/>
		public IStructuralizedHelp Help { get; }

		/// <inheritdoc/>
		public IReadOnlyList<string> Parse(string[] args)
		{
			var input = new InputProcessor(args);

			// Collect mandatory options.
			var mandatoryOptions = _optionInfos.Values
				.Where(x => x.OptionAppearance == Appearance.Required)
				.Select(x => x.OptionIdentifier)
				.ToHashSet();

			// Auxilary set for options that were already parsed (to prevent parsing the same option multiple times).
			var alreadyParsed = new HashSet<IOption>();

			// Denotes whether the delimiter for arguments has already been reached or not.
			var delimiterReached = false;

			var parsedArguments = new List<string>();

			while (input.AnyInputLeft)
			{
				if (input.CurrentToken == ArgumentsDelimiter)
				{
					delimiterReached = true;
					input.MoveNext();
				}

				else if (TryParseOption(input, delimiterReached, out var parsedOption))
				{
					// We have found a parser which can parse this option (and it has parsed successfully).
					// Remove it from a set of mandatory options (to mark that we have already parsed it).
					mandatoryOptions.RemoveAll(_optionAliases[parsedOption]);

					// At this point, option is parsed, which is good.
					// But it can be parsed for the second time, which is not that good.
					if (!alreadyParsed.AddAll(_optionAliases[parsedOption]))
					{
						throw new IncorrectInputException(
							$"The option { parsedOption.Identifier } cannot be parsed multiple times.");
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

		private bool TryParseOption(InputProcessor input, bool delimiterReached, [NotNullWhen(true)] out IOption? parsedOption)
		{
			if (!delimiterReached)
			{
				foreach (var (option, optionParser) in _optionParsers)
				{
					if (optionParser.TryParse(input))
					{
						parsedOption = option;
						return true;
					}
				}
			}

			parsedOption = null;
			return false;
		}
	}
}
