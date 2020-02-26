﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals.Options
{
	interface IOptionSetup : IParsable
	{
		public IParsable OptionIdentifier { get; }

		public ParameterAppearance ParameterOptions { get; }
		
		public OptionAppearance Appearance { get; }

		public Action<object> Callback { get; }
	}
}
