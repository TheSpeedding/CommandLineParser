namespace CMDParser.Internals.Options
{
	/// <summary>
	/// Definition of methods required by <see cref="Option"/>.
	/// </summary>
	/// Simplifies unit testing.
	internal interface IOption : IParsable
	{
		/// <summary>
		/// Prefix of the option, e.g., "-" or "--".
		/// </summary>
		string Prefix { get; }

		/// <summary>
		/// Identifier of the option.
		/// </summary>
		string Identifier { get; }
	}
}
