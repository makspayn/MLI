using System.Windows.Forms;

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
	}
}