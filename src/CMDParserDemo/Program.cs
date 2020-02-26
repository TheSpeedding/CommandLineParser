using static CMDParser.Options.OptionFactory;

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

			parser.SetupOption<string>(CreateShort('f'), CreateLong("format"))
				.Callback(format => output.OutputFormat = format)
				.ParameterRequired();

			parser.SetupOption<bool>(CreateShort('p'), CreateLong("portability"))
				.Callback(isPortable => output.IsPortable = isPortable)
				.NoParameterRequired();

			parser.SetupOption<string>(CreateShort('o'), CreateLong("output"))
				.Callback(file => output.OutputFile = file)
				.ParameterRequired();

			parser.SetupOption<bool>(CreateShort('a'), CreateLong("append"))
				.Callback(shouldAppend => output.ShouldAppend = shouldAppend)
				.NoParameterRequired();

			parser.SetupOption<bool>(CreateShort('v'), CreateLong("verbose"))
				.Callback(isVerbose => output.IsOutputVerbose = isVerbose)
				.NoParameterRequired();

			parser.SetupOption<bool>(CreateLong("help"))
				.Callback(shouldPrintHelp => output.ShouldPrintHelp = shouldPrintHelp)
				.NoParameterRequired();

			parser.SetupOption<bool>(CreateShort('V'), CreateLong("version"))
				.Callback(shouldPrintVersion => output.ShouldPrintVersion = shouldPrintVersion)
				.NoParameterRequired();

			parser.Parse(args);
		}
	}
}
