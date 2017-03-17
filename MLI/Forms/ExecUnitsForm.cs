using System.Linq;
using System.Windows.Forms;
using MLI.Services;

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

		private void btnShowLog_Click(object sender, System.EventArgs e)
		{
			rtbLog.Lines = (from statElement in StatisticsService.GetStatistics()
							where statElement.GetExecutions().Any(execution => execution.ProcessExecUnitNumber == numExecUnitNumber.Value)
							select $"Выполнен процесс {statElement.ProcessFullName}").ToArray();
		}
	}
}