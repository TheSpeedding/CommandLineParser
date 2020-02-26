using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	internal interface IParserMethodsView
	{
		void RegisterParseMethod<TParsedType>(Func<string, TParsedType> parser);
	}
}
