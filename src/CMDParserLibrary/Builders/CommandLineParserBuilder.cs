using CMDParser.Internals;
using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMDParser.Builders
{
	/// <summary>
	/// A builder class for <see cref="ICommandLineParser"/>.
	/// </summary>
	public class CommandLineParserBuilder
	{
		// For example, `int.Parse(System.String)` can appear in this collection.
		private readonly ParserMethodsCollection _parserMethods 
			= new ParserMethodsCollection();

		// Note: Dictionary<TKey, TValue> used instead of IDictionary<TKey, TValue> just because
		// IDictionary<TKey, TValue> does not implement IReadOnlyDictionary<TKey, TValue>, whilst
		// Dictionary<TKey, TValue> does. This is further used in CreateParser() method.

		// A collection of methods that can parse options.
		// Parse methods are intentionally seperated because flag options do not have any parsers.
		// Additionally, `IOptionInfo` does not implement `IParsable`, only `IOptionInfo<T>` does.
		private readonly Dictionary<IOption, IParsable> _optionParsers 
			= new Dictionary<IOption, IParsable>();

		// A map from option to its info.
		private readonly Dictionary<IOption, IOptionInfo> _optionInfos
			= new Dictionary<IOption, IOptionInfo>();

		// A map from option to all its aliases, including self.
		private readonly Dictionary<IOption, IEnumerable<IOption>> _optionAliases
			= new Dictionary<IOption, IEnumerable<IOption>>();

		// Name of the command.
		private readonly string _commandName;

		/// <summary>
		/// Creates a new <see cref="CommandLineParserBuilder"/> instance.
		/// </summary>
		public CommandLineParserBuilder() : this(string.Empty)
		{
		}

		/// <summary>
		/// Creates a new <see cref="CommandLineParserBuilder"/> instance.
		/// </summary>
		/// <param name="commandName">Name of the command.</param>
		public CommandLineParserBuilder(string commandName)
		{
			_commandName = commandName;
		}

		/// <summary>
		/// Registers the parse method into the instance.
		/// </summary>
		/// <typeparam name="TParsedType">Type of the parsed instance.</typeparam>
		/// <param name="parser">A function that converts <see cref="string"/> into <typeparamref name="TParsedType"/>.</param>
		public void RegisterParser<TParsedType>(Func<string, TParsedType> parser)
		{
			_parserMethods.RegisterParseMethod(parser);
		}

		/// <summary>
		/// Creates a new setup for the parametrised option.
		/// </summary>
		/// <typeparam name="TParsedType">Type of the parsed argument.</typeparam>
		/// <param name="option">A collection of <see cref="Option"/> instances.</param>
		/// <returns>A builder that can set-up the options.</returns>
		/// <remarks>
		/// The <paramref name="option"/> collection defines aliases (synonyms) for the option.
		/// </remarks>
		/// <exception cref="ArgumentException">Thrown when <paramref name="option"/> is empty.</exception>
		public ParametrisedOptionBuilder<TParsedType> SetupOption<TParsedType>(params Option[] option)
		{
			if (option.Length == 0)
				throw new ArgumentException("Cannot set-up the option without specifying at least one of its aliases.");

			SetAliases(option);

			return new ParametrisedOptionBuilder<TParsedType>(PrepareOptionSetups<TParsedType>(option));
		}

		/// <summary>
		/// Creates a new setup for the flag option.
		/// </summary>
		/// <param name="option">A collection of <see cref="Option"/> instances.</param>
		/// <returns>A builder that can set-up the options.</returns>
		/// <remarks>
		/// The <paramref name="option"/> collection defines aliases (synonyms) for the option.
		/// </remarks>
		/// <exception cref="ArgumentException">Thrown when <paramref name="option"/> is empty.</exception>
		public FlagOptionBuilder SetupOption(params Option[] option)
		{
			if (option.Length == 0)
				throw new ArgumentException("Cannot set-up the option without specifying at least one of its aliases.");

			SetAliases(option);

			return new FlagOptionBuilder(PrepareOptionSetups<Internals.Void>(option));
		}

		/// <summary>
		/// Creates a parser with the configuration set by the user.
		/// </summary>
		public ICommandLineParser CreateParser()
		{
			return new CommandLineParser(_optionParsers, _optionInfos, _optionAliases, _commandName);
		}

		/// <summary>
		/// Converts option names (defined by user) into option setups (handled by the parser).
		/// </summary>
		private IReadOnlyCollection<OptionSetup<T>> PrepareOptionSetups<T>(Option[] options)
		{
			var optionSetups = options.Select(x => new OptionSetup<T>(x, _parserMethods)).ToArray();

			foreach (var o in optionSetups)
			{
				// Parse methods are intentionally seperated so we don't run into troubles with type system.
				_optionParsers.Add(o.OptionIdentifier, o);
				_optionInfos.Add(o.OptionIdentifier, o);
			}

			return optionSetups;
		}

		private void SetAliases(Option[] options)
		{
			foreach (var o in options)
			{
				// Option itself is included on purpose.
				_optionAliases.Add(o, options);
			}
		}
	}
}
