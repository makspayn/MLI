using System.Windows.Forms;

namespace MLI.Forms
{
	public partial class AboutForm : Form
	{
		private static AboutForm instance;

		private AboutForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new AboutForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new AboutForm();
				}
				else
				{
					instance.Focus();
					return;
				}
			}
			instance.Show();
		}
	}
}