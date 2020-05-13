using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;

namespace CMDParser.Internals
{
	/// <summary>
	/// A class responsible for input processing.
	/// </summary>
	internal class InputProcessor
	{
		private readonly IReadOnlyList<string> _input;
		private int _currentIndex;

		/// <summary>
		/// Creates a new <see cref="InputProcessor"/> instance.
		/// </summary>
		/// <param name="args">A collection of arguments as provided by .NET runtime.</param>
		/// <exception cref="IncorrectInputException">Thrown when there are multiple assignment
		/// operators within one assignments.</exception>
		public InputProcessor(IEnumerable<string> args)
		{
			_currentIndex = 0;

			// Remove assignment operator from long options (treat them the same way like small options).
			var list = new List<string>();

			foreach (var arg in args)
			{
				if (arg.StartsWith(LongOption.OptionPrefix))
				{
					var splitted = arg.Split(LongOption.AssignmentOperator, StringSplitOptions.RemoveEmptyEntries);

					if (splitted.Length > 2)
						throw new IncorrectInputException($"Multiple assignment operators encountered in \"{ arg }\" argument.");

					foreach (var token in splitted)
						list.Add(token);
				}
				else
				{
					list.Add(arg);
				}
			}

			_input = list;
		}

		/// <inheritdoc/>
		public bool AnyInputLeft => _currentIndex < _input.Count;

		/// <summary>
		/// Moves to the next token.
		/// </summary>
		public void MoveNext() => ++_currentIndex;

		/// <summary>
		/// Gets current token as a plain string.
		/// </summary>
		public string CurrentToken => _input[_currentIndex];
	}
}
