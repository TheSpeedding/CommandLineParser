using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;

namespace CMDParser.Builders
{
	/// <summary>
	/// A builder for flag options (i.e., those which cannot accept any arguments).
	/// </summary>
	public class FlagOptionBuilder : AbstractOptionBuilder<FlagOptionBuilder, Internals.Void>
	{
		/// <summary>
		/// Creates a new <see cref="FlagOptionBuilder"/> instance.
		/// </summary>
		/// <param name="setup">A collection of option setups.</param>
		internal FlagOptionBuilder(IReadOnlyCollection<OptionSetup<Internals.Void>> setup) : base(setup)
		{
		}

		/// <inheritdoc/>
		protected override FlagOptionBuilder Instance => this;

		/// <summary>
		/// Registers an action that should happen when the option is parsed successfully.
		/// </summary>
		/// <param name="callback">Function to be called once the option is parsed.</param>
		public FlagOptionBuilder Callback(Action callback)
		{
			// We shall loop over all the option aliases.
			foreach (var s in Setup)
				s.Callback = _ => callback();

			return Instance;
		}
	}
}
