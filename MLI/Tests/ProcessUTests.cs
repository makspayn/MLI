using MLI.Data;
using MLI.Machine;
using MLI.Method;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml;

namespace MLI.Tests
{
	[TestClass]
	public class ProcessUTests
	{
		private class TestData
		{
			public string predicate1;
			public string predicate2;
			public string status;
			public string substitution;
		}

		private static string fileName = "ProcessUTests.xml";
		private List<TestData> testsData = new List<TestData>();

		[TestMethod]
		public void UnificationTest()
		{
			ReadTestsData();
			RunTests();
		}

		private void ReadTestsData()
		{
			XmlDocument document = new XmlDocument();
			document.Load(fileName);
			foreach (XmlNode test in document.DocumentElement["tests"])
			{
				testsData.Add(new TestData()
				{
					predicate1 = test["predicate1"].InnerText,
					predicate2 = test["predicate2"].InnerText,
					status = test["status"].InnerText,
					substitution = test["substitution"].InnerText
				});
			}
		}

		private void RunTests()
		{
			foreach (TestData testData in testsData)
			{
				ProcessU processU = new ProcessU(null, 0, new Predicate(testData.predicate1), new Predicate(testData.predicate2));
				processU.Run(new ExecUnit("EU", 1, new ReconfigurationUnit(1024, 8), new List<ProcessUnit>{new UnifUnit("UU", 1)}));
				Assert.AreEqual(testData.status, processU.GetProcessUStatus().ToString());
				Assert.AreEqual(testData.substitution, processU.GetSubstitution().GetSubstitutions());
			}
		}
	}
}