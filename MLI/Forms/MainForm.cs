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

		private void menuItemSettings_Click(object sender, System.EventArgs e)
		{
			SettingForm.ShowForm();
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