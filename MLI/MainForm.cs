using System.Windows.Forms;

namespace MLI
{
	public partial class MainForm : Form
	{
		private static MainForm instance = null;

		public MainForm()
		{
			InitializeComponent();
		}

		public static MainForm getInstance()
		{
			if (instance == null)
			{
				instance = new MainForm();
			}
			return instance;
		}
	}
}
