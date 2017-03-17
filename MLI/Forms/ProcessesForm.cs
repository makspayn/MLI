using System.Collections.Generic;
using System.Windows.Forms;
using MLI.Services;

namespace MLI.Forms
{
	public partial class ProcessesForm : Form
	{
		private static ProcessesForm instance;

		private ProcessesForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new ProcessesForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new ProcessesForm();
				}
				else
				{
					instance.Focus();
					return;
				}
			}
			instance.Show();
		}

		private void ProcessesForm_Load(object sender, System.EventArgs e)
		{
			LoadStatistics();
		}

		private void LoadStatistics()
		{
			int currentRow = 0;
			dgProcesses.Rows.Clear();
			foreach (StatElement statElement in StatisticsService.GetStatistics())
			{
				dgProcesses.Rows.Add(statElement.GetExecutions().Count);
				dgProcesses.Rows[currentRow].Cells[0].Value = $"Процесс {statElement.ProcessFullName}";
				foreach (Execution execution in statElement.GetExecutions())
				{
					dgProcesses.Rows[currentRow].Cells[1].Value = execution.ProcessUnitName;
					dgProcesses.Rows[currentRow].Cells[2].Value = $"{execution.WaitTime} нс";
					dgProcesses.Rows[currentRow].Cells[3].Value = $"{execution.ReadyTime} нс";
					dgProcesses.Rows[currentRow].Cells[4].Value = $"{execution.RunTime} нс";
					currentRow++;
				}
				
			}
		}
	}
}