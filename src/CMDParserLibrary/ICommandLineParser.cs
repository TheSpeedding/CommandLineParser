using System.Collections.Generic;

namespace CMDParser
{
	/// <summary>
	/// A parser for command line arguments.
	/// </summary>
	public interface ICommandLineParser
	{
		/// <summary>
		/// Provides description for the options.
		/// </summary>
		IStructuralizedHelp Help { get; }

		/// <summary>
		/// Parses <paramref name="args"/> based on
		/// the configuration and returns a list
		/// of the parsed arguments.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		/// <returns>A collection of parsed command arguments.</returns>
		/// <example>
		/// Consider the following input: <c>-c --path=PATH_TO_FILE -d -f --- a -b --c</c>.
		/// The return value will be the collection having "a", "-b" and "--c" string literals.
		/// </example>
		/// <exception cref="IncorrectInputException">Thrown when there was a problem with parsing
		/// the input. The actual problem is announced in exception message.</exception>
		IReadOnlyList<string> Parse(string[] args);
	}
}
