using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMDParser
{
	public class OptionSetupBuilder<TParsedType>
	{
		private readonly IReadOnlyCollection<OptionSetup<TParsedType>> _setup;

		internal OptionSetupBuilder(IReadOnlyCollection<OptionSetup<TParsedType>> setup)
		{
			_setup = setup;
		}
		
		public OptionSetupBuilder<TParsedType> OptionRequired()
		{
			foreach (var s in _setup)
				s.OptionAppearance = Appearance.Required;

			return this;
		}

		public OptionSetupBuilder<TParsedType> ParameterRequired()
		{
			foreach (var s in _setup)
				s.ParameterAppearance = Appearance.Required;

			return this;
		}

		public OptionSetupBuilder<TParsedType> Callback(Action<TParsedType> callback)
		{
			foreach (var s in _setup)
				s.Callback = callback;

			return this;
		}
	}
}
