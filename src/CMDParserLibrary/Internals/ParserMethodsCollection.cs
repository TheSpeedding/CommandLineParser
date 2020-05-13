using System;
using System.Collections.Generic;

namespace CMDParser.Internals
{
	/// <summary>
	/// A collection of parse methods.
	/// </summary>
	internal class ParserMethodsCollection : IParserMethodsView
	{
		private readonly IDictionary<Type, Func<string, object>> _dic = new Dictionary<Type, Func<string, object>>();

		/// <summary>
		/// Creates a new <see cref="ParserMethodsCollection"/> and
		/// registers common parse methods, e.g., <see cref="int.Parse(string)"/>.
		/// </summary>
		public ParserMethodsCollection()
		{
			RegisterParseMethod(s => s); // Identity parser for strings.

			RegisterParseMethod(int.Parse);
			RegisterParseMethod(uint.Parse);
			RegisterParseMethod(long.Parse);
			RegisterParseMethod(ulong.Parse);
			RegisterParseMethod(byte.Parse);
			RegisterParseMethod(sbyte.Parse);
			RegisterParseMethod(bool.Parse);
		}

		/// <summary>
		/// Decides whether <typeparamref name="TParsedType"/> can be parsed or not.
		/// </summary>
		/// <typeparam name="TParsedType">A type to be parsed.</typeparam>
		/// <returns><see langword="true"/> if <see cref="ParserMethodsCollection"/> can parse <typeparamref name="TParsedType"/>,
		/// otherwise <see langword="false"/>.</returns>
		public bool CanParse<TParsedType>() => _dic.ContainsKey(typeof(TParsedType));

		/// <summary>
		/// Registers <paramref name="parser"/> method.
		/// </summary>
		/// <typeparam name="TParsedType">A type to be parsed.</typeparam>
		/// <param name="parser">Parser method.</param>
		public void RegisterParseMethod<TParsedType>(Func<string, TParsedType> parser)
		{
			if (!_dic.ContainsKey(typeof(TParsedType)))
				_dic.Add(typeof(TParsedType), x => parser(x)!);
			else
				_dic[typeof(TParsedType)] = x => parser(x)!; // If contained yet, overwrite it and start using the new parse method.
		}

		/// <inheritdoc/>
		/// <exception cref="NotSupportedException">Thrown when the collection does not support parsing of <typeparamref name="TParsedType"/>.</exception>
		public TParsedType Parse<TParsedType>(string input)
		{
			if (!CanParse<TParsedType>())
				throw new NotSupportedException($"The collection does not support parsing of { typeof(TParsedType) } type.");

			return (TParsedType)_dic[typeof(TParsedType)](input);
		}
	}
}
