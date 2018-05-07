namespace MLI.Forms
{
	partial class CommonSettingsForm
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
			this.label1 = new System.Windows.Forms.Label();
			this.numUnitResourceCount = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.numTickLength = new System.Windows.Forms.NumericUpDown();
			this.gbInfoLevel = new System.Windows.Forms.GroupBox();
			this.rbU = new System.Windows.Forms.RadioButton();
			this.rbM = new System.Windows.Forms.RadioButton();
			this.rbN = new System.Windows.Forms.RadioButton();
			this.rbV = new System.Windows.Forms.RadioButton();
			this.btnSave = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numUnitResourceCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTickLength)).BeginInit();
			this.gbInfoLevel.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(250, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Количество ресурсов:";
			// 
			// numUnitResourceCount
			// 
			this.numUnitResourceCount.Location = new System.Drawing.Point(268, 9);
			this.numUnitResourceCount.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.numUnitResourceCount.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            0});
			this.numUnitResourceCount.Name = "numExecUnitCount";
			this.numUnitResourceCount.Size = new System.Drawing.Size(50, 20);
			this.numUnitResourceCount.TabIndex = 2;
			this.numUnitResourceCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(12, 39);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(141, 20);
			this.label3.TabIndex = 4;
			this.label3.Text = "Длина такта (нс):";
			// 
			// numTickLength
			// 
			this.numTickLength.Location = new System.Drawing.Point(268, 39);
			this.numTickLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numTickLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numTickLength.Name = "numTickLength";
			this.numTickLength.Size = new System.Drawing.Size(50, 20);
			this.numTickLength.TabIndex = 5;
			this.numTickLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// gbInfoLevel
			// 
			this.gbInfoLevel.Controls.Add(this.rbU);
			this.gbInfoLevel.Controls.Add(this.rbM);
			this.gbInfoLevel.Controls.Add(this.rbN);
			this.gbInfoLevel.Controls.Add(this.rbV);
			this.gbInfoLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gbInfoLevel.Location = new System.Drawing.Point(16, 69);
			this.gbInfoLevel.Name = "gbInfoLevel";
			this.gbInfoLevel.Size = new System.Drawing.Size(302, 137);
			this.gbInfoLevel.TabIndex = 6;
			this.gbInfoLevel.TabStop = false;
			this.gbInfoLevel.Text = "Уровень сбора информации:";
			// 
			// rbU
			// 
			this.rbU.AutoSize = true;
			this.rbU.Location = new System.Drawing.Point(6, 103);
			this.rbU.Name = "rbU";
			this.rbU.Size = new System.Drawing.Size(95, 21);
			this.rbU.TabIndex = 3;
			this.rbU.TabStop = true;
			this.rbU.Text = "U-процесс";
			this.rbU.UseVisualStyleBackColor = true;
			// 
			// rbM
			// 
			this.rbM.AutoSize = true;
			this.rbM.Location = new System.Drawing.Point(6, 76);
			this.rbM.Name = "rbM";
			this.rbM.Size = new System.Drawing.Size(96, 21);
			this.rbM.TabIndex = 2;
			this.rbM.TabStop = true;
			this.rbM.Text = "M-процесс";
			this.rbM.UseVisualStyleBackColor = true;
			// 
			// rbN
			// 
			this.rbN.AutoSize = true;
			this.rbN.Location = new System.Drawing.Point(6, 49);
			this.rbN.Name = "rbN";
			this.rbN.Size = new System.Drawing.Size(95, 21);
			this.rbN.TabIndex = 1;
			this.rbN.TabStop = true;
			this.rbN.Text = "N-процесс";
			this.rbN.UseVisualStyleBackColor = true;
			// 
			// rbV
			// 
			this.rbV.AutoSize = true;
			this.rbV.Location = new System.Drawing.Point(6, 22);
			this.rbV.Name = "rbV";
			this.rbV.Size = new System.Drawing.Size(94, 21);
			this.rbV.TabIndex = 0;
			this.rbV.TabStop = true;
			this.rbV.Text = "V-процесс";
			this.rbV.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(134, 247);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "Сохранить";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// CommonSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(332, 282);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.gbInfoLevel);
			this.Controls.Add(this.numTickLength);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.numUnitResourceCount);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CommonSettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Общие настройки";
			this.Load += new System.EventHandler(this.SettingsForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.numUnitResourceCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTickLength)).EndInit();
			this.gbInfoLevel.ResumeLayout(false);
			this.gbInfoLevel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numUnitResourceCount;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numTickLength;
		private System.Windows.Forms.GroupBox gbInfoLevel;
		private System.Windows.Forms.RadioButton rbU;
		private System.Windows.Forms.RadioButton rbM;
		private System.Windows.Forms.RadioButton rbN;
		private System.Windows.Forms.RadioButton rbV;
		private System.Windows.Forms.Button btnSave;
	}
}