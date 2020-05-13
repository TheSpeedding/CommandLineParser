using System;

namespace CMDParser
{
	/// <summary>
	/// An exception to handle all errors related to the incorrect input.
	/// </summary>
	public class IncorrectInputException : ArgumentException
	{
		/// <inheritdoc/>
		public IncorrectInputException(string message) : base(message)
		{
		}

		/// <inheritdoc/>
		public IncorrectInputException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
