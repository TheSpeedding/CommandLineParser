using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals.Options
{
	interface IOptionSetup<in TParsedType> : IParsable
	{
		public Option OptionIdentifier { get; }

		public Appearance ParameterAppearance { get; }

		public Appearance OptionAppearance { get; }

		public Action<TParsedType> Callback { get; }
	}
}
