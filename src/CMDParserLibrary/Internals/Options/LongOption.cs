namespace CMDParser.Internals.Options
{
	/// <summary>
	/// Implementation of long option.
	/// </summary>
	internal class LongOption : Option
	{
		/// <summary>
		/// Prefix of the long option.
		/// </summary>
		public const string OptionPrefix = "--";

		/// <summary>
		/// Assignment operator used to assign the argument value
		/// to the option.
		/// </summary>
		public const string AssignmentOperator = "=";

		/// <inheritdoc/>
		public override string Prefix => OptionPrefix;

		/// <inheritdoc/>
		public LongOption(string id) : base(id)
		{
		}
	}
}
