using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals
{
	internal interface IParserMethodsView
	{
		void RegisterParseMethod<TParsedType>(Func<string, TParsedType> parser);
	}
}
