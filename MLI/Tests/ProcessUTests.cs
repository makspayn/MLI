using MLI.Data;
using MLI.Machine;
using MLI.Method;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MLI.Tests
{
	[TestClass]
	public class ProcessUTests
	{
		[TestMethod]
		public void UnificationTest()
		{
			ProcessU processU = new ProcessU(null, 0, new Predicate("-P(x,y)"), new Predicate("-P(A,z)"));
			processU.Run(new ExecUnit("UU", 1, 1));
			Assert.AreEqual(ProcessU.ProcessUStatus.Complete, processU.GetProcessUStatus());
			Assert.AreEqual("x/A; y/z", processU.GetSubstitution().GetSubstitutions());
		}
	}
}