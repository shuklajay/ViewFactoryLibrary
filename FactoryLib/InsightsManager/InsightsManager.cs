using System;
using Xamarin;

namespace FactoryLib
{
	public static class InsightsManager
	{
		public static string InsightsApiKey = string.Empty;

		public static bool IsEnabled {
			get {
				return !string.IsNullOrWhiteSpace (InsightsApiKey);
			}
		}

		public static void Report (Exception e, Insights.Severity severity = Insights.Severity.Warning)
		{
			if (!IsEnabled)
				return;
			Insights.Report (e, severity);
		}
	}
}