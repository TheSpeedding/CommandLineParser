using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals
{
	class InputProcessor
	{
		private readonly IReadOnlyList<string> _input;
		private int _currentIndex;

		public InputProcessor(string[] args)
		{
			_currentIndex = 0;

			// Remove assignment operator from long options (treat them the same way like small options)
			var list = new List<string>();

			foreach (var arg in args)
			{
				if (arg.StartsWith(LongOption.OptionPrefix))
				{
					foreach (var token in arg.Split(LongOption.AssignmentOperator, StringSplitOptions.RemoveEmptyEntries))
					{
						list.Add(token);
					}
				}
				else
				{
					list.Add(arg);
				}
			}

			_input = list;
		}

		public bool EndReached => _currentIndex >= _input.Count;

		public bool AnyInputLeft => !EndReached;

		public void MoveNext() => ++_currentIndex;

		public string CurrentToken => _input[_currentIndex];
	}
}
