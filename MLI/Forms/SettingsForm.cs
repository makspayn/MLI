using System.Windows.Forms;
using MLI.Services;
using NLog;

namespace MLI.Forms
{
	public partial class SettingsForm : Form
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private static SettingsForm instance;

		private SettingsForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new SettingsForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new SettingsForm();
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
			numExecUnitCount.Value = SettingsService.ExecUnitCount;
			numUnifUnitCount.Value = SettingsService.UnifUnitCount;
			numTickLength.Value = SettingsService.TickLength;
			switch (SettingsService.InfoLevel)
			{
				case 0:
					rbV.Checked = true;
					break;
				case 1:
					rbN.Checked = true;
					break;
				case 2:
					rbM.Checked = true;
					break;
				case 3:
					rbU.Checked = true;
					break;
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SettingsService.ExecUnitCount = (int)numExecUnitCount.Value;
			SettingsService.UnifUnitCount = (int)numUnifUnitCount.Value;
			SettingsService.TickLength = (int)numTickLength.Value;
			if (rbV.Checked) SettingsService.InfoLevel = 0;
			if (rbN.Checked) SettingsService.InfoLevel = 1;
			if (rbM.Checked) SettingsService.InfoLevel = 2;
			if (rbU.Checked) SettingsService.InfoLevel = 3;
			logger.Info("Установлены новые настройки:\n" +
				$"Число исполнительных блоков: {SettingsService.ExecUnitCount}\n" +
				$"Число блоков унификации: {SettingsService.UnifUnitCount}\n" +
				$"Длина такта (нс): {SettingsService.TickLength}\n" +
				$"Уровень сбора информации: {SettingsService.InfoLevel}");
		}
	}
}