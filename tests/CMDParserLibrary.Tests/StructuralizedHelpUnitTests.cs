using CMDParser.Internals;
using CMDParser.Internals.Options;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace CMDParser.Tests
{
	public class StructuralizedHelpUnitTests
	{
		[Fact]
		public void GetCommandName_WorksCorrectly()
		{
			// Arrange.
			var help = new StructuralizedHelp(Enumerable.Empty<IOptionInfo>(), "my command");

			// Act.
			var actualName = help.CommandName;

			// Assert.
			Assert.Equal("my command", actualName);
		}

		[Fact]
		public void GetDescription_DoesNotExist_ThrowsException()
		{
			// Arrange.
			var help = new StructuralizedHelp(Enumerable.Empty<IOptionInfo>(), "my command");

			// Assert.
			Assert.Throws<ArgumentException>(() => help.GetDescription("option"));
		}

		[Fact]
		public void GetDescription_ShortOption_WorksCorrectly()
		{
			// Arrange.
			var optionMock = new Mock<IOption>();
			optionMock.Setup(x => x.Identifier).Returns("o");
			optionMock.Setup(x => x.Prefix).Returns("-");

			var optionInfoMock = new Mock<IOptionInfo>();
			optionInfoMock.Setup(x => x.Description).Returns("my description");
			optionInfoMock.Setup(x => x.OptionIdentifier).Returns(optionMock.Object);

			var help = new StructuralizedHelp(new[] { optionInfoMock.Object }, "my command");

			// Act.
			var description = help.GetDescription("-o");

			// Assert.
			Assert.Equal("my description", description);
		}

		[Fact]
		public void GetDescription_LongOption_WorksCorrectly()
		{
			// Arrange.
			var optionMock = new Mock<IOption>();
			optionMock.Setup(x => x.Identifier).Returns("option");
			optionMock.Setup(x => x.Prefix).Returns("--");

			var optionInfoMock = new Mock<IOptionInfo>();
			optionInfoMock.Setup(x => x.Description).Returns("my description");
			optionInfoMock.Setup(x => x.OptionIdentifier).Returns(optionMock.Object);

			var help = new StructuralizedHelp(new[] { optionInfoMock.Object }, "my command");

			// Act.
			var description = help.GetDescription("--option");

			// Assert.
			Assert.Equal("my description", description);
		}

		[Fact]
		public void Stringify_Aliases_WorksCorrectly()
		{
			// Arrange.
			var optionMock1 = new Mock<IOption>();
			optionMock1.Setup(x => x.Identifier).Returns("o");
			optionMock1.Setup(x => x.Prefix).Returns("-");

			var optionInfoMock1 = new Mock<IOptionInfo>();
			optionInfoMock1.Setup(x => x.Description).Returns("my description");
			optionInfoMock1.Setup(x => x.OptionIdentifier).Returns(optionMock1.Object);

			var optionMock2 = new Mock<IOption>();
			optionMock2.Setup(x => x.Identifier).Returns("option");
			optionMock2.Setup(x => x.Prefix).Returns("--");

			var optionInfoMock2 = new Mock<IOptionInfo>();
			optionInfoMock2.Setup(x => x.Description).Returns("my description");
			optionInfoMock2.Setup(x => x.OptionIdentifier).Returns(optionMock2.Object);

			var help = new StructuralizedHelp(new[] { optionInfoMock1.Object, optionInfoMock2.Object }, "my command");

			// Act.
			var description = help.Stringify();

			// Assert.
			Assert.Contains("\"-o\", \"--option\": my description", description);
		}
	}
}
