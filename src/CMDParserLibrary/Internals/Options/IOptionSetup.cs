﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals.Options
{
	interface IOptionSetup : IParsable
	{
		public Option OptionIdentifier { get; }

		public ParameterAppearance ParameterOptions { get; }
		
		public OptionAppearance Appearance { get; }
	}
}