using System;
using Xunit;

using static CMDParser.OptionFactory;

namespace CMDParser.Tests
{
	public class CommandLineParserIntegrationTests
	{
		#region Helper classes
		private class TimeCommandLine
		{
			public string OutputFormat { get; set; }

			public bool IsPortable { get; set; }

			public string OutputFile { get; set; }

			public bool ShouldAppend { get; set; }

			public bool IsOutputVerbose { get; set; }

			public bool ShouldPrintHelp { get; set; }

			public bool ShouldPrintVersion { get; set; }
		}
		#endregion

		#region Helper methods
		private static ICommandLineParser CreateTimeCommandParser(TimeCommandLine output)
		{
			var parserBuilder = new CommandLineParserBuilder();

			parserBuilder.SetupOption<string>(Short('f'), Long("format"))
				.WithDescription("Specify output format, possibly overriding the format specified in the environment variable TIME.")
				.Callback(format => output.OutputFormat = format)
				.ParameterRequired();

			parserBuilder.SetupOption(Short('p'), Long("portability"))
				.WithDescription("Use the portable output format.")
				.Callback(_ => output.IsPortable = true);

			parserBuilder.SetupOption<string>(Short('o'), Long("output"))
				.Callback(file => output.OutputFile = file)
				.WithDescription("Do not send the results to stderr, but overwrite the specified file.")
				.ParameterRequired();

			parserBuilder.SetupOption(Short('a'), Long("append"))
				.WithDescription("(Used together with -o.) Do not overwrite but append.")
				.Callback(_ => output.ShouldAppend = true);

			parserBuilder.SetupOption(Short('v'), Long("verbose"))
				.WithDescription("Give very verbose output about all the program knows about.")
				.Callback(isVerbose => output.IsOutputVerbose = true);

			parserBuilder.SetupOption(Long("help"))
				.WithDescription("Print a usage message on standard output and exit successfully.")
				.Callback(_ => output.ShouldPrintHelp = true);

			parserBuilder.SetupOption(Short('V'), Long("version"))
				.WithDescription("Print version information on standard output, then exit successfully.")
				.Callback(_ => output.ShouldPrintVersion = true);

			return parserBuilder.CreateParser();
		}
		#endregion

		[Fact]
		public void FunctionalRequirements_TimeExample_ParserWorksCorrectly()
		{
			// Arrange.
			var args = "-f MyFormat -a -v --output=MyFile -- -arg0 arg1 --arg2".Split(' ');

			var output = new TimeCommandLine();
			var parser = CreateTimeCommandParser(output);

			// Act.
			var parsedArgs = parser.Parse(args);

			// Assert.
			Assert.Equal("MyFormat", output.OutputFormat);
			Assert.Equal("MyFile", output.OutputFile);

			Assert.True(output.ShouldAppend);
			Assert.True(output.IsOutputVerbose);
			Assert.False(output.IsPortable);

			Assert.Contains("-arg0", parsedArgs);
			Assert.Contains("arg1", parsedArgs);
			Assert.Contains("--arg2", parsedArgs);

			Assert.Equal(
				"\"-f\", \"--format\": Specify output format, possibly overriding the format specified in the environment variable TIME." +
				Environment.NewLine + "\"-p\", \"--portability\": Use the portable output format." +
				Environment.NewLine + "\"-o\", \"--output\": Do not send the results to stderr, but overwrite the specified file." +
				Environment.NewLine + "\"-a\", \"--append\": (Used together with -o.) Do not overwrite but append." +
				Environment.NewLine + "\"-v\", \"--verbose\": Give very verbose output about all the program knows about." +
				Environment.NewLine + "\"--help\": Print a usage message on standard output and exit successfully." +
				Environment.NewLine + "\"-V\", \"--version\": Print version information on standard output, then exit successfully." +
				Environment.NewLine, parser.Help.Stringify());
		}
	}
}
