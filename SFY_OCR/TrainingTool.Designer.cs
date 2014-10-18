﻿namespace SFY_OCR
{
	partial class TrainingTool
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrainingTool));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.cmbPreProcess = new System.Windows.Forms.ComboBox();
			this.chkToTiff = new System.Windows.Forms.CheckBox();
			this.chkDenoise = new System.Windows.Forms.CheckBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.button2 = new System.Windows.Forms.Button();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pbxExample = new System.Windows.Forms.PictureBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.ofdFile = new System.Windows.Forms.OpenFileDialog();
			this.bgwProcessImage = new System.ComponentModel.BackgroundWorker();
			this.tmrClearBox = new System.Windows.Forms.Timer(this.components);
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.dudX = new System.Windows.Forms.DomainUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.domainUpDown2 = new System.Windows.Forms.DomainUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.domainUpDown3 = new System.Windows.Forms.DomainUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.domainUpDown4 = new System.Windows.Forms.DomainUpDown();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnInsert = new System.Windows.Forms.Button();
			this.btnMerge = new System.Windows.Forms.Button();
			this.btnSplit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.txtChar = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbxExample)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(784, 522);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location = new System.Drawing.Point(4, 26);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(776, 492);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
			this.splitContainer1.Size = new System.Drawing.Size(770, 486);
			this.splitContainer1.SplitterDistance = 256;
			this.splitContainer1.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.button1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnSave, 0, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 197F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 486);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(256, 30);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 27);
			this.toolStripButton1.Text = "打开图片";
			this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.cmbPreProcess, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.chkToTiff, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.chkDenoise, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.trackBar1, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.button2, 0, 3);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 33);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 5;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(250, 191);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// cmbPreProcess
			// 
			this.cmbPreProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPreProcess.FormattingEnabled = true;
			this.cmbPreProcess.Location = new System.Drawing.Point(3, 3);
			this.cmbPreProcess.Name = "cmbPreProcess";
			this.cmbPreProcess.Size = new System.Drawing.Size(121, 25);
			this.cmbPreProcess.TabIndex = 0;
			// 
			// chkToTiff
			// 
			this.chkToTiff.AutoSize = true;
			this.chkToTiff.Location = new System.Drawing.Point(3, 54);
			this.chkToTiff.Name = "chkToTiff";
			this.chkToTiff.Size = new System.Drawing.Size(142, 21);
			this.chkToTiff.TabIndex = 1;
			this.chkToTiff.Text = "转换为无损 TIFF 格式";
			this.chkToTiff.UseVisualStyleBackColor = true;
			// 
			// chkDenoise
			// 
			this.chkDenoise.AutoSize = true;
			this.chkDenoise.Checked = true;
			this.chkDenoise.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkDenoise.Location = new System.Drawing.Point(3, 84);
			this.chkDenoise.Name = "chkDenoise";
			this.chkDenoise.Size = new System.Drawing.Size(51, 21);
			this.chkDenoise.TabIndex = 2;
			this.chkDenoise.Text = "降噪";
			this.chkDenoise.UseVisualStyleBackColor = true;
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(3, 143);
			this.trackBar1.Maximum = 100;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(172, 45);
			this.trackBar1.TabIndex = 3;
			this.trackBar1.TickFrequency = 5;
			this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(3, 113);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "降噪";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 3;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.436214F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.56379F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(510, 486);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 230);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(183, 33);
			this.button1.TabIndex = 1;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pbxExample);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 76);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(504, 407);
			this.panel1.TabIndex = 2;
			// 
			// pbxExample
			// 
			this.pbxExample.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbxExample.Location = new System.Drawing.Point(0, 0);
			this.pbxExample.Name = "pbxExample";
			this.pbxExample.Size = new System.Drawing.Size(504, 407);
			this.pbxExample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbxExample.TabIndex = 1;
			this.pbxExample.TabStop = false;
			this.pbxExample.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxExample_MouseClick);
			this.pbxExample.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxExample_MouseDown);
			this.pbxExample.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxExample_MouseMove);
			this.pbxExample.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbxExample_MouseUp);
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 26);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(776, 492);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// bgwProcessImage
			// 
			this.bgwProcessImage.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwProcessImage_DoWork);
			this.bgwProcessImage.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwProcessImage_RunWorkerCompleted);
			// 
			// tmrClearBox
			// 
			this.tmrClearBox.Tick += new System.EventHandler(this.tmrClearBox_Tick);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.label5);
			this.flowLayoutPanel1.Controls.Add(this.txtChar);
			this.flowLayoutPanel1.Controls.Add(this.label1);
			this.flowLayoutPanel1.Controls.Add(this.dudX);
			this.flowLayoutPanel1.Controls.Add(this.label2);
			this.flowLayoutPanel1.Controls.Add(this.domainUpDown2);
			this.flowLayoutPanel1.Controls.Add(this.label3);
			this.flowLayoutPanel1.Controls.Add(this.domainUpDown3);
			this.flowLayoutPanel1.Controls.Add(this.label4);
			this.flowLayoutPanel1.Controls.Add(this.domainUpDown4);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(504, 29);
			this.flowLayoutPanel1.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(109, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "X";
			// 
			// dudX
			// 
			this.dudX.Location = new System.Drawing.Point(131, 3);
			this.dudX.Name = "dudX";
			this.dudX.Size = new System.Drawing.Size(40, 23);
			this.dudX.TabIndex = 0;
			this.dudX.Text = "0";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(177, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(15, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Y";
			// 
			// domainUpDown2
			// 
			this.domainUpDown2.Location = new System.Drawing.Point(198, 3);
			this.domainUpDown2.Name = "domainUpDown2";
			this.domainUpDown2.Size = new System.Drawing.Size(40, 23);
			this.domainUpDown2.TabIndex = 2;
			this.domainUpDown2.Text = "0";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(244, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "宽度";
			// 
			// domainUpDown3
			// 
			this.domainUpDown3.Location = new System.Drawing.Point(282, 3);
			this.domainUpDown3.Name = "domainUpDown3";
			this.domainUpDown3.Size = new System.Drawing.Size(40, 23);
			this.domainUpDown3.TabIndex = 4;
			this.domainUpDown3.Text = "0";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(328, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 17);
			this.label4.TabIndex = 7;
			this.label4.Text = "高度";
			// 
			// domainUpDown4
			// 
			this.domainUpDown4.Location = new System.Drawing.Point(366, 3);
			this.domainUpDown4.Name = "domainUpDown4";
			this.domainUpDown4.Size = new System.Drawing.Size(40, 23);
			this.domainUpDown4.TabIndex = 6;
			this.domainUpDown4.Text = "0";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.btnInsert);
			this.flowLayoutPanel2.Controls.Add(this.btnMerge);
			this.flowLayoutPanel2.Controls.Add(this.btnSplit);
			this.flowLayoutPanel2.Controls.Add(this.btnDelete);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 38);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(504, 32);
			this.flowLayoutPanel2.TabIndex = 5;
			// 
			// btnInsert
			// 
			this.btnInsert.Location = new System.Drawing.Point(3, 3);
			this.btnInsert.Name = "btnInsert";
			this.btnInsert.Size = new System.Drawing.Size(75, 23);
			this.btnInsert.TabIndex = 0;
			this.btnInsert.Text = "插入";
			this.btnInsert.UseVisualStyleBackColor = true;
			this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
			// 
			// btnMerge
			// 
			this.btnMerge.Location = new System.Drawing.Point(84, 3);
			this.btnMerge.Name = "btnMerge";
			this.btnMerge.Size = new System.Drawing.Size(75, 23);
			this.btnMerge.TabIndex = 1;
			this.btnMerge.Text = "合并";
			this.btnMerge.UseVisualStyleBackColor = true;
			// 
			// btnSplit
			// 
			this.btnSplit.Location = new System.Drawing.Point(165, 3);
			this.btnSplit.Name = "btnSplit";
			this.btnSplit.Size = new System.Drawing.Size(75, 23);
			this.btnSplit.TabIndex = 2;
			this.btnSplit.Text = "分割";
			this.btnSplit.UseVisualStyleBackColor = true;
			// 
			// btnDelete
			// 
			this.btnDelete.Location = new System.Drawing.Point(246, 3);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 23);
			this.btnDelete.TabIndex = 3;
			this.btnDelete.Text = "删除";
			this.btnDelete.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(3, 274);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "保存";
			this.btnSave.UseVisualStyleBackColor = true;
			// 
			// txtChar
			// 
			this.txtChar.Location = new System.Drawing.Point(41, 3);
			this.txtChar.Name = "txtChar";
			this.txtChar.Size = new System.Drawing.Size(62, 23);
			this.txtChar.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 17);
			this.label5.TabIndex = 9;
			this.label5.Text = "字符";
			// 
			// TrainingTool
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 522);
			this.Controls.Add(this.tabControl1);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("微软雅黑", 9F);
			this.Name = "TrainingTool";
			this.Text = "训练工具";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrainingTool_FormClosing);
			this.Load += new System.EventHandler(this.TrainingTool_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbxExample)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.OpenFileDialog ofdFile;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pbxExample;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.ComboBox cmbPreProcess;
		private System.Windows.Forms.CheckBox chkToTiff;
		private System.Windows.Forms.CheckBox chkDenoise;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Button button2;
		private System.ComponentModel.BackgroundWorker bgwProcessImage;
		private System.Windows.Forms.Timer tmrClearBox;
		private System.Windows.Forms.Timer tmrCheckMovement;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DomainUpDown dudX;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DomainUpDown domainUpDown2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.DomainUpDown domainUpDown3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DomainUpDown domainUpDown4;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.Button btnMerge;
		private System.Windows.Forms.Button btnSplit;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtChar;
	}
}