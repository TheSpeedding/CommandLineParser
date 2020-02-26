using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public class OptionSetupBuilder<TParsedType>
	{
		private readonly IEnumerable<OptionSetup> _setup;

		internal OptionSetupBuilder(IEnumerable<OptionSetup> setup)
		{
			_setup = setup;
		}

		public OptionSetupBuilder<TParsedType> ParameterRequired()
		{
			foreach (var s in _setup)
				s.ParameterOptions = ParameterAppearance.Required;

			return this;
		}

		public OptionSetupBuilder<TParsedType> NoParameterRequired()
		{
			foreach (var s in _setup)
				s.ParameterOptions = ParameterAppearance.None;

			return this;
		}

		public OptionSetupBuilder<TParsedType> Callback(Action<TParsedType> callback)
		{
			foreach (var s in _setup)
				s.Callback = x => callback((TParsedType)x);

			return this;
		}
	}
}
