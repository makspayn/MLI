using System.Linq;
using System.Windows.Forms;
using MLI.Data;
using MLI.Services;

namespace MLI.Forms
{
	public partial class MainForm : Form
	{
		private static MainForm instance;

		private MainForm()
		{
			InitializeComponent();
		}

		public static MainForm GetInstance()
		{
			return instance ?? (instance = new MainForm());
		}

		private void menuItemTree_Click(object sender, System.EventArgs e)
		{
			TreeForm.ShowForm();
		}

		private void menuItemExecUnits_Click(object sender, System.EventArgs e)
		{
			ExecUnitsForm.ShowForm();
		}

		private void menuItemProcesses_Click(object sender, System.EventArgs e)
		{
			ProcessesForm.ShowForm();
		}

		private void menuItemStatistics_Click(object sender, System.EventArgs e)
		{
			StatisticsForm.ShowForm();
		}

		private void menuItemSettings_Click(object sender, System.EventArgs e)
		{
			SettingsForm.ShowForm();
		}

		private void menuItemHelp_Click(object sender, System.EventArgs e)
		{
			HelpForm.ShowForm();
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			AboutForm.ShowForm();
		}

		private void menuItemNewFile_Click(object sender, System.EventArgs e)
		{
			FileService.NewFile();
			ReadKnowledgeBase();
		}

		private void menuItemOpenFile_Click(object sender, System.EventArgs e)
		{
			try
			{
				FileService.OpenFile();
			}
			catch
			{
				MessageBox.Show(@"Возникла ошибка при чтении файла!");
				return;
			}
			ReadKnowledgeBase();
		}

		private void menuItemSaveFile_Click(object sender, System.EventArgs e)
		{
			FillKnowledgeBase();
			try
			{
				FileService.SaveFile();
			}
			catch
			{
				MessageBox.Show(@"Возникла ошибка при сохранении файла!");
			}
		}

		private void menuItemSaveAsFile_Click(object sender, System.EventArgs e)
		{
			FillKnowledgeBase();
			try
			{
				FileService.SaveAsFile();
			}
			catch
			{
				MessageBox.Show(@"Возникла ошибка при сохранении файла!");
			}
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void ReadKnowledgeBase()
		{
			rtbFacts.Lines = KnowledgeBase.Facts.ToArray();
			rtbRules.Lines = KnowledgeBase.Rules.ToArray();
			rtbConclusions.Lines = KnowledgeBase.Conclusions.ToArray();
		}

		private void FillKnowledgeBase()
		{
			KnowledgeBase.Facts.Clear();
			foreach (string fact in rtbFacts.Lines.Where(fact => !string.IsNullOrWhiteSpace(fact)))
			{
				KnowledgeBase.Facts.Add(fact);
			}
			KnowledgeBase.Rules.Clear();
			foreach (string rule in rtbRules.Lines.Where(rule => !string.IsNullOrWhiteSpace(rule)))
			{
				KnowledgeBase.Rules.Add(rule);
			}
			KnowledgeBase.Conclusions.Clear();
			foreach (string conclusion in rtbConclusions.Lines.Where(conclusion => !string.IsNullOrWhiteSpace(conclusion)))
			{
				KnowledgeBase.Conclusions.Add(conclusion);
			}
		}
	}
}