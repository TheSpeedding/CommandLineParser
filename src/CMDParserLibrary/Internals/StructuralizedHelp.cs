using CMDParser.Internals.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMDParser.Internals
{
	internal class StructuralizedHelp : IStructuralizedHelp
	{
		// Mapping between the option identifier and its description
		private readonly IReadOnlyDictionary<string, string> _descriptions;

		public StructuralizedHelp(IEnumerable<IOptionInfo> optionInfos)
		{
			_descriptions = optionInfos
				.Where(x => !string.IsNullOrEmpty(x.Description))
				.ToDictionary(x => x.OptionIdentifier.Identifier, x => x.Description);
		}

		public string GetDescription(string optionIdentifier)
		{
			if (!_descriptions.ContainsKey(optionIdentifier))
				throw new ArgumentException($"There is no description for \"{ optionIdentifier }\" option.");

			return _descriptions[optionIdentifier];
		}

		public string Stringify()
		{
			// Group options by their descriptions.
			var groupedOptions = _descriptions.GroupBy(x => x.Value, x => x.Key);

			var sb = new StringBuilder();

			foreach (var groupedOption in groupedOptions)
				sb.AppendLine($"{ string.Join(", ", groupedOption) }: { groupedOption.Key }");

			return sb.ToString();
		}
	}
}
