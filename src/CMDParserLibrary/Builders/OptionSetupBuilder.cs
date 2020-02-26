using CMDParser.Builders;
using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMDParser
{
	public class OptionSetupBuilder<TParsedType> : AbstractOptionBuilder<OptionSetupBuilder<TParsedType>, TParsedType>
	{
		internal OptionSetupBuilder(IReadOnlyCollection<OptionSetup<TParsedType>> setup) : base(setup)
		{
		}

		protected override OptionSetupBuilder<TParsedType> Instance => this;

		public OptionSetupBuilder<TParsedType> ParameterRequired()
		{
			foreach (var s in Setup)
				s.ParameterAppearance = Appearance.Required;

			return Instance;
		}
	}
}
