using System.Windows.Forms;
using static MLI.Services.StatisticsService;
using static MLI.Services.SettingsService;

namespace MLI.Forms
{
	public partial class StatisticsForm : Form
	{
		private static StatisticsForm instance;

		private StatisticsForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new StatisticsForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new StatisticsForm();
				}
				else
				{
					instance.Focus();
					return;
				}
			}
			instance.Show();
		}

		private void StatisticsForm_Load(object sender, System.EventArgs e)
		{
			LoadStatistics();
		}

		private void LoadStatistics()
		{
			dgStatistics.Rows.Clear();
			dgStatistics.Rows.Add("Время работы");
			dgStatistics.Rows.Add("Количество V-процессов");
			dgStatistics.Rows.Add("Количество N-процессов");
			dgStatistics.Rows.Add("Количество M-процессов");
			dgStatistics.Rows.Add("Количество U-процессов");
			dgStatistics.Rows.Add("Время работы Блока Управления");
			dgStatistics.Rows.Add("Время работы Операционного Модуля");
			dgStatistics.Rows.Add("Время работы Модуля Унификации");
			dgStatistics.Rows.Add("Загрузка Исполнительных Блоков");
			dgStatistics.Rows.Add("Загрузка Блоков Унификации");

			dgStatistics.Rows[0].Cells[1].Value = $"{TotalTime} нс";
			dgStatistics.Rows[1].Cells[1].Value = ProcessVCount;
			dgStatistics.Rows[2].Cells[1].Value = ProcessNCount;
			dgStatistics.Rows[3].Cells[1].Value = ProcessMCount;
			dgStatistics.Rows[4].Cells[1].Value = ProcessUCount;
			dgStatistics.Rows[5].Cells[1].Value = $"{TotalTimeControlUnit} нс ({(double)TotalTimeControlUnit / TotalTime * 100.0}%)";
			dgStatistics.Rows[6].Cells[1].Value = $"{TotalTimeProcessUnit} нс ({(double)TotalTimeProcessUnit / TotalTime * 100.0}%)";
			dgStatistics.Rows[7].Cells[1].Value = $"{TotalTimeUnifUnit} нс ({(double)TotalTimeUnifUnit / TotalTime * 100.0}%)";
			dgStatistics.Rows[8].Cells[1].Value = $"{LoadProcessExecUnit} ({(double) LoadProcessExecUnit / ExecUnitCount * 100.0}%)";
			dgStatistics.Rows[9].Cells[1].Value = $"{LoadProcessUnifUnit} ({(double) LoadProcessUnifUnit / UnifUnitCount * 100.0}%)";
		}
	}
}