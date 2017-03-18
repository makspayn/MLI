namespace MLI.Forms
{
	partial class ProcessesForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgProcesses = new System.Windows.Forms.DataGridView();
			this.colProcess = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colExecutor = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colWaitTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colReadyTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colRunTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colStartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colEndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgProcesses)).BeginInit();
			this.SuspendLayout();
			// 
			// dgProcesses
			// 
			this.dgProcesses.AllowUserToAddRows = false;
			this.dgProcesses.AllowUserToDeleteRows = false;
			this.dgProcesses.AllowUserToResizeColumns = false;
			this.dgProcesses.AllowUserToResizeRows = false;
			this.dgProcesses.BackgroundColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgProcesses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgProcesses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgProcesses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProcess,
            this.colExecutor,
            this.colWaitTime,
            this.colReadyTime,
            this.colRunTime,
            this.colStartTime,
            this.colEndTime});
			this.dgProcesses.Location = new System.Drawing.Point(1, -1);
			this.dgProcesses.MultiSelect = false;
			this.dgProcesses.Name = "dgProcesses";
			this.dgProcesses.ReadOnly = true;
			this.dgProcesses.RowHeadersVisible = false;
			this.dgProcesses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgProcesses.Size = new System.Drawing.Size(1009, 540);
			this.dgProcesses.TabIndex = 1;
			this.dgProcesses.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dgProcesses_SortCompare);
			// 
			// colProcess
			// 
			this.colProcess.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colProcess.HeaderText = "Процесс";
			this.colProcess.Name = "colProcess";
			this.colProcess.ReadOnly = true;
			this.colProcess.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colProcess.Width = 64;
			// 
			// colExecutor
			// 
			this.colExecutor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colExecutor.HeaderText = "Исполнитель";
			this.colExecutor.Name = "colExecutor";
			this.colExecutor.ReadOnly = true;
			this.colExecutor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colExecutor.Width = 91;
			// 
			// colWaitTime
			// 
			this.colWaitTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colWaitTime.HeaderText = "Время WAIT";
			this.colWaitTime.Name = "colWaitTime";
			this.colWaitTime.ReadOnly = true;
			this.colWaitTime.Width = 97;
			// 
			// colReadyTime
			// 
			this.colReadyTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colReadyTime.HeaderText = "Время READY";
			this.colReadyTime.Name = "colReadyTime";
			this.colReadyTime.ReadOnly = true;
			this.colReadyTime.Width = 106;
			// 
			// colRunTime
			// 
			this.colRunTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colRunTime.HeaderText = "Время RUN";
			this.colRunTime.Name = "colRunTime";
			this.colRunTime.ReadOnly = true;
			this.colRunTime.Width = 93;
			// 
			// colStartTime
			// 
			this.colStartTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colStartTime.HeaderText = "Время START";
			this.colStartTime.Name = "colStartTime";
			this.colStartTime.ReadOnly = true;
			this.colStartTime.Width = 105;
			// 
			// colEndTime
			// 
			this.colEndTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colEndTime.HeaderText = "Время END";
			this.colEndTime.Name = "colEndTime";
			this.colEndTime.ReadOnly = true;
			this.colEndTime.Width = 92;
			// 
			// ProcessesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 537);
			this.Controls.Add(this.dgProcesses);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProcessesForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Процессы";
			this.Load += new System.EventHandler(this.ProcessesForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgProcesses)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgProcesses;
		private System.Windows.Forms.DataGridViewTextBoxColumn colProcess;
		private System.Windows.Forms.DataGridViewTextBoxColumn colExecutor;
		private System.Windows.Forms.DataGridViewTextBoxColumn colWaitTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn colReadyTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn colRunTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn colStartTime;
		private System.Windows.Forms.DataGridViewTextBoxColumn colEndTime;
	}
}