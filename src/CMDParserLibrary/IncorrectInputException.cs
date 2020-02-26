using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	public class IncorrectInputException : ArgumentException
	{
		public IncorrectInputException(string message) : base(message)
		{
		}
	}
}
