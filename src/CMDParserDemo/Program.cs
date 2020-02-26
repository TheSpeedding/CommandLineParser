using static CMDParser.OptionFactory;

namespace CMDParser.Demo
{
	public class Program
	{
		public class TimeCommandLine
		{
			public string OutputFormat { get; set; }

			public bool IsPortable { get; set; }

			public string OutputFile { get; set; }

			public bool ShouldAppend { get; set; }

			public bool IsOutputVerbose { get; set; }

			public bool ShouldPrintHelp { get; set; }

			public bool ShouldPrintVersion { get; set; }
		}

		private static ICommandLineParser GetTimeParser(TimeCommandLine output)
		{
			var parserBuilder = new CommandLineParserBuilder();

			parserBuilder.SetupOption<string>(Short('f'), Long("format"))
				.Callback(format => output.OutputFormat = format)
				.ParameterRequired();

			parserBuilder.SetupOption<bool>(Short('p'), Long("portability"))
				.Callback(isPortable => output.IsPortable = isPortable)
				.NoParameterRequired();

			parserBuilder.SetupOption<string>(Short('o'), Long("output"))
				.Callback(file => output.OutputFile = file)
				.ParameterRequired();

			parserBuilder.SetupOption<bool>(Short('a'), Long("append"))
				.Callback(shouldAppend => output.ShouldAppend = shouldAppend)
				.NoParameterRequired();

			parserBuilder.SetupOption<bool>(Short('v'), Long("verbose"))
				.Callback(isVerbose => output.IsOutputVerbose = isVerbose)
				.NoParameterRequired();

			parserBuilder.SetupOption<bool>(Long("help"))
				.Callback(shouldPrintHelp => output.ShouldPrintHelp = shouldPrintHelp)
				.NoParameterRequired();

			parserBuilder.SetupOption<bool>(Short('V'), Long("version"))
				.Callback(shouldPrintVersion => output.ShouldPrintVersion = shouldPrintVersion)
				.NoParameterRequired();

			return parserBuilder.CreateParser();
		}

		private static void Main(string[] args)
		{
			// The parser will parse the output into `output` instance.
			var output = new TimeCommandLine();

			GetTimeParser(output).Parse(args);
		}
	}
}
