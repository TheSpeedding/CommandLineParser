namespace CMDParser.Internals
{
	/// <summary>
	/// A read-only collection of parsed methods.
	/// </summary>
	internal interface IParserMethodsView
	{
		/// <summary>
		/// Parses the <paramref name="input"/> using the registered
		/// parse methods and returns <typeparamref name="TParsedType"/>
		/// if the type was parsed successfully.
		/// </summary>
		/// <typeparam name="TParsedType">Type of the instance
		/// which should be the input parsed in.</typeparam>
		TParsedType Parse<TParsedType>(string input);
	}
}
