﻿using System.Windows.Forms;
using MLI.Services;
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

			dgStatistics.Rows[0].Cells[1].Value = $"{TotalTime * TickLength} нс";
			dgStatistics.Rows[1].Cells[1].Value = ProcessVCount;
			dgStatistics.Rows[2].Cells[1].Value = ProcessNCount;
			dgStatistics.Rows[3].Cells[1].Value = ProcessMCount;
			dgStatistics.Rows[4].Cells[1].Value = ProcessUCount;
			dgStatistics.Rows[5].Cells[1].Value = $"{TotalTimeControlUnit * TickLength} нс ({(double)TotalTimeControlUnit / TotalTime * 100.0}%)";
			dgStatistics.Rows[6].Cells[1].Value = $"{TotalTimeProcessUnit * TickLength} нс ({(double)TotalTimeProcessUnit / TotalTime * 100.0}%)";
			dgStatistics.Rows[7].Cells[1].Value = $"{TotalTimeUnifUnit * TickLength} нс ({(double)TotalTimeUnifUnit / TotalTime * 100.0}%)";
		}
	}
}