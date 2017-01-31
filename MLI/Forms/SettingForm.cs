using System.Windows.Forms;
using MLI.Services;

namespace MLI.Forms
{
	public partial class SettingForm : Form
	{
		private static SettingForm instance;

		private SettingForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new SettingForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new SettingForm();
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
			numExecUnitCount.Value = SettingService.ExecUnitCount;
			numUnifUnitCount.Value = SettingService.UnifUnitCount;
			numTickLength.Value = SettingService.TickLength;
			switch (SettingService.InfoLevel)
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
			SettingService.ExecUnitCount = (int)numExecUnitCount.Value;
			SettingService.UnifUnitCount = (int)numUnifUnitCount.Value;
			SettingService.TickLength = (int)numTickLength.Value;
			if (rbV.Checked) SettingService.InfoLevel = 0;
			if (rbN.Checked) SettingService.InfoLevel = 1;
			if (rbM.Checked) SettingService.InfoLevel = 2;
			if (rbU.Checked) SettingService.InfoLevel = 3;
		}
	}
}