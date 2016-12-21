using System.Text.RegularExpressions;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Domain
{
	public static class AggregateHelper
	{
		public const string StreamIdPrefix = "Aggregate";

		public static ObservableFilter BuildObservableFilter()
		{
			var regEx = new Regex($"{StreamIdPrefix}.*");
			return new ObservableFilter(regEx);
		}

		public static string GetAggregateStreamIdPrefix(string aggregateType)
		{
			return $"{StreamIdPrefix}.{aggregateType}";
		}
	}
}
