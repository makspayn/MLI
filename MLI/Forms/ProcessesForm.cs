using System.ComponentModel;
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
				foreach (Execution execution in statElement.GetExecutions())
				{
					dgProcesses.Rows[currentRow].Cells[0].Value = $"Процесс {statElement.ProcessFullName}";
					dgProcesses.Rows[currentRow].Cells[1].Value = execution.ProcessUnitName;
					dgProcesses.Rows[currentRow].Cells[2].Value = $"{execution.WaitTime * SettingsService.TickLength} нс";
					dgProcesses.Rows[currentRow].Cells[3].Value = $"{execution.ReadyTime * SettingsService.TickLength} нс";
					dgProcesses.Rows[currentRow].Cells[4].Value = $"{execution.RunTime * SettingsService.TickLength} нс";
					dgProcesses.Rows[currentRow].Cells[5].Value = $"{execution.StartTime * SettingsService.TickLength} нс";
					dgProcesses.Rows[currentRow].Cells[6].Value = $"{execution.EndTime * SettingsService.TickLength} нс";
					currentRow++;
				}
			}
			dgProcesses.Sort(dgProcesses.Columns[5], ListSortDirection.Ascending);
		}

		private void dgProcesses_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
		{
			e.SortResult = int.Parse(e.CellValue1.ToString().Split(' ')[0]) - int.Parse(e.CellValue2.ToString().Split(' ')[0]);

			if (e.SortResult == 0)
			{
				e.SortResult = string.Compare(
					dgProcesses.Rows[e.RowIndex1].Cells[0].Value.ToString(),
					dgProcesses.Rows[e.RowIndex2].Cells[0].Value.ToString());
			}
			e.Handled = true;
		}
	}
}