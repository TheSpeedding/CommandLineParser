using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CMDParser.Internals.Options
{
	class OptionSetup : IOptionSetup
	{
		public IParsable OptionIdentifier { get; set; }

		public ParameterAppearance ParameterOptions { get; set; }

		public OptionAppearance Appearance { get; set; }

		public Action<object> Callback { get; set; } = _ => { };

		public OptionSetup(Option option)
		{
			OptionIdentifier = option;
		}

		public bool TryParse(InputProcessor input, [NotNullWhen(true)] out string? parsed)
		{
			if (!OptionIdentifier.TryParse(input, out var parsedIdentifier))
			{
				parsed = null;
				return false;
			}
			else
			{
				switch (ParameterOptions)
				{
					// Option is present at input, but accepts no arguments, thus is intended to return `true`.
					case ParameterAppearance.None:
						parsed = parsedIdentifier;
						Callback(true); // TODO: This can cail if the input is incorrect, i.e., throw BadCastException.
						break;

					case ParameterAppearance.Optional:
						if (input.CurrentToken.StartsWith(LongOption.OptionPrefix) || input.CurrentToken.StartsWith(ShortOption.OptionPrefix))
						{
							// Then the argument is non-present, skip it.
							parsed = parsedIdentifier;
							Callback(true);
							break;
						}
						else
						{
							// Otherwise the argument is present.
							var arg = input.CurrentToken;
							input.MoveNext();

							// TODO: Parse it using a parser and call callback.
						}
						break;

					case ParameterAppearance.Required:
						break;
				}
			}
		}

		private bool TryParseArgument(InputProcessor input, [NotNullWhen(true)] out string? parsed)
		{

		}

	}
}
