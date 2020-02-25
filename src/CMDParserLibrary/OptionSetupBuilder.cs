using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public class OptionSetupBuilder<TParsedType>
	{
		public OptionSetupBuilder<TParsedType> ParameterRequired()
		{
			throw new NotImplementedException();
		}

		public OptionSetupBuilder<TParsedType> NoParameterRequired()
		{
			throw new NotImplementedException();
		}

		public OptionSetupBuilder<TParsedType> Callback(Action<TParsedType> callback)
		{
			throw new NotImplementedException();
		}
	}
}
