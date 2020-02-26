﻿using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Builders
{
	public abstract class AbstractOptionBuilder<TBuilder, TParsedType>
		where TBuilder : AbstractOptionBuilder<TBuilder, TParsedType>
	{
		protected abstract TBuilder Instance { get; }

		internal IReadOnlyCollection<OptionSetup<TParsedType>> Setup { get; }

		internal AbstractOptionBuilder(IReadOnlyCollection<OptionSetup<TParsedType>> setup)
		{
			Setup = setup;
		}

		public TBuilder OptionRequired()
		{
			foreach (var s in Setup)
				s.OptionAppearance = Appearance.Required;

			return Instance;
		}
		
		public TBuilder Callback(Action<TParsedType> callback)
		{
			foreach (var s in Setup)
				s.Callback = callback;

			return Instance;
		}
	}
}