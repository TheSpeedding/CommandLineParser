using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;

namespace CMDParser.Builders
{
	/// <summary>
	/// A builder for parametrised options (i.e., those which can accept an argument).
	/// </summary>
	public class ParametrisedOptionBuilder<TParsedType> : AbstractOptionBuilder<ParametrisedOptionBuilder<TParsedType>, TParsedType>
	{
		/// <summary>
		/// Creates a new <see cref="ParametrisedOptionBuilder{TParsedType}"/> instance.
		/// </summary>
		/// <param name="setup">A collection of option setups.</param>
		internal ParametrisedOptionBuilder(IReadOnlyCollection<OptionSetup<TParsedType>> setup) : base(setup)
		{
		}

		/// <inheritdoc/>
		protected override ParametrisedOptionBuilder<TParsedType> Instance => this;

		/// <summary>
		/// Sets this option to require some argument.
		/// </summary>
		/// <remarks>
		/// If not set, the parameter is optional.
		/// </remarks>
		public ParametrisedOptionBuilder<TParsedType> ParameterRequired()
		{
			// We shall loop over all the option aliases.
			foreach (var s in Setup)
				s.ParameterAppearance = Appearance.Required;

			return Instance;
		}

		/// <summary>
		/// Registeres an action that should happen when the option is parsed successfully.
		/// </summary>
		/// <param name="callback">Function to be called once the option is parsed.
		/// The argument of the call is parsed argument of the option. If the
		/// argument is not present, i.e., it is optional, then <see langword="default"/>
		/// is the argument value.</param>
		public ParametrisedOptionBuilder<TParsedType> Callback(Action<TParsedType> callback)
		{
			// We shall loop over all the option aliases.
			foreach (var s in Setup)
				s.Callback = callback;

			return Instance;
		}
	}
}
