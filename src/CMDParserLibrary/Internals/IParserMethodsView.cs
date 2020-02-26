using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals
{
	internal interface IParserMethodsView
	{
		TParsedType Parse<TParsedType>(string input);
	}
}
