using System.Text.RegularExpressions;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Saga
{
	public static class SagaHelper
	{
		public const string StreamIdPrefix = "Saga";

		public static ObservableFilter BuildObservableFilter()
		{
			var regEx = new Regex($"{StreamIdPrefix}.*");
			return new ObservableFilter(regEx);
		}
	}
}
