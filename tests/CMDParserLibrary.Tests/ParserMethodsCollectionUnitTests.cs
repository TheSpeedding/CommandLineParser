using CMDParser.Internals;
using System;
using Xunit;

namespace CMDParser.Tests
{
	public class ParserMethodsCollectionUnitTests
	{
		[Fact]
		public void Constructor_DefaultTypesRegistered()
		{
			// Arrange.
			var collection = new ParserMethodsCollection();

			// Assert.
			Assert.True(collection.CanParse<int>());
			Assert.True(collection.CanParse<uint>());
			Assert.True(collection.CanParse<long>());
			Assert.True(collection.CanParse<ulong>());
			Assert.True(collection.CanParse<string>());
			Assert.True(collection.CanParse<bool>());
			Assert.True(collection.CanParse<sbyte>());
			Assert.True(collection.CanParse<byte>());
		}

		[Fact]
		public void RegisterParserAndParse_AddNew_WorksCorrectly()
		{
			// Arrange.
			var collection = new ParserMethodsCollection();

			// Act.
			collection.RegisterParseMethod(x => x.Split('.'));
			var parsed = collection.Parse<string[]>("a.b.c");

			// Assert.
			Assert.True(collection.CanParse<string[]>());
			Assert.Contains("a", parsed);
			Assert.Contains("b", parsed);
			Assert.Contains("c", parsed);
		}

		[Fact]
		public void RegisterParserAndParse_OverwriteExisting_WorksCorrectly()
		{
			// Arrange.
			var collection = new ParserMethodsCollection();

			// Act.
			collection.RegisterParseMethod(x => int.Parse(x) + 2);
			var parsed = collection.Parse<int>("5");

			// Assert.
			Assert.True(collection.CanParse<int>());
			Assert.Equal(7, parsed);
		}

		[Fact]
		public void Parse_TypeNotRegistered_ThrowsException()
		{
			// Arrange.
			var collection = new ParserMethodsCollection();

			// Assert. 
			Assert.Throws<NotSupportedException>(() => collection.Parse<int[]>("5,2,4"));
		}
	}
}
