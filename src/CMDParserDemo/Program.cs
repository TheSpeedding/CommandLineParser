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

			var parser = new CommandLineParserBuilder();

			parser.Setup<string>(CreateShort('f'), CreateLong("format"))
				.Callback(format => output.OutputFormat = format)
				.ParameterRequired();

			parser.Setup<bool>(CreateShort('p'), CreateLong("portability"))
				.Callback(isPortable => output.IsPortable = isPortable)
				.NoParameterRequired();

			parser.Setup<string>(CreateShort('o'), CreateLong("output"))
				.Callback(file => output.OutputFile = file)
				.ParameterRequired();

			parser.Setup<bool>(CreateShort('a'), CreateLong("append"))
				.Callback(shouldAppend => output.ShouldAppend = shouldAppend)
				.NoParameterRequired();

			parser.Setup<bool>(CreateShort('v'), CreateLong("verbose"))
				.Callback(isVerbose => output.IsOutputVerbose = isVerbose)
				.NoParameterRequired();

			parser.Setup<bool>(CreateLong("help"))
				.Callback(shouldPrintHelp => output.ShouldPrintHelp = shouldPrintHelp)
				.NoParameterRequired();

			parser.Setup<bool>(CreateShort('V'), CreateLong("version"))
				.Callback(shouldPrintVersion => output.ShouldPrintVersion = shouldPrintVersion)
				.NoParameterRequired();

			parser.Parse(args);
		}
	}
}
