using CMDParser.Internals.Options;
using System.Collections.Generic;

namespace CMDParser.Builders
{
	/// <summary>
	/// An abstract builder which is supposed to configure parser's options.
	/// </summary>
	/// <typeparam name="TBuilder">Type of the builder.</typeparam>
	/// <typeparam name="TParsedType">Type which should be parsed.</typeparam>
	public abstract class AbstractOptionBuilder<TBuilder, TParsedType>
		where TBuilder : AbstractOptionBuilder<TBuilder, TParsedType>
	{
		/// <summary>
		/// This <see cref="AbstractOptionBuilder{TBuilder, TParsedType}"/> instance.
		/// </summary>
		protected abstract TBuilder Instance { get; }

		/// <summary>
		/// A collection of <see cref="OptionSetup{TParsedType}"/> instances.
		/// This represents all the aliases for the option.
		/// </summary>
		/// This should be rather `protected` within the assembly, but C# unfortunately does not have package private modifier like Java.
		internal IReadOnlyCollection<OptionSetup<TParsedType>> Setup { get; }

		/// <summary>
		/// Creates a new <see cref="AbstractOptionBuilder{TBuilder, TParsedType}"/> instance.
		/// </summary>
		/// <param name="setup">A collection of <see cref="OptionSetup{TParsedType}"/> instances. 
		/// This represents all the aliases for the option.</param>
        /// The constructor cannot be `protected` because `OptionSetup{TParsedType}`is `internal`
        /// and the compiler would not let us through. We would need `protected internal` modifier
        /// here but it does not exist in .NET world.
		internal AbstractOptionBuilder(IReadOnlyCollection<OptionSetup<TParsedType>> setup)
		{
			Setup = setup;
		}

		/// <summary>
		/// Configures the option to be required.
		/// </summary>
		public TBuilder OptionRequired()
		{
			// We shall loop over all the option aliases.
			foreach (var s in Setup)
				s.OptionAppearance = Appearance.Required;

			return Instance;
		}

		/// <summary>
		/// Sets the description for the option.
		/// </summary>
		/// <param name="description">Description of the option.</param>
		public TBuilder WithDescription(string description)
		{
			// We shall loop over all the option aliases.
			foreach (var s in Setup)
				s.Description = description;

			return Instance;
		}
	}
}
