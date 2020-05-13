using System;

namespace CMDParser.Internals.Options
{
	/// <summary>
	/// A common interface for the options extending the
	/// basic information about the option with actual
	/// implementation of the parser and callback which
	/// shall be called once the option is parsed successfully.
	/// </summary>
	/// <typeparam name="TParsedType">Type of the parsed instance.</typeparam>
	interface IOptionSetup<in TParsedType> : IOptionInfo, IParsable
	{
		/// <summary>
		/// Callback to be called once the option is parsed successfully.
		/// </summary>
		public Action<TParsedType> Callback { get; }
	}
}
