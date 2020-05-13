namespace CMDParser.Internals
{
	/// <summary>
	/// The interface providing methods that can parse the input.
	/// </summary>
	/// <remarks>
	/// The common usage will be such that some option setup will
	/// implement this interface. Note that the option setup
	/// should not store any data about the parsed input.
	/// It only should call the respective callback which is
	/// responsible for manipulating the parsed input.
	/// 
	/// We are aware of that instance parse methods are considered
	/// to be anti-pattern, yet this is really not the intention.
	/// The callback will be called once the input is parsed successfully.
	/// Check builders for more detailed information.
	/// </remarks>
	internal interface IParsable
	{
		/// <summary>
		/// Tries to parse the input and returns <see langword="true"/>
		/// if the input is parsed successfully.
		/// </summary>
		/// <param name="input"><see cref="InputProcessor"/> instance
		/// providing the input data.</param>
		bool TryParse(InputProcessor input);
	}
}
