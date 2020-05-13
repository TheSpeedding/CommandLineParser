using CMDParser.Internals;
using System.Linq;
using Xunit;

namespace CMDParser.Tests
{
	public class InputProcessorUnitTests
	{
		[Fact]
		public void Process_NoArguments_WorksCorrectly()
		{
			// Arrange.
			var processor = new InputProcessor(Enumerable.Empty<string>());

			// Assert.
			Assert.False(processor.AnyInputLeft);
		}

		[Fact]
		public void Process_LongOption_WorksCorrectly()
		{
			// Arrange.
			var processor = new InputProcessor(new[] { "--myoption=ARGUMENT" });

			// Act.
			var optionName = processor.CurrentToken;
			processor.MoveNext();

			var optionArgument = processor.CurrentToken;
			processor.MoveNext();

			// Assert.
			Assert.Equal("--myoption", optionName);
			Assert.Equal("ARGUMENT", optionArgument);
			Assert.False(processor.AnyInputLeft);
		}

		[Fact]
		public void Process_MultipleAssignmentOperators_ThrowsException()
		{
			// Assert.
			Assert.Throws<IncorrectInputException>(() => new InputProcessor(new[] { "--myoption=ARGUMENT1=ARGUMENT2" }));
		}

		[Fact]
		public void Process_ShortOption_WorksCorrectly()
		{
			// Arrange.
			var processor = new InputProcessor(new[] { "-o", "ARGUMENT" });

			// Act.
			var optionName = processor.CurrentToken;
			processor.MoveNext();

			var optionArgument = processor.CurrentToken;
			processor.MoveNext();

			// Assert.
			Assert.Equal("-o", optionName);
			Assert.Equal("ARGUMENT", optionArgument);
			Assert.False(processor.AnyInputLeft);
		}
	}
}
