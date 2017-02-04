using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MLI.Data;
using NLog;

namespace MLI.Services
{
	public static class FileService
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private static string fileName = "";

		public static void NewFile()
		{
			fileName = "";
			KnowledgeBase.Clear();
			logger.Debug(@"Создан новый файл");
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
			logger.Debug($"Чтение файла {fileName}");
			XmlDocument document = new XmlDocument();
			document.Load(fileName);
			if (document.DocumentElement == null)
			{
				throw new XmlException(@"Отсутствует DocumentElement");
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
						logger.Debug($"Прочитан факт {node.InnerText}");
						break;
					case "Rule":
						rules.Add(node.InnerText);
						logger.Debug($"Прочитано правило {node.InnerText}");
						break;
					case "Conclusion":
						conclusions.Add(node.InnerText);
						logger.Debug($"Прочитано выводимое правило {node.InnerText}");
						break;
				}
			}
			KnowledgeBase.Clear();
			KnowledgeBase.Facts.AddRange(facts);
			KnowledgeBase.Rules.AddRange(rules);
			KnowledgeBase.Conclusions.AddRange(conclusions);
			logger.Debug($"Файл {fileName} успешно прочитан");
		}

		private static void CreateXml()
		{
			logger.Debug($"Создание xml {fileName}");
			XmlTextWriter textWritter = new XmlTextWriter(fileName, Encoding.UTF8);
			textWritter.WriteStartDocument();
			textWritter.WriteStartElement("head");
			textWritter.WriteEndElement();
			textWritter.Close();
			logger.Debug($"Файл {fileName} успешно создан");
		}

		private static void SaveXml()
		{

			CreateXml();
			logger.Debug($"Сохранение файла {fileName}");
			XmlDocument document = new XmlDocument();
			document.Load(fileName);
			foreach (string fact in KnowledgeBase.Facts)
			{
				XmlNode element = document.CreateElement("Fact");
				element.InnerText = fact;
				document.DocumentElement?.AppendChild(element);
				logger.Debug($"Сохранен факт {fact}");
			}
			foreach (string rule in KnowledgeBase.Rules)
			{
				XmlNode element = document.CreateElement("Rule");
				element.InnerText = rule;
				document.DocumentElement?.AppendChild(element);
				logger.Debug($"Сохранено правило {rule}");
			}
			foreach (string conclusion in KnowledgeBase.Conclusions)
			{
				XmlNode element = document.CreateElement("Conclusion");
				element.InnerText = conclusion;
				document.DocumentElement?.AppendChild(element);
				logger.Debug($"Сохранено выводимое правило {conclusion}");
			}
			document.Save(fileName);
			logger.Debug($"Файл {fileName} успешно сохранен");
		}
	}
}