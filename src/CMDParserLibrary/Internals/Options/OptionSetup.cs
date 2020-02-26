using CMDParser.Internals.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals.Options
{
	internal class OptionSetup<TParsedType> : IOptionSetup<TParsedType>
	{
		private readonly IParserMethodsView _parsers;
		
		public Option OptionIdentifier { get; }

		public Appearance ParameterAppearance { get; set; } = Appearance.Optional;

		public Appearance OptionAppearance { get; set; } = Appearance.Optional;

		public Action<TParsedType> Callback { get; set; } = _ => { };

		public string Description { get; set; } = string.Empty;

		public OptionSetup(Option option, IParserMethodsView parsers)
		{
			OptionIdentifier = option;
			_parsers = parsers;
		}

		public bool TryParse(InputProcessor input)
		{
			if (!OptionIdentifier.AsParsable().TryParse(input))
			{
				return false;
			}
			else
			{
				if (typeof(TParsedType) == typeof(Void))
				{
					// Option is intended to have no arguments.
					Callback(default!);
					return true;
				}

				else if (ParameterAppearance == Appearance.Optional)
				{
					if (input.CurrentToken.StartsWith(LongOption.OptionPrefix) || input.CurrentToken.StartsWith(ShortOption.OptionPrefix))
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
		}

		private TParsedType ParseArgument(InputProcessor input)
		{
			var arg = input.CurrentToken;
			input.MoveNext();
			return _parsers.Parse<TParsedType>(arg);
		}

		public bool Equals(IOptionInfo? other)
		{
			return other != null && OptionAppearance == other.OptionAppearance &&
				ParameterAppearance == other.ParameterAppearance && OptionIdentifier.Equals(other.OptionIdentifier);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as IOptionInfo);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(OptionAppearance, ParameterAppearance, OptionIdentifier);
		}
	}
}
