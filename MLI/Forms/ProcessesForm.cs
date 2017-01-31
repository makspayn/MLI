using System.Windows.Forms;

namespace MLI.Forms
{
	public partial class ProcessesForm : Form
	{
		private static ProcessesForm instance;

		private ProcessesForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new ProcessesForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new ProcessesForm();
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