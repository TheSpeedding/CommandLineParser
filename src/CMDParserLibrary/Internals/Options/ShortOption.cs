namespace CMDParser.Internals.Options
{
	/// <summary>
	/// Implementation of the short option.
	/// </summary>
	internal class ShortOption : Option
	{
		/// <summary>
		/// Prefix of the short option.
		/// </summary>
		public const string OptionPrefix = "-";

		/// <inheritdoc/>
		public override string Prefix => OptionPrefix;

		/// <inheritdoc/>
		public ShortOption(string id) : base(id)
		{
		}
	}
}
