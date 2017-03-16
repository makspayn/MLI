namespace MLI.Forms
{
	partial class ExecUnitsForm
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
			this.btnShowLog = new System.Windows.Forms.Button();
			this.numExecUnitNumber = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.rtbLog = new System.Windows.Forms.RichTextBox();
			((System.ComponentModel.ISupportInitialize)(this.numExecUnitNumber)).BeginInit();
			this.SuspendLayout();
			// 
			// btnShowLog
			// 
			this.btnShowLog.Location = new System.Drawing.Point(281, 18);
			this.btnShowLog.Name = "btnShowLog";
			this.btnShowLog.Size = new System.Drawing.Size(99, 22);
			this.btnShowLog.TabIndex = 10;
			this.btnShowLog.Text = "Показать лог";
			this.btnShowLog.UseVisualStyleBackColor = true;
			this.btnShowLog.Click += new System.EventHandler(this.btnShowLog_Click);
			// 
			// numExecUnitNumber
			// 
			this.numExecUnitNumber.Location = new System.Drawing.Point(199, 19);
			this.numExecUnitNumber.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.numExecUnitNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numExecUnitNumber.Name = "numExecUnitNumber";
			this.numExecUnitNumber.Size = new System.Drawing.Size(50, 20);
			this.numExecUnitNumber.TabIndex = 9;
			this.numExecUnitNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 20);
			this.label1.TabIndex = 8;
			this.label1.Text = "Исполнительный блок:";
			// 
			// rtbLog
			// 
			this.rtbLog.Location = new System.Drawing.Point(0, 61);
			this.rtbLog.Name = "rtbLog";
			this.rtbLog.ReadOnly = true;
			this.rtbLog.Size = new System.Drawing.Size(484, 401);
			this.rtbLog.TabIndex = 11;
			this.rtbLog.Text = "";
			// 
			// ExecUnitsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 461);
			this.Controls.Add(this.rtbLog);
			this.Controls.Add(this.btnShowLog);
			this.Controls.Add(this.numExecUnitNumber);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExecUnitsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Исполнительные блоки";
			((System.ComponentModel.ISupportInitialize)(this.numExecUnitNumber)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnShowLog;
		private System.Windows.Forms.NumericUpDown numExecUnitNumber;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RichTextBox rtbLog;
	}
}