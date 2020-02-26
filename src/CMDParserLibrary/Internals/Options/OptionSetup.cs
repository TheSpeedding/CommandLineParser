﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals.Options
{
	internal class OptionSetup<TParsedType> : IOptionSetup<TParsedType>
	{
		private readonly IParserMethodsView _parsers;

		// We need this because `Option` implements `IParsable` explicitly.
		private IParsable ParsableIdentifier => OptionIdentifier;

		public Option OptionIdentifier { get; }

		public Appearance ParameterAppearance { get; set; } = Appearance.Optional;

		public Appearance OptionAppearance { get; set; } = Appearance.Optional;

		public Action<TParsedType> Callback { get; set; } = _ => { };

		public OptionSetup(Option option, IParserMethodsView parsers)
		{
			OptionIdentifier = option;
			_parsers = parsers;
		}

		public bool TryParse(InputProcessor input)
		{
			if (!ParsableIdentifier.TryParse(input))
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
	}
}
