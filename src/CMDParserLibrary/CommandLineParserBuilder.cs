using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public class CommandLineParserBuilder
	{		
		public OptionSetupBuilder<TParsedType> Setup<TParsedType>(params IOption[] option)
		{
			throw new NotImplementedException();
		}

		public void Parse(string[] args)
		{
			throw new NotImplementedException();
		}
	}
}
