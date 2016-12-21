using System;
using System.Text.RegularExpressions;

namespace PVDevelop.UCoach.Shared.Observing
{
	public class ObservableFilter
	{
		public Regex FilterRegEx { get; }

		public ObservableFilter(Regex filterRegEx)
		{
			FilterRegEx = filterRegEx;
		}

		public bool Match(string eventCategory)
		{
			if (string.IsNullOrWhiteSpace(eventCategory))
				throw new ArgumentException("Not set", nameof(eventCategory));

			if (FilterRegEx == null) return true;

			return FilterRegEx.IsMatch(eventCategory);
		}
	}
}
