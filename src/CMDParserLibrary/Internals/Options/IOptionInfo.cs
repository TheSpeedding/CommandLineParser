using System;

namespace CMDParser.Internals.Options
{
	/// <summary>
	/// Interface providing basic information about the option.
	/// </summary>
	internal interface IOptionInfo : IEquatable<IOptionInfo>
	{
		/// <summary>
		/// Option identifier as <see cref="IOption"/>.
		/// </summary>
		/// <example>
		/// It can be something like <c>--longoption</c>, or
		/// <c>-s</c>.
		/// </example>
		public IOption OptionIdentifier { get; }

		/// <summary>
		/// <see cref="Appearance"/> of the parameter.
		/// </summary>
		public Appearance ParameterAppearance { get; }

		/// <summary>
		/// <see cref="Appearance"/> of the option.
		/// </summary>
		public Appearance OptionAppearance { get; }

		/// <summary>
		/// Description of the option as plain string.
		/// </summary>
		public string Description { get; }
	}
}
