namespace MLI.Forms
{
	partial class MainForm
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

		#region Код, автоматически созданный конструктором форм Windows

		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.rtbFacts = new System.Windows.Forms.RichTextBox();
			this.rtbRules = new System.Windows.Forms.RichTextBox();
			this.rtbConclusions = new System.Windows.Forms.RichTextBox();
			this.rtbLog = new System.Windows.Forms.RichTextBox();
			this.btnClearLog = new System.Windows.Forms.Button();
			this.btnSaveLog = new System.Windows.Forms.Button();
			this.menu = new System.Windows.Forms.MenuStrip();
			this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemNewFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemOpenFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSaveFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSaveAsFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemLogicInference = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemRun = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemStop = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemTree = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemExecUnits = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemProcesses = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemStatistics = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Исходные факты:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(12, 238);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(156, 20);
			this.label2.TabIndex = 1;
			this.label2.Text = "Исходные правила:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(12, 448);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(172, 20);
			this.label3.TabIndex = 2;
			this.label3.Text = "Выводимые правила:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(402, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "Лог:";
			// 
			// rtbFacts
			// 
			this.rtbFacts.Location = new System.Drawing.Point(16, 47);
			this.rtbFacts.Name = "rtbFacts";
			this.rtbFacts.Size = new System.Drawing.Size(350, 188);
			this.rtbFacts.TabIndex = 4;
			this.rtbFacts.Text = "";
			// 
			// rtbRules
			// 
			this.rtbRules.Location = new System.Drawing.Point(16, 261);
			this.rtbRules.Name = "rtbRules";
			this.rtbRules.Size = new System.Drawing.Size(350, 184);
			this.rtbRules.TabIndex = 5;
			this.rtbRules.Text = "";
			// 
			// rtbConclusions
			// 
			this.rtbConclusions.Location = new System.Drawing.Point(16, 471);
			this.rtbConclusions.Name = "rtbConclusions";
			this.rtbConclusions.Size = new System.Drawing.Size(350, 54);
			this.rtbConclusions.TabIndex = 6;
			this.rtbConclusions.Text = "";
			// 
			// rtbLog
			// 
			this.rtbLog.Location = new System.Drawing.Point(406, 47);
			this.rtbLog.Name = "rtbLog";
			this.rtbLog.ReadOnly = true;
			this.rtbLog.Size = new System.Drawing.Size(590, 446);
			this.rtbLog.TabIndex = 7;
			this.rtbLog.Text = "";
			// 
			// btnClearLog
			// 
			this.btnClearLog.Location = new System.Drawing.Point(406, 502);
			this.btnClearLog.Name = "btnClearLog";
			this.btnClearLog.Size = new System.Drawing.Size(75, 23);
			this.btnClearLog.TabIndex = 8;
			this.btnClearLog.Text = "Очистить";
			this.btnClearLog.UseVisualStyleBackColor = true;
			// 
			// btnSaveLog
			// 
			this.btnSaveLog.Location = new System.Drawing.Point(921, 502);
			this.btnSaveLog.Name = "btnSaveLog";
			this.btnSaveLog.Size = new System.Drawing.Size(75, 23);
			this.btnSaveLog.TabIndex = 9;
			this.btnSaveLog.Text = "Сохранить";
			this.btnSaveLog.UseVisualStyleBackColor = true;
			// 
			// menu
			// 
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemLogicInference,
            this.menuItemSettings,
            this.menuItemInfo});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(1008, 24);
			this.menu.TabIndex = 10;
			this.menu.Text = "menuStrip1";
			// 
			// menuItemFile
			// 
			this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNewFile,
            this.menuItemOpenFile,
            this.menuItemSaveFile,
            this.menuItemSaveAsFile,
            this.menuItemSeparator,
            this.menuItemExit});
			this.menuItemFile.Name = "menuItemFile";
			this.menuItemFile.Size = new System.Drawing.Size(48, 20);
			this.menuItemFile.Text = "Файл";
			// 
			// menuItemNewFile
			// 
			this.menuItemNewFile.Name = "menuItemNewFile";
			this.menuItemNewFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.menuItemNewFile.Size = new System.Drawing.Size(234, 22);
			this.menuItemNewFile.Text = "Новый";
			// 
			// menuItemOpenFile
			// 
			this.menuItemOpenFile.Name = "menuItemOpenFile";
			this.menuItemOpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.menuItemOpenFile.Size = new System.Drawing.Size(234, 22);
			this.menuItemOpenFile.Text = "Открыть";
			// 
			// menuItemSaveFile
			// 
			this.menuItemSaveFile.Name = "menuItemSaveFile";
			this.menuItemSaveFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.menuItemSaveFile.Size = new System.Drawing.Size(234, 22);
			this.menuItemSaveFile.Text = "Сохранить";
			// 
			// menuItemSaveAsFile
			// 
			this.menuItemSaveAsFile.Name = "menuItemSaveAsFile";
			this.menuItemSaveAsFile.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.menuItemSaveAsFile.Size = new System.Drawing.Size(234, 22);
			this.menuItemSaveAsFile.Text = "Сохранить как...";
			// 
			// menuItemSeparator
			// 
			this.menuItemSeparator.Name = "menuItemSeparator";
			this.menuItemSeparator.Size = new System.Drawing.Size(231, 6);
			// 
			// menuItemExit
			// 
			this.menuItemExit.Name = "menuItemExit";
			this.menuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.menuItemExit.Size = new System.Drawing.Size(234, 22);
			this.menuItemExit.Text = "Выход";
			// 
			// menuItemLogicInference
			// 
			this.menuItemLogicInference.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemRun,
            this.menuItemStop,
            this.menuItemTree,
            this.menuItemExecUnits,
            this.menuItemProcesses,
            this.menuItemStatistics});
			this.menuItemLogicInference.Name = "menuItemLogicInference";
			this.menuItemLogicInference.Size = new System.Drawing.Size(122, 20);
			this.menuItemLogicInference.Text = "Логический вывод";
			// 
			// menuItemRun
			// 
			this.menuItemRun.Name = "menuItemRun";
			this.menuItemRun.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.menuItemRun.Size = new System.Drawing.Size(207, 22);
			this.menuItemRun.Text = "Выполнить";
			// 
			// menuItemStop
			// 
			this.menuItemStop.Enabled = false;
			this.menuItemStop.Name = "menuItemStop";
			this.menuItemStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.menuItemStop.Size = new System.Drawing.Size(207, 22);
			this.menuItemStop.Text = "Остановить";
			// 
			// menuItemTree
			// 
			this.menuItemTree.Name = "menuItemTree";
			this.menuItemTree.Size = new System.Drawing.Size(207, 22);
			this.menuItemTree.Text = "Дерево вывода";
			this.menuItemTree.Click += new System.EventHandler(this.menuItemTree_Click);
			// 
			// menuItemExecUnits
			// 
			this.menuItemExecUnits.Name = "menuItemExecUnits";
			this.menuItemExecUnits.Size = new System.Drawing.Size(207, 22);
			this.menuItemExecUnits.Text = "Исполнительные блоки";
			this.menuItemExecUnits.Click += new System.EventHandler(this.menuItemExecUnits_Click);
			// 
			// menuItemProcesses
			// 
			this.menuItemProcesses.Name = "menuItemProcesses";
			this.menuItemProcesses.Size = new System.Drawing.Size(207, 22);
			this.menuItemProcesses.Text = "Процессы";
			this.menuItemProcesses.Click += new System.EventHandler(this.menuItemProcesses_Click);
			// 
			// menuItemStatistics
			// 
			this.menuItemStatistics.Name = "menuItemStatistics";
			this.menuItemStatistics.Size = new System.Drawing.Size(207, 22);
			this.menuItemStatistics.Text = "Статистика";
			this.menuItemStatistics.Click += new System.EventHandler(this.menuItemStatistics_Click);
			// 
			// menuItemSettings
			// 
			this.menuItemSettings.Name = "menuItemSettings";
			this.menuItemSettings.Size = new System.Drawing.Size(79, 20);
			this.menuItemSettings.Text = "Настройки";
			this.menuItemSettings.Click += new System.EventHandler(this.menuItemSettings_Click);
			// 
			// menuItemInfo
			// 
			this.menuItemInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemHelp,
            this.menuItemAbout});
			this.menuItemInfo.Name = "menuItemInfo";
			this.menuItemInfo.Size = new System.Drawing.Size(65, 20);
			this.menuItemInfo.Text = "Справка";
			// 
			// menuItemHelp
			// 
			this.menuItemHelp.Name = "menuItemHelp";
			this.menuItemHelp.Size = new System.Drawing.Size(149, 22);
			this.menuItemHelp.Text = "Помощь";
			this.menuItemHelp.Click += new System.EventHandler(this.menuItemHelp_Click);
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Name = "menuItemAbout";
			this.menuItemAbout.Size = new System.Drawing.Size(149, 22);
			this.menuItemAbout.Text = "О программе";
			this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 537);
			this.Controls.Add(this.btnSaveLog);
			this.Controls.Add(this.btnClearLog);
			this.Controls.Add(this.rtbLog);
			this.Controls.Add(this.rtbConclusions);
			this.Controls.Add(this.rtbRules);
			this.Controls.Add(this.rtbFacts);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.menu);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menu;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Машина логического вывода";
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RichTextBox rtbFacts;
		private System.Windows.Forms.RichTextBox rtbRules;
		private System.Windows.Forms.RichTextBox rtbConclusions;
		private System.Windows.Forms.RichTextBox rtbLog;
		private System.Windows.Forms.Button btnClearLog;
		private System.Windows.Forms.Button btnSaveLog;
		private System.Windows.Forms.MenuStrip menu;
		private System.Windows.Forms.ToolStripMenuItem menuItemFile;
		private System.Windows.Forms.ToolStripMenuItem menuItemNewFile;
		private System.Windows.Forms.ToolStripMenuItem menuItemOpenFile;
		private System.Windows.Forms.ToolStripMenuItem menuItemSaveFile;
		private System.Windows.Forms.ToolStripMenuItem menuItemSaveAsFile;
		private System.Windows.Forms.ToolStripSeparator menuItemSeparator;
		private System.Windows.Forms.ToolStripMenuItem menuItemExit;
		private System.Windows.Forms.ToolStripMenuItem menuItemLogicInference;
		private System.Windows.Forms.ToolStripMenuItem menuItemRun;
		private System.Windows.Forms.ToolStripMenuItem menuItemStop;
		private System.Windows.Forms.ToolStripMenuItem menuItemTree;
		private System.Windows.Forms.ToolStripMenuItem menuItemExecUnits;
		private System.Windows.Forms.ToolStripMenuItem menuItemProcesses;
		private System.Windows.Forms.ToolStripMenuItem menuItemStatistics;
		private System.Windows.Forms.ToolStripMenuItem menuItemSettings;
		private System.Windows.Forms.ToolStripMenuItem menuItemInfo;
		private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
		private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
	}
}

