using System.Windows.Forms;

namespace MLI.Forms
{
	public partial class HelpForm : Form
	{
		private static HelpForm instance;

		private HelpForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new HelpForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new HelpForm();
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