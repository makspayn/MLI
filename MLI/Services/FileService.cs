using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MLI.Data;

namespace MLI.Services
{
	public static class FileService
	{
		private static string fileName = "";

		public static void NewFile()
		{
			fileName = "";
			KnowledgeBase.Clear();
		}

		public static void OpenFile()
		{
			OpenFileDialog dlgOpen = new OpenFileDialog();
			dlgOpen.Filter = @"Файлы базы знаний (*.kb)|*.kb";
			dlgOpen.ShowDialog();
			if (string.IsNullOrEmpty(dlgOpen.FileName))
			{
				return;
			}
			fileName = dlgOpen.FileName;
			OpenXml();
		}

		public static void SaveFile()
		{
			if (string.IsNullOrEmpty(fileName))
			{
				SaveAsFile();
			}
			else
			{
				SaveXml();
			}
		}

		public static void SaveAsFile()
		{
			SaveFileDialog dlgSave = new SaveFileDialog();
			dlgSave.Filter = @"Файлы базы знаний (*.kb)|*.kb";
			dlgSave.ShowDialog();
			if (string.IsNullOrEmpty(dlgSave.FileName))
			{
				return;
			}
			fileName = dlgSave.FileName;
			SaveXml();
		}

		private static void OpenXml()
		{
			XmlDocument document = new XmlDocument();
			document.Load(fileName);
			if (document.DocumentElement == null)
			{
				throw new XmlException(@"Ошибка чтения файла");
			}
			List<string> facts = new List<string>();
			List<string> rules = new List<string>();
			List<string> conclusions = new List<string>();
			foreach (XmlNode node in document.DocumentElement)
			{
				switch (node.Name)
				{
					case "Fact":
						facts.Add(node.InnerText);
						break;
					case "Rule":
						rules.Add(node.InnerText);
						break;
					case "Conclusion":
						conclusions.Add(node.InnerText);
						break;
				}
			}
			KnowledgeBase.Clear();
			KnowledgeBase.Facts.AddRange(facts);
			KnowledgeBase.Rules.AddRange(rules);
			KnowledgeBase.Conclusions.AddRange(conclusions);
		}

		private static void CreateXml()
		{
			XmlTextWriter textWritter = new XmlTextWriter(fileName, Encoding.UTF8);
			textWritter.WriteStartDocument();
			textWritter.WriteStartElement("head");
			textWritter.WriteEndElement();
			textWritter.Close();
		}

		private static void SaveXml()
		{
			CreateXml();
			XmlDocument document = new XmlDocument();
			document.Load(fileName);
			foreach (string fact in KnowledgeBase.Facts)
			{
				XmlNode element = document.CreateElement("Fact");
				element.InnerText = fact;
				document.DocumentElement?.AppendChild(element);
			}
			foreach (string rule in KnowledgeBase.Rules)
			{
				XmlNode element = document.CreateElement("Rule");
				element.InnerText = rule;
				document.DocumentElement?.AppendChild(element);
			}
			foreach (string conclusion in KnowledgeBase.Conclusions)
			{
				XmlNode element = document.CreateElement("Conclusion");
				element.InnerText = conclusion;
				document.DocumentElement?.AppendChild(element);
			}
			document.Save(fileName);
		}
	}
}