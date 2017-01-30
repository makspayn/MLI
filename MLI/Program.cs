using System;
using System.Windows.Forms;
using MLI.Forms;

namespace MLI
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(MainForm.GetInstance());
		}
	}
}