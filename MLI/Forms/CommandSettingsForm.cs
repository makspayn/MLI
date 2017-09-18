using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MLI.Data;
using MLI.Services;

namespace MLI.Forms
{
	public partial class CommandSettingsForm : Form
	{
		private static CommandSettingsForm instance;
		private List<Command> commandList;
		private Dictionary<CommandId, NumericUpDown> commandSettings = new Dictionary<CommandId, NumericUpDown>();

		public CommandSettingsForm()
		{
			InitializeComponent();
		}

		public static void ShowForm()
		{
			if (instance == null)
			{
				instance = new CommandSettingsForm();
			}
			else
			{
				if (!instance.Created)
				{
					instance = new CommandSettingsForm();
				}
				else
				{
					instance.Focus();
					return;
				}
			}
			instance.Show();
		}

		private void CommandSettingsForm_Load(object sender, EventArgs e)
		{
			commandList = CommandService.GetCommandList();
			int x = 15, y = 15;
			int maxX = 0;
			foreach (Command command in commandList)
			{
				Label label = new Label
				{
					AutoSize = true,
					Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
					Location = new Point(x, y),
					Text = $"{command.title}: "
				};
				Controls.Add(label);
				NumericUpDown number = new NumericUpDown
				{
					AutoSize = true,
					Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 204),
					Location = new Point(label.Right + 15, y),
					Minimum = new decimal(new[] { 1, 0, 0, 0 }),
					Maximum = new decimal(new[] { 100, 0, 0, 0 }),
					Text = command.time.ToString()
				};
				Controls.Add(number);
				commandSettings.Add(command.id, number);
				maxX = number.Right > maxX ? number.Right : maxX;
				if (y + 25 < 450)
				{
					y += 25;
				}
				else
				{
					x = maxX + 15;
					y = 15;
					maxX = 0;
				}
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			foreach (Command command in commandList)
			{
				NumericUpDown number;
				if (commandSettings.TryGetValue(command.id, out number))
				{
					command.time = (int)number.Value;
				}
			}
			CommandService.SetCommandList(commandList);
		}
	}
}