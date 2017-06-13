using MLI.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml;

namespace MLI.Tests
{
	[TestClass]
	public class MinimizeTests
	{
		private class TestData
		{
			public string sequence1;
			public string sequence2;
		}

		private static string fileName = "MinimizeTests.xml";
		private List<TestData> testsData = new List<TestData>();

		[TestMethod]
		public void MinimizeTest()
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
					sequence1 = test["sequence1"].InnerText,
					sequence2 = test["sequence2"].InnerText
				});
			}
		}

		private void RunTests()
		{
			foreach (TestData testData in testsData)
			{
				Sequence sequence1 = new Sequence(testData.sequence1);
				Sequence sequence2 = new Sequence(testData.sequence2);
				Sequence result = Sequence.Minimize(sequence1);
				Assert.AreEqual(sequence2.ToString(), result.ToString());
			}
		}
	}
}