﻿namespace SFY_OCR
{
	partial class frmTrainingTool
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrainingTool));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripOpenImage = new System.Windows.Forms.ToolStripButton();
			this.toolStripBtnSaveBox = new System.Windows.Forms.ToolStripButton();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.cmbPreProcess = new System.Windows.Forms.ComboBox();
			this.chkToTiff = new System.Windows.Forms.CheckBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.button1 = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.dgvBoxes = new System.Windows.Forms.DataGridView();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label5 = new System.Windows.Forms.Label();
			this.txtCharacter = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nudX = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.nudY = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.nudWidth = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.nudHeight = new System.Windows.Forms.NumericUpDown();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pbxExample = new System.Windows.Forms.PictureBox();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnInsert = new System.Windows.Forms.Button();
			this.btnMerge = new System.Windows.Forms.Button();
			this.btnSplit = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnDisplayBoxes = new System.Windows.Forms.Button();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.btnRotateLeft = new System.Windows.Forms.Button();
			this.btnRotateRight = new System.Windows.Forms.Button();
			this.btnMakeBox = new System.Windows.Forms.Button();
			this.btnConvertToTiff = new System.Windows.Forms.Button();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnTextCleaner = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.ofdFile = new System.Windows.Forms.OpenFileDialog();
			this.bgwProcess = new System.ComponentModel.BackgroundWorker();
			this.tmrClearBox = new System.Windows.Forms.Timer(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvBoxes)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbxExample)).BeginInit();
			this.flowLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
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
			this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.btnSave, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.dgvBoxes, 0, 4);
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
            this.toolStripOpenImage,
            this.toolStripBtnSaveBox});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(256, 30);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripOpenImage
			// 
			this.toolStripOpenImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripOpenImage.Image")));
			this.toolStripOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripOpenImage.Name = "toolStripOpenImage";
			this.toolStripOpenImage.Size = new System.Drawing.Size(23, 27);
			this.toolStripOpenImage.Text = "打开图片";
			this.toolStripOpenImage.Click += new System.EventHandler(this.toolStripBtnOpenImage_Click);
			// 
			// toolStripBtnSaveBox
			// 
			this.toolStripBtnSaveBox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripBtnSaveBox.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnSaveBox.Image")));
			this.toolStripBtnSaveBox.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripBtnSaveBox.Name = "toolStripBtnSaveBox";
			this.toolStripBtnSaveBox.Size = new System.Drawing.Size(23, 27);
			this.toolStripBtnSaveBox.Text = "toolStripButton2";
			this.toolStripBtnSaveBox.Click += new System.EventHandler(this.toolStripBtnSaveBox_Click);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 1;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.cmbPreProcess, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.chkToTiff, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.trackBar1, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.button1, 0, 2);
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
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(3, 84);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(3, 274);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "保存";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// dgvBoxes
			// 
			this.dgvBoxes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvBoxes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvBoxes.Location = new System.Drawing.Point(3, 319);
			this.dgvBoxes.Name = "dgvBoxes";
			this.dgvBoxes.RowTemplate.Height = 24;
			this.dgvBoxes.Size = new System.Drawing.Size(250, 164);
			this.dgvBoxes.TabIndex = 3;
			this.dgvBoxes.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBoxes_CellValueChanged);
			this.dgvBoxes.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvBoxes_CellValueNeeded);
			this.dgvBoxes.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvBoxes_DataBindingComplete);
			this.dgvBoxes.SelectionChanged += new System.EventHandler(this.dgvBoxes_SelectionChanged);
			this.dgvBoxes.Click += new System.EventHandler(this.dgvBoxes_Click);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 0, 2);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 4;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.86475F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.13525F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(510, 486);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.label5);
			this.flowLayoutPanel1.Controls.Add(this.txtCharacter);
			this.flowLayoutPanel1.Controls.Add(this.label1);
			this.flowLayoutPanel1.Controls.Add(this.nudX);
			this.flowLayoutPanel1.Controls.Add(this.label2);
			this.flowLayoutPanel1.Controls.Add(this.nudY);
			this.flowLayoutPanel1.Controls.Add(this.label3);
			this.flowLayoutPanel1.Controls.Add(this.nudWidth);
			this.flowLayoutPanel1.Controls.Add(this.label4);
			this.flowLayoutPanel1.Controls.Add(this.nudHeight);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(504, 29);
			this.flowLayoutPanel1.TabIndex = 4;
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
			// txtCharacter
			// 
			this.txtCharacter.Location = new System.Drawing.Point(41, 3);
			this.txtCharacter.Name = "txtCharacter";
			this.txtCharacter.Size = new System.Drawing.Size(62, 23);
			this.txtCharacter.TabIndex = 8;
			this.txtCharacter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCharacter_KeyPress);
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
			// nudX
			// 
			this.nudX.Location = new System.Drawing.Point(131, 3);
			this.nudX.Name = "nudX";
			this.nudX.Size = new System.Drawing.Size(45, 23);
			this.nudX.TabIndex = 10;
			this.nudX.ValueChanged += new System.EventHandler(this.nudX_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(182, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(15, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Y";
			// 
			// nudY
			// 
			this.nudY.Location = new System.Drawing.Point(203, 3);
			this.nudY.Name = "nudY";
			this.nudY.Size = new System.Drawing.Size(45, 23);
			this.nudY.TabIndex = 11;
			this.nudY.ValueChanged += new System.EventHandler(this.nudY_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(254, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "宽度";
			// 
			// nudWidth
			// 
			this.nudWidth.Location = new System.Drawing.Point(292, 3);
			this.nudWidth.Name = "nudWidth";
			this.nudWidth.Size = new System.Drawing.Size(45, 23);
			this.nudWidth.TabIndex = 12;
			this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(343, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 17);
			this.label4.TabIndex = 7;
			this.label4.Text = "高度";
			// 
			// nudHeight
			// 
			this.nudHeight.Location = new System.Drawing.Point(381, 3);
			this.nudHeight.Name = "nudHeight";
			this.nudHeight.Size = new System.Drawing.Size(45, 23);
			this.nudHeight.TabIndex = 13;
			this.nudHeight.ValueChanged += new System.EventHandler(this.nudHeight_ValueChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pbxExample);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 118);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(504, 365);
			this.panel1.TabIndex = 2;
			// 
			// pbxExample
			// 
			this.pbxExample.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbxExample.Location = new System.Drawing.Point(0, 0);
			this.pbxExample.Name = "pbxExample";
			this.pbxExample.Size = new System.Drawing.Size(504, 365);
			this.pbxExample.TabIndex = 0;
			this.pbxExample.TabStop = false;
			this.pbxExample.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxExample_MouseClick);
			this.pbxExample.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbxExample_MouseDown);
			this.pbxExample.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxExample_MouseMove);
			this.pbxExample.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbxExample_MouseUp);
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.btnInsert);
			this.flowLayoutPanel2.Controls.Add(this.btnMerge);
			this.flowLayoutPanel2.Controls.Add(this.btnSplit);
			this.flowLayoutPanel2.Controls.Add(this.btnDelete);
			this.flowLayoutPanel2.Controls.Add(this.btnDisplayBoxes);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 38);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(504, 39);
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
			this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
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
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnDisplayBoxes
			// 
			this.btnDisplayBoxes.Location = new System.Drawing.Point(327, 3);
			this.btnDisplayBoxes.Name = "btnDisplayBoxes";
			this.btnDisplayBoxes.Size = new System.Drawing.Size(75, 23);
			this.btnDisplayBoxes.TabIndex = 4;
			this.btnDisplayBoxes.Text = "显示Box";
			this.btnDisplayBoxes.UseVisualStyleBackColor = true;
			this.btnDisplayBoxes.Click += new System.EventHandler(this.button3_Click);
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.btnRotateLeft);
			this.flowLayoutPanel3.Controls.Add(this.btnRotateRight);
			this.flowLayoutPanel3.Controls.Add(this.btnMakeBox);
			this.flowLayoutPanel3.Controls.Add(this.btnConvertToTiff);
			this.flowLayoutPanel3.Controls.Add(this.btnReset);
			this.flowLayoutPanel3.Controls.Add(this.btnTextCleaner);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 83);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(504, 29);
			this.flowLayoutPanel3.TabIndex = 6;
			// 
			// btnRotateLeft
			// 
			this.btnRotateLeft.Location = new System.Drawing.Point(3, 3);
			this.btnRotateLeft.Name = "btnRotateLeft";
			this.btnRotateLeft.Size = new System.Drawing.Size(75, 23);
			this.btnRotateLeft.TabIndex = 0;
			this.btnRotateLeft.Text = "左转90°";
			this.btnRotateLeft.UseVisualStyleBackColor = true;
			this.btnRotateLeft.Click += new System.EventHandler(this.btnRotateLeft_Click);
			// 
			// btnRotateRight
			// 
			this.btnRotateRight.Location = new System.Drawing.Point(84, 3);
			this.btnRotateRight.Name = "btnRotateRight";
			this.btnRotateRight.Size = new System.Drawing.Size(75, 23);
			this.btnRotateRight.TabIndex = 1;
			this.btnRotateRight.Text = "右转90°";
			this.btnRotateRight.UseVisualStyleBackColor = true;
			this.btnRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);
			// 
			// btnMakeBox
			// 
			this.btnMakeBox.Location = new System.Drawing.Point(165, 3);
			this.btnMakeBox.Name = "btnMakeBox";
			this.btnMakeBox.Size = new System.Drawing.Size(75, 23);
			this.btnMakeBox.TabIndex = 2;
			this.btnMakeBox.Text = "重新识别";
			this.btnMakeBox.UseVisualStyleBackColor = true;
			this.btnMakeBox.Click += new System.EventHandler(this.btnMakeBox_Click);
			// 
			// btnConvertToTiff
			// 
			this.btnConvertToTiff.Location = new System.Drawing.Point(246, 3);
			this.btnConvertToTiff.Name = "btnConvertToTiff";
			this.btnConvertToTiff.Size = new System.Drawing.Size(75, 23);
			this.btnConvertToTiff.TabIndex = 3;
			this.btnConvertToTiff.Text = "TOTIFF";
			this.btnConvertToTiff.UseVisualStyleBackColor = true;
			this.btnConvertToTiff.Click += new System.EventHandler(this.btnConvertToTiff_Click);
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(327, 3);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(75, 23);
			this.btnReset.TabIndex = 4;
			this.btnReset.Text = "复位";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnTextCleaner
			// 
			this.btnTextCleaner.Location = new System.Drawing.Point(408, 3);
			this.btnTextCleaner.Name = "btnTextCleaner";
			this.btnTextCleaner.Size = new System.Drawing.Size(75, 23);
			this.btnTextCleaner.TabIndex = 4;
			this.btnTextCleaner.Text = "降噪";
			this.btnTextCleaner.UseVisualStyleBackColor = true;
			this.btnTextCleaner.Click += new System.EventHandler(this.btnTextCleaner_Click);
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
			// bgwProcess
			// 
			this.bgwProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwProcess_DoWork);
			this.bgwProcess.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwProcess_RunWorkerCompleted);
			// 
			// tmrClearBox
			// 
			this.tmrClearBox.Tick += new System.EventHandler(this.tmrClearBox_Tick);
			// 
			// frmTrainingTool
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 522);
			this.Controls.Add(this.tabControl1);
			this.Font = new System.Drawing.Font("微软雅黑", 9F);
			this.Name = "frmTrainingTool";
			this.Text = "训练工具";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TrainingTool_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTrainingTool_FormClosed);
			this.Load += new System.EventHandler(this.TrainingTool_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgvBoxes)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbxExample)).EndInit();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton toolStripOpenImage;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.OpenFileDialog ofdFile;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.ComboBox cmbPreProcess;
		private System.Windows.Forms.CheckBox chkToTiff;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.Button btnTextCleaner;
		private System.ComponentModel.BackgroundWorker bgwProcess;
		private System.Windows.Forms.Timer tmrClearBox;
		private System.Windows.Forms.Timer tmrCheckMovement;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Button btnInsert;
		private System.Windows.Forms.Button btnMerge;
		private System.Windows.Forms.Button btnSplit;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtCharacter;
		private System.Windows.Forms.ToolStripButton toolStripBtnSaveBox;
		private System.Windows.Forms.Button btnDisplayBoxes;
		private System.Windows.Forms.DataGridView dgvBoxes;
		private System.Windows.Forms.NumericUpDown nudX;
		private System.Windows.Forms.NumericUpDown nudY;
		private System.Windows.Forms.NumericUpDown nudWidth;
		private System.Windows.Forms.NumericUpDown nudHeight;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.Button btnRotateLeft;
		private System.Windows.Forms.Button btnRotateRight;
		private System.Windows.Forms.Button btnMakeBox;
		private System.Windows.Forms.Button btnConvertToTiff;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.PictureBox pbxExample;
		private System.Windows.Forms.Button button1;
	}
}