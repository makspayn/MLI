using System;
using System.Windows.Forms;
using MLI.Forms;
using MLI.Services;

namespace MLI
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			CommandService.InitCommands();
			Application.Run(MainForm.GetInstance());
		}
	}
}