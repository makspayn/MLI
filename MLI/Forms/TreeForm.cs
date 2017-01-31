using System.Windows.Forms;

namespace MLI.Forms
{
	public partial class TreeForm : Form
	{
		private static TreeForm instance;

		private TreeForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new TreeForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new TreeForm();
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