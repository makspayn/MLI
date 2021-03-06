﻿using System.Windows.Forms;
using MLI.Services;

namespace MLI.Forms
{
	public partial class CommonSettingsForm : Form
	{
		private static CommonSettingsForm instance;

		private CommonSettingsForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new CommonSettingsForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new CommonSettingsForm();
				}
				else
				{
					instance.Focus();
					return;
				}
			}
			instance.Show();
		}

		private void SettingsForm_Load(object sender, System.EventArgs e)
		{
			numUnitResourceCount.Value = SettingsService.UnitResourceCount;
			numTickLength.Value = SettingsService.TickLength;
			switch (SettingsService.InfoLevel)
			{
				case LogService.InfoLevel.ProcessV:
					rbV.Checked = true;
					break;
				case LogService.InfoLevel.ProcessN:
					rbN.Checked = true;
					break;
				case LogService.InfoLevel.ProcessM:
					rbM.Checked = true;
					break;
				case LogService.InfoLevel.ProcessU:
					rbU.Checked = true;
					break;
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SettingsService.UnitResourceCount = (int)numUnitResourceCount.Value;
			SettingsService.TickLength = (int)numTickLength.Value;
			if (rbV.Checked) SettingsService.InfoLevel = LogService.InfoLevel.ProcessV;
			if (rbN.Checked) SettingsService.InfoLevel = LogService.InfoLevel.ProcessN;
			if (rbM.Checked) SettingsService.InfoLevel = LogService.InfoLevel.ProcessM;
			if (rbU.Checked) SettingsService.InfoLevel = LogService.InfoLevel.ProcessU;
			LogService.Info("Установлены новые настройки:\n" +
			                $"Количество ресурсов: {SettingsService.UnitResourceCount}\n" +
			                $"Длина такта (нс): {SettingsService.TickLength}\n" +
			                $"Уровень сбора информации: {SettingsService.InfoLevel}");
		}
	}
}