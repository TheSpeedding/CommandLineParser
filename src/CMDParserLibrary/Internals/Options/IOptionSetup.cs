using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals.Options
{
	interface IOptionSetup<in TParsedType> : IOptionInfo, IParsable
	{
		public Action<TParsedType> Callback { get; }
	}
}
