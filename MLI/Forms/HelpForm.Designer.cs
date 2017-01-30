namespace MLI.Forms
{
	partial class HelpForm
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

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.rtbHelp = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// rtbHelp
			// 
			this.rtbHelp.Location = new System.Drawing.Point(0, 0);
			this.rtbHelp.Name = "rtbHelp";
			this.rtbHelp.ReadOnly = true;
			this.rtbHelp.Size = new System.Drawing.Size(752, 393);
			this.rtbHelp.TabIndex = 0;
			this.rtbHelp.Text = "";
			// 
			// HelpForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(752, 393);
			this.Controls.Add(this.rtbHelp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HelpForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Помощь";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox rtbHelp;
	}
}