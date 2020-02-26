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
		#endregion

		[Fact]
		public void FunctionalRequirementsExample_ParserWorksCorrectly()
		{
			// Arrange.
			var args = "-f MyFormat -a -v --output=MyFile arg1 arg2".Split(' ');

			var output = new TimeCommandLine();
			var parser = CreateTimeCommandParser(output);

			// Act.
			parser.Parse(args);

			// Assert.
			Assert.Equal("MyFormat", output.OutputFormat);
			Assert.Equal("MyFile", output.OutputFile);
			Assert.True(output.ShouldAppend);
			Assert.True(output.IsOutputVerbose);
		}
	}
}
