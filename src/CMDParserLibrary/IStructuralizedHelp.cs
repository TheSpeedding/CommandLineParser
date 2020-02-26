using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public interface IStructuralizedHelp
	{
		string GetDescription(string optionIdentifier);

		string Stringify();
	}
}
