using System.Collections.Generic;
using MLI.Data;

namespace MLI.Machine
{
	public class KnowledgeBase
	{
		public List<Sequence> facts = new List<Sequence>();
		public List<Sequence> rules = new List<Sequence>();
		public List<Sequence> conclusions = new List<Sequence>();

		public void SetFacts(List<Sequence> facts)
		{
			this.facts.Clear();
			this.facts.AddRange(facts);
		}

		public void SetRules(List<Sequence> rules)
		{
			this.rules.Clear();
			this.rules.AddRange(rules);
		}

		public void SetConclusions(List<Sequence> conclusions)
		{
			this.conclusions.Clear();
			this.conclusions.AddRange(conclusions);
		}
	}
}