using System.Windows.Forms;

namespace MLI
{
	public partial class HelpForm : Form
	{
		private static HelpForm instance = null;

		public HelpForm()
		{
			InitializeComponent();
		}

		public static HelpForm getInstance()
		{
			if (instance == null)
			{
				instance = new HelpForm();
			}
			return instance;
		}
	}
}