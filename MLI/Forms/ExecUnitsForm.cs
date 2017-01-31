using System.Windows.Forms;

namespace MLI.Forms
{
	public partial class ExecUnitsForm : Form
	{
		private static ExecUnitsForm instance;

		private ExecUnitsForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new ExecUnitsForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new ExecUnitsForm();
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