using System.Windows.Forms;

namespace MLI.Forms
{
	public partial class StatisticsForm : Form
	{
		private static StatisticsForm instance;

		private StatisticsForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new StatisticsForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new StatisticsForm();
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