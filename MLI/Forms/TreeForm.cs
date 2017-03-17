using System.Windows.Forms;
using MLI.Services;

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

		private void TreeForm_Load(object sender, System.EventArgs e)
		{
			LoadStatistics();
		}

		private void LoadStatistics()
		{
			tree.Nodes.Clear();
			tree.BeginUpdate();
			foreach (StatElement statElement in StatisticsService.GetStatistics())
			{
				switch (statElement.ProcessKind)
				{
					case "V":
						tree.Nodes.Add(statElement.ProcessFullName);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes.Add(statElement.InputData);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes.Add(statElement.StatusData);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes.Add(statElement.ResultData);
						break;
					case "N":
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes.Add(statElement.ProcessFullName);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes.Add(statElement.InputData);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes.Add(statElement.StatusData);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes.Add(statElement.ResultData);
						break;
					case "M":
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes.Add(statElement.ProcessFullName);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes[3 + statElement.ProcessMIndex - 1].Nodes.Add(statElement.InputData);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes[3 + statElement.ProcessMIndex - 1].Nodes.Add(statElement.StatusData);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes[3 + statElement.ProcessMIndex - 1].Nodes.Add(statElement.ResultData);
						break;
					case "U":
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes[3 + statElement.ProcessMIndex - 1].Nodes.Add(statElement.ProcessFullName);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes[3 + statElement.ProcessMIndex - 1].Nodes[3 + statElement.ProcessUIndex - 1].Nodes.Add(statElement.InputData);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes[3 + statElement.ProcessMIndex - 1].Nodes[3 + statElement.ProcessUIndex - 1].Nodes.Add(statElement.StatusData);
						tree.Nodes[statElement.ProcessVIndex - 1].Nodes[3 + statElement.ProcessNIndex - 1].Nodes[3 + statElement.ProcessMIndex - 1].Nodes[3 + statElement.ProcessUIndex - 1].Nodes.Add(statElement.ResultData);
						break;
				}
			}
			tree.EndUpdate();
		}
	}
}