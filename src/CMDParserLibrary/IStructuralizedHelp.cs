namespace CMDParser
{
	/// <summary>
	/// Provides structuralized help information.
	/// </summary>
	public interface IStructuralizedHelp
	{
		/// <summary>
		/// Name of the command.
		/// </summary>
		public string CommandName { get; }

		/// <summary>
		/// Gets a description for the option.
		/// </summary>
		/// <param name="optionIdentifier">Identifier of the option.</param>
		string GetDescription(string optionIdentifier);

		/// <summary>
		/// Converts <see cref="IStructuralizedHelp"/> instance
		/// into the string.
		/// </summary>
		string Stringify();
	}
}
