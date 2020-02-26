﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMDParser.Internals
{
	class CommandLineParser : ICommandLineParser
	{
		private readonly IReadOnlyCollection<Func<InputProcessor, bool>> _options;

		public CommandLineParser(IReadOnlyCollection<Func<InputProcessor, bool>> options)
		{
			_options = options;
		}

		public void Parse(string[] args)
		{
			var input = new InputProcessor(args);

			while (input.AnyInputLeft)
				if (!_options.Any(optionParser => optionParser(input)))
					throw new IncorrectInputException("The option (TODO: which option?) cannot be parsed by any of the configured parsers."); // TODO
		}
	}
}
