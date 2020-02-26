using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser.Internals
{
	class InputProcessor
	{
		private const string TokensDelimiter = " ";

		private readonly string _input;
		private int _currentIndex;

		public InputProcessor(string[] args)
		{
			_input = string.Join(TokensDelimiter, args);
			_currentIndex = 0;
		}

		public bool EndReached => _currentIndex >= _input.Length;

		public bool AnyInputLeft => !EndReached;

		public void Move(int n) => _currentIndex += n;

		public void GetRemainingInput() => _input.Substring(_currentIndex);
	}
}
