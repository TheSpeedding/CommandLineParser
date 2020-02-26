using System;
using System.Collections.Generic;
using System.Text;

namespace CMDParser
{
	class ParserMethodsCollection : IParserMethodsView
	{
		private readonly IDictionary<Type, Func<string, object>> _dic = new Dictionary<Type, Func<string, object>>();

		public ParserMethodsCollection()
		{
			RegisterParseMethod(s => s); // Identity parser for strings.
			RegisterParseMethod(x => int.Parse(x));
			RegisterParseMethod(x => uint.Parse(x));
			RegisterParseMethod(x => long.Parse(x));
			RegisterParseMethod(x => ulong.Parse(x));
			RegisterParseMethod(x => byte.Parse(x));
			RegisterParseMethod(x => sbyte.Parse(x));
			RegisterParseMethod(x => bool.Parse(x));
		}

		public void RegisterParseMethod<TParsedType>(Func<string, TParsedType> parser)
		{
			if (!_dic.ContainsKey(typeof(TParsedType)))
				_dic.Add(typeof(TParsedType), x => parser(x)!);
		}

		public TParsedType Parse<TParsedType>(string input)
		{
			if (!_dic.ContainsKey(typeof(TParsedType)))
				throw new NotSupportedException($"The collection does not support parsing of { typeof(TParsedType) } type.");

			return (TParsedType)_dic[typeof(TParsedType)](input);
		}
	}
}
