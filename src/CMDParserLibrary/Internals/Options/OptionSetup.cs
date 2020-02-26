﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals.Options
{
	class OptionSetup<TParsedType> : IOptionSetup<TParsedType>
	{
		private readonly IParserMethodsView _parsers;

		public Option OptionIdentifier { get; set; }

		public ParameterAppearance ParameterOptions { get; set; }

		public OptionAppearance Appearance { get; set; }

		public Action<TParsedType> Callback { get; set; } = _ => { };

		public OptionSetup(Option option, IParserMethodsView parsers)
		{
			OptionIdentifier = option;
			_parsers = parsers;
		}

		public bool TryParse(InputProcessor input)
		{
			if (!OptionIdentifier.TryParse(input))
			{
				return false;
			}
			else
			{
				switch (ParameterOptions)
				{
					// Option is present at input, but accepts no arguments, thus is intended to return `true`.
					case ParameterAppearance.None:
						// TODO: Call callback with "true" argument somehow.
						Callback(default!);
						return true;

					case ParameterAppearance.Optional:
						if (input.CurrentToken.StartsWith(LongOption.OptionPrefix) || input.CurrentToken.StartsWith(ShortOption.OptionPrefix))
						{
							// Then the argument is non-present, skip it.
							Callback(default!);
						}
						else
						{
							// Otherwise the argument is present.
							Callback(ParseArgument(input));
						}
						return true;

					case ParameterAppearance.Required:
						Callback(ParseArgument(input));
						return true;
				}
			}

			throw new InvalidOperationException("Unreachable code detected.");
		}

		private TParsedType ParseArgument(InputProcessor input)
		{
			var arg = input.CurrentToken;
			input.MoveNext();
			return _parsers.Parse<TParsedType>(arg);
		}
	}
}
