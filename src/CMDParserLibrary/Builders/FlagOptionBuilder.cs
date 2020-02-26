using CMDParser.Builders;
using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public class FlagOptionBuilder : AbstractOptionBuilder<FlagOptionBuilder, Void>
	{
		internal FlagOptionBuilder(IReadOnlyCollection<OptionSetup<Void>> setup) : base(setup)
		{
		}

		protected override FlagOptionBuilder Instance => this;
	}
}
