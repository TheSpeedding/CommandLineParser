using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals.Options
{
	internal interface IOptionInfo : IEquatable<IOptionInfo>
	{
		public Option OptionIdentifier { get; }

		public Appearance ParameterAppearance { get; }

		public Appearance OptionAppearance { get; }

		public string Description { get; }
	}
}
