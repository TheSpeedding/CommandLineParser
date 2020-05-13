using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMDParser.Internals
{
	/// <inheritdoc/>
	internal class StructuralizedHelp : IStructuralizedHelp
	{
		// Mapping between the option identifier and its description.
		private readonly IReadOnlyDictionary<string, string> _descriptions;

		/// <inheritdoc/>
		public string CommandName { get; }

		/// <summary>
		/// Creates a new <see cref="StructuralizedHelp"/> instance.
		/// </summary>
		/// <param name="optionInfos">A collection of <see cref="IOptionInfo"/>
		/// instances.</param>
		/// <param name="commandName">Name of the command.</param>
		public StructuralizedHelp(IEnumerable<IOptionInfo> optionInfos, string commandName)
		{
			_descriptions = optionInfos
				.Where(x => !string.IsNullOrEmpty(x.Description))
				.ToDictionary(x => x.OptionIdentifier.Prefix + x.OptionIdentifier.Identifier, x => x.Description);
			CommandName = commandName;
		}

		/// <inheritdoc/>
		public string GetDescription(string optionIdentifier)
		{
			if (!_descriptions.ContainsKey(optionIdentifier))
				throw new ArgumentException($"There is no description for \"{ optionIdentifier }\" option.");

			return _descriptions[optionIdentifier];
		}

		/// <inheritdoc/>
		public string Stringify()
		{
			// Group options by their descriptions.
			var groupedOptions = _descriptions.GroupBy(x => x.Value, x => "\"" + x.Key + "\"");

			var sb = new StringBuilder();

			if (!string.IsNullOrEmpty(CommandName))
				sb.AppendLine(CommandName);

			foreach (var groupedOption in groupedOptions)
				sb.AppendLine($"{ string.Join(", ", groupedOption) }: { groupedOption.Key }");

			return sb.ToString();
		}
	}
}
