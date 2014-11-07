namespace SFY_OCR
{
	partial class frmMain
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lbxImages = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblPictureBoxError = new System.Windows.Forms.Label();
			this.pbxForOcr = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.btnOcr = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.cbxLanguageType = new System.Windows.Forms.ComboBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtResult = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.progressBarOcr = new System.Windows.Forms.ProgressBar();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.清空CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.训练TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.选项OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.ofdPicture = new System.Windows.Forms.OpenFileDialog();
			this.tmrProgressBar = new System.Windows.Forms.Timer(this.components);
			this.bgwImageRecognition = new System.ComponentModel.BackgroundWorker();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbxForOcr)).BeginInit();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			resources.ApplyResources(this.splitContainer1, "splitContainer1");
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// splitContainer2
			// 
			resources.ApplyResources(this.splitContainer2, "splitContainer2");
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lbxImages);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// lbxImages
			// 
			resources.ApplyResources(this.lbxImages, "lbxImages");
			this.lbxImages.FormattingEnabled = true;
			this.lbxImages.Name = "lbxImages";
			this.lbxImages.SelectedIndexChanged += new System.EventHandler(this.lbxImages_SelectedIndexChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblPictureBoxError);
			this.groupBox2.Controls.Add(this.pbxForOcr);
			resources.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			// 
			// lblPictureBoxError
			// 
			this.lblPictureBoxError.AutoEllipsis = true;
			this.lblPictureBoxError.BackColor = System.Drawing.SystemColors.Window;
			resources.ApplyResources(this.lblPictureBoxError, "lblPictureBoxError");
			this.lblPictureBoxError.Name = "lblPictureBoxError";
			// 
			// pbxForOcr
			// 
			this.pbxForOcr.BackColor = System.Drawing.SystemColors.Window;
			this.pbxForOcr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.pbxForOcr, "pbxForOcr");
			this.pbxForOcr.Name = "pbxForOcr";
			this.pbxForOcr.TabStop = false;
			// 
			// tableLayoutPanel4
			// 
			resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
			this.tableLayoutPanel4.Controls.Add(this.btnOcr, 2, 0);
			this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.cbxLanguageType, 1, 0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			// 
			// btnOcr
			// 
			resources.ApplyResources(this.btnOcr, "btnOcr");
			this.btnOcr.Name = "btnOcr";
			this.btnOcr.UseVisualStyleBackColor = true;
			this.btnOcr.Click += new System.EventHandler(this.btnOcr_Click);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// cbxLanguageType
			// 
			resources.ApplyResources(this.cbxLanguageType, "cbxLanguageType");
			this.cbxLanguageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxLanguageType.FormattingEnabled = true;
			this.cbxLanguageType.Name = "cbxLanguageType";
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.groupBox3, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtResult);
			resources.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			// 
			// txtResult
			// 
			resources.ApplyResources(this.txtResult, "txtResult");
			this.txtResult.Name = "txtResult";
			this.txtResult.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtResult_MouseMove);
			// 
			// tableLayoutPanel3
			// 
			resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
			this.tableLayoutPanel3.Controls.Add(this.btnCancel, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.progressBarOcr, 0, 0);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			// 
			// btnCancel
			// 
			resources.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// progressBarOcr
			// 
			resources.ApplyResources(this.progressBarOcr, "progressBarOcr");
			this.progressBarOcr.Name = "progressBarOcr";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.帮助HToolStripMenuItem});
			resources.ApplyResources(this.menuStrip1, "menuStrip1");
			this.menuStrip1.Name = "menuStrip1";
			// 
			// 文件FToolStripMenuItem
			// 
			this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开OToolStripMenuItem,
            this.清空CToolStripMenuItem,
            this.toolStripSeparator1,
            this.退出XToolStripMenuItem});
			this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
			resources.ApplyResources(this.文件FToolStripMenuItem, "文件FToolStripMenuItem");
			// 
			// 打开OToolStripMenuItem
			// 
			this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
			resources.ApplyResources(this.打开OToolStripMenuItem, "打开OToolStripMenuItem");
			this.打开OToolStripMenuItem.Click += new System.EventHandler(this.打开OToolStripMenuItem_Click);
			// 
			// 清空CToolStripMenuItem
			// 
			this.清空CToolStripMenuItem.Name = "清空CToolStripMenuItem";
			resources.ApplyResources(this.清空CToolStripMenuItem, "清空CToolStripMenuItem");
			this.清空CToolStripMenuItem.Click += new System.EventHandler(this.清空CToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// 退出XToolStripMenuItem
			// 
			this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
			resources.ApplyResources(this.退出XToolStripMenuItem, "退出XToolStripMenuItem");
			this.退出XToolStripMenuItem.Click += new System.EventHandler(this.退出XToolStripMenuItem_Click);
			// 
			// 工具TToolStripMenuItem
			// 
			this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.训练TToolStripMenuItem,
            this.toolStripSeparator2,
            this.选项OToolStripMenuItem});
			this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
			resources.ApplyResources(this.工具TToolStripMenuItem, "工具TToolStripMenuItem");
			// 
			// 训练TToolStripMenuItem
			// 
			this.训练TToolStripMenuItem.Name = "训练TToolStripMenuItem";
			resources.ApplyResources(this.训练TToolStripMenuItem, "训练TToolStripMenuItem");
			this.训练TToolStripMenuItem.Click += new System.EventHandler(this.训练TToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// 选项OToolStripMenuItem
			// 
			this.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem";
			resources.ApplyResources(this.选项OToolStripMenuItem, "选项OToolStripMenuItem");
			this.选项OToolStripMenuItem.Click += new System.EventHandler(this.选项OToolStripMenuItem_Click);
			// 
			// 帮助HToolStripMenuItem
			// 
			this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
			this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
			resources.ApplyResources(this.帮助HToolStripMenuItem, "帮助HToolStripMenuItem");
			// 
			// 关于ToolStripMenuItem
			// 
			this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
			resources.ApplyResources(this.关于ToolStripMenuItem, "关于ToolStripMenuItem");
			this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
			// 
			// statusStrip1
			// 
			resources.ApplyResources(this.statusStrip1, "statusStrip1");
			this.statusStrip1.Name = "statusStrip1";
			// 
			// ofdPicture
			// 
			this.ofdPicture.Multiselect = true;
			// 
			// tmrProgressBar
			// 
			this.tmrProgressBar.Enabled = true;
			// 
			// bgwImageRecognition
			// 
			this.bgwImageRecognition.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwImageRecognition_DoWork);
			this.bgwImageRecognition.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgwImageRecognition_ProgressChanged);
			this.bgwImageRecognition.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwImageRecognition_RunWorkerCompleted);
			// 
			// frmMain
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmMain";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.Load += new System.EventHandler(this.Main_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbxForOcr)).EndInit();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog ofdPicture;
		private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 选项OToolStripMenuItem;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListBox lbxImages;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.PictureBox pbxForOcr;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ToolStripMenuItem 清空CToolStripMenuItem;
		private System.Windows.Forms.Timer tmrProgressBar;
		private System.ComponentModel.BackgroundWorker bgwImageRecognition;
		private System.Windows.Forms.ToolStripMenuItem 退出XToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
		private System.Windows.Forms.TextBox txtResult;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ProgressBar progressBarOcr;
		private System.Windows.Forms.Label lblPictureBoxError;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbxLanguageType;
		private System.Windows.Forms.Button btnOcr;
		private System.Windows.Forms.ToolStripMenuItem 训练TToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

	}
}

