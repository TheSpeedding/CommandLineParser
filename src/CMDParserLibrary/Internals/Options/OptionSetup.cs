using System;

namespace CMDParser.Internals.Options
{
	/// <summary>
	/// Naïve implementation of <see cref="IOptionSetup{TParsedType}"/> interface.
	/// </summary>
	/// <typeparam name="TParsedType">Parameter of the parsed type.</typeparam>
    /// <remarks>
    /// The class works as a builder, the interface it impements is immutable.
    /// </remarks>
	internal class OptionSetup<TParsedType> : IOptionSetup<TParsedType>
	{
		private readonly IParserMethodsView _parsers;

		/// <inheritdoc/>
		public IOption OptionIdentifier { get; }

		/// <inheritdoc/>
		public Appearance ParameterAppearance { get; set; } = Appearance.Optional;

		/// <inheritdoc/>
		public Appearance OptionAppearance { get; set; } = Appearance.Optional;

		/// <inheritdoc/>
		public Action<TParsedType> Callback { get; set; } = _ => { };

		/// <inheritdoc/>
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Creates a new <see cref="OptionSetup{TParsedType}"/> instance.
		/// </summary>
		/// <param name="option">The option identifier.</param>
		/// <param name="parsers">A read-only collection of the parse methods.</param>
		public OptionSetup(Option option, IParserMethodsView parsers)
		{
			OptionIdentifier = option;
			_parsers = parsers;
		}

		/// <inheritdoc/>
        /// <exception cref="IncorrectInputException">Thrown when end of stream reached, but
        /// some argument is expected.</exception>
		public bool TryParse(InputProcessor input)
		{
			if (!OptionIdentifier.TryParse(input))
			{
				// Option cannot be parsed.
				return false;
			}

			else if (typeof(TParsedType) == typeof(Void))
			{
				// Option is intended to have no arguments (i.e., it is a flag option).
				// Note that the default argument is never accessible from user code.
				Callback(default!);
				return true;
			}

			else if (ParameterAppearance == Appearance.Optional)
			{
				if (IsOptionDefinition(input.CurrentToken))
				{
					// Then the argument is non-present, skip it.
					Callback(default!);
					return true;
				}

				else
				{
					// Otherwise the argument is present.
					Callback(ParseArgument(input));
					return true;
				}
			}

			else if (ParameterAppearance == Appearance.Required)
			{
				Callback(ParseArgument(input));
				return true;
			}

			else
			{
				return false;
			}
		}

		private bool IsOptionDefinition(string token) => token.StartsWith(LongOption.OptionPrefix) || token.StartsWith(ShortOption.OptionPrefix);

		private TParsedType ParseArgument(InputProcessor input)
		{
			if (!input.AnyInputLeft)
				throw new IncorrectInputException("Cannot parse argument for the option because end of input stream has been reached.");

			var arg = input.CurrentToken;
			input.MoveNext();

			// We intentionally do not catch any exception here, we are letting parse methods to define their 
			// own exception if the parsing of the argument fails.
            // For example, whenever `int.Parse("this it not a number")` fails, exception defined by that parse
            // method is thrown.
			return _parsers.Parse<TParsedType>(arg);
		}

		/// <inheritdoc/>
		public bool Equals(IOptionInfo? other)
		{
			return other != null && OptionAppearance == other.OptionAppearance &&
				ParameterAppearance == other.ParameterAppearance && OptionIdentifier.Equals(other.OptionIdentifier);
		}

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			return Equals(obj as IOptionInfo);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return HashCode.Combine(OptionAppearance, ParameterAppearance, OptionIdentifier);
		}
	}
}
