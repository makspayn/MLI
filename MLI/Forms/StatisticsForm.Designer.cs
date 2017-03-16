namespace MLI.Forms
{
	partial class StatisticsForm
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
			this.dgStatistics = new System.Windows.Forms.DataGridView();
			this.colStatistic = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgStatistics)).BeginInit();
			this.SuspendLayout();
			// 
			// dgStatistics
			// 
			this.dgStatistics.AllowUserToAddRows = false;
			this.dgStatistics.AllowUserToDeleteRows = false;
			this.dgStatistics.AllowUserToResizeColumns = false;
			this.dgStatistics.AllowUserToResizeRows = false;
			this.dgStatistics.BackgroundColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgStatistics.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgStatistics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStatistic,
            this.colValue});
			this.dgStatistics.Location = new System.Drawing.Point(0, 0);
			this.dgStatistics.MultiSelect = false;
			this.dgStatistics.Name = "dgStatistics";
			this.dgStatistics.ReadOnly = true;
			this.dgStatistics.RowHeadersVisible = false;
			this.dgStatistics.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgStatistics.Size = new System.Drawing.Size(584, 262);
			this.dgStatistics.TabIndex = 0;
			// 
			// colStatistic
			// 
			this.colStatistic.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colStatistic.HeaderText = "Объект статистики";
			this.colStatistic.Name = "colStatistic";
			this.colStatistic.ReadOnly = true;
			this.colStatistic.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colStatistic.Width = 115;
			// 
			// colValue
			// 
			this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.colValue.HeaderText = "Значение";
			this.colValue.Name = "colValue";
			this.colValue.ReadOnly = true;
			this.colValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.colValue.Width = 69;
			// 
			// StatisticsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 261);
			this.Controls.Add(this.dgStatistics);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "StatisticsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Статистика";
			this.Load += new System.EventHandler(this.StatisticsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dgStatistics)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dgStatistics;
		private System.Windows.Forms.DataGridViewTextBoxColumn colStatistic;
		private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
	}
}