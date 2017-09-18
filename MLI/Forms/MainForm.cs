using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MLI.Data;
using MLI.Services;

namespace MLI.Forms
{
	public partial class MainForm : Form
	{
		private static MainForm instance;
		private Machine.Machine machine;

		private MainForm()
		{
			CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
		}

		public static MainForm GetInstance()
		{
			return instance ?? (instance = new MainForm());
		}

		public void MachineCompleteEventHandler(string message)
		{
			rtbLog.Lines = LogService.GetLog().ToArray();
			SwitchState(true);
			MessageBox.Show(message);
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
			catch (Exception exception)
			{
				LogService.Error($"Возникла ошибка при чтении файла!\n{exception}");
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
			catch (Exception exception)
			{
				LogService.Error($"Возникла ошибка при сохранении файла!\n{exception}");
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
			catch (Exception exception)
			{
				LogService.Error($"Возникла ошибка при сохранении файла!\n{exception}");
				MessageBox.Show(@"Возникла ошибка при сохранении файла!");
			}
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void menuItemRun_Click(object sender, System.EventArgs e)
		{
			LogService.Debug("Создание машины");
			FillKnowledgeBase();
			try
			{
				machine = CreateMachine();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return;
			}
			SwitchState(false);
			LogService.Debug("Запуск машины");
			LogService.StartLog();
			machine.Run();
		}

		private void menuItemStop_Click(object sender, EventArgs e)
		{
			LogService.Debug("Остановка машины");
			machine.Stop();
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

		private void menuItemCommonSettings_Click(object sender, EventArgs e)
		{
			CommonSettingsForm.ShowForm();
		}

		private void menuItemCommandSettings_Click(object sender, EventArgs e)
		{
			CommandSettingsForm.ShowForm();
		}

		private void menuItemHelp_Click(object sender, System.EventArgs e)
		{
			HelpForm.ShowForm();
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			AboutForm.ShowForm();
		}

		private void btnRecommend_Click(object sender, EventArgs e)
		{
			List<Sequence> facts;
			List<Sequence> rules;
			List<Sequence> conclusions;
			FillKnowledgeBase();
			try
			{
				PrepareKnowledgeBase(out facts, out rules, out conclusions);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return;
			}
			Tuple<int, int> recommendation = RecommendService.GetRecommendation(facts, rules, conclusions);
			MessageBox.Show($"Рекомендуется использовать:\n{recommendation.Item1} ИБ\n{recommendation.Item2} БУ");
		}

		private void btnClearLog_Click(object sender, EventArgs e)
		{
			rtbLog.Clear();
		}

		private void ReadKnowledgeBase()
		{
			rtbFacts.Lines = KnowledgeBase.Facts.ToArray();
			rtbRules.Lines = KnowledgeBase.Rules.ToArray();
			rtbConclusions.Lines = KnowledgeBase.Conclusions.ToArray();
		}

		private void FillKnowledgeBase()
		{
			KnowledgeBase.Clear();
			foreach (string fact in rtbFacts.Lines.Where(fact => !string.IsNullOrWhiteSpace(fact)))
			{
				KnowledgeBase.Facts.Add(fact);
			}
			foreach (string rule in rtbRules.Lines.Where(rule => !string.IsNullOrWhiteSpace(rule)))
			{
				KnowledgeBase.Rules.Add(rule);
			}
			foreach (string conclusion in rtbConclusions.Lines.Where(conclusion => !string.IsNullOrWhiteSpace(conclusion)))
			{
				KnowledgeBase.Conclusions.Add(conclusion);
			}
		}

		private Machine.Machine CreateMachine()
		{
			List<Sequence> facts;
			List<Sequence> rules;
			List<Sequence> conclusions;
			PrepareKnowledgeBase(out facts, out rules, out conclusions);
			return new Machine.Machine(facts, rules, conclusions);
		}

		private void PrepareKnowledgeBase(out List<Sequence> facts, out List<Sequence> rules, out List<Sequence> conclusions)
		{
			facts = new List<Sequence>();
			rules = new List<Sequence>();
			conclusions = new List<Sequence>();
			try
			{
				if (KnowledgeBase.Facts.Count != 0)
				{
					facts.AddRange(KnowledgeBase.Facts.Select(fact => new Sequence(fact)));
				}
				else
				{
					throw new Exception("Факты отсутствуют!");
				}
				foreach (Sequence fact in facts)
				{
					fact.ValidateFact();
				}
			}
			catch (Exception exception)
			{
				LogService.Error("Создание машины прервано. Ошибка при создании фактов!");
				throw new Exception($"Ошибка при создании фактов!\n{exception.Message}");
			}
			try
			{
				if (KnowledgeBase.Rules.Count != 0)
				{
					rules.AddRange(KnowledgeBase.Rules.Select(rule => new Sequence(rule)));
				}
				else
				{
					throw new Exception("Правила отсутствуют!");
				}
			}
			catch (Exception exception)
			{
				LogService.Error("Создание машины прервано. Ошибка при создании правил!");
				throw new Exception($"Ошибка при создании правил!\n{exception.Message}");
			}
			try
			{
				if (KnowledgeBase.Rules.Count != 0)
				{
					conclusions.AddRange(KnowledgeBase.Conclusions.Select(conclusion => new Sequence(conclusion)));
				}
				else
				{
					throw new Exception("Выводимые правила отсутствуют!");
				}
			}
			catch (Exception exception)
			{
				LogService.Error("Создание машины прервано. Ошибка при создании выводимых правил!");
				throw new Exception($"Ошибка при создании выводимых правил!\n{exception.Message}");
			}
		}

		private void SwitchState(bool state)
		{
			menuItemRun.Enabled = state;
			menuItemStop.Enabled = !state;
			rtbFacts.Enabled = state;
			rtbRules.Enabled = state;
			rtbConclusions.Enabled = state;
			btnRecommend.Enabled = state;
		}
	}
}