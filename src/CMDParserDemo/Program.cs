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

		static void Main(string[] args)
		{
			var output = new TimeCommandLine();

			var parser = new CommandLineParser();

			parser.SetupOption<string>(Short('f'), Long("format"))
				.Callback(format => output.OutputFormat = format)
				.ParameterRequired();

			parser.SetupOption<bool>(Short('p'), Long("portability"))
				.Callback(isPortable => output.IsPortable = isPortable)
				.NoParameterRequired();

			parser.SetupOption<string>(Short('o'), Long("output"))
				.Callback(file => output.OutputFile = file)
				.ParameterRequired();

			parser.SetupOption<bool>(Short('a'), Long("append"))
				.Callback(shouldAppend => output.ShouldAppend = shouldAppend)
				.NoParameterRequired();

			parser.SetupOption<bool>(Short('v'), Long("verbose"))
				.Callback(isVerbose => output.IsOutputVerbose = isVerbose)
				.NoParameterRequired();

			parser.SetupOption<bool>(Long("help"))
				.Callback(shouldPrintHelp => output.ShouldPrintHelp = shouldPrintHelp)
				.NoParameterRequired();

			parser.SetupOption<bool>(Short('V'), Long("version"))
				.Callback(shouldPrintVersion => output.ShouldPrintVersion = shouldPrintVersion)
				.NoParameterRequired();

			parser.Parse(args);
		}
	}
}
