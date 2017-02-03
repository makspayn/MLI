using System.Collections.Generic;

namespace MLI.Data
{
	public static class KnowledgeBase
	{
		public static List<string> Facts = new List<string>();
		public static List<string> Rules = new List<string>();
		public static List<string> Conclusions = new List<string>();

		public static void Clear()
		{
			Facts.Clear();
			Rules.Clear();
			Conclusions.Clear();
		}
	}
}