using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public class CommandLineParser
	{		

		public void RegisterParser<TParsedType>(Func<string, TParsedType> parser)
		{

		}

		public OptionSetupBuilder<TParsedType> SetupOption<TParsedType>(params IOption[] option)
		{
			throw new NotImplementedException();
		}

		public void Parse(string[] args)
		{
			throw new NotImplementedException();
		}
	}
}
