using System.Windows.Forms;
using MLI.Services;

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

			dgStatistics.Rows[1].Cells[1].Value = StatisticsService.ProcessVCount;
			dgStatistics.Rows[2].Cells[1].Value = StatisticsService.ProcessNCount;
			dgStatistics.Rows[3].Cells[1].Value = StatisticsService.ProcessMCount;
			dgStatistics.Rows[4].Cells[1].Value = StatisticsService.ProcessUCount;
		}
	}
}