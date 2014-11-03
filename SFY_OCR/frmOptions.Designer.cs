namespace SFY_OCR
{
	partial class frmOptions
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.fbdChooseDir = new System.Windows.Forms.FolderBrowserDialog();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.basicOptions = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.txtOutputDir = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnChooseOutputDir = new System.Windows.Forms.Button();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.txtTesseractOcrDir = new System.Windows.Forms.TextBox();
			this.btnChooseTesseractOcrDir = new System.Windows.Forms.Button();
			this.trainingOptions = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.btnDefault = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.txtImageMagickDir = new System.Windows.Forms.TextBox();
			this.btnChooseImageMagickDir = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.basicOptions.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.trainingOptions.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel6.SuspendLayout();
			this.tableLayoutPanel8.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.18633F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.81367F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(504, 161);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.basicOptions);
			this.tabControl1.Controls.Add(this.trainingOptions);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(3, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(498, 106);
			this.tabControl1.TabIndex = 1;
			// 
			// basicOptions
			// 
			this.basicOptions.Controls.Add(this.tableLayoutPanel2);
			this.basicOptions.Location = new System.Drawing.Point(4, 26);
			this.basicOptions.Name = "basicOptions";
			this.basicOptions.Padding = new System.Windows.Forms.Padding(3);
			this.basicOptions.Size = new System.Drawing.Size(490, 76);
			this.basicOptions.TabIndex = 0;
			this.basicOptions.Text = "基本设置";
			this.basicOptions.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 490F));
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(484, 70);
			this.tableLayoutPanel2.TabIndex = 1;
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 3;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel4.Controls.Add(this.txtOutputDir, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.btnChooseOutputDir, 2, 0);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 38);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 1;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(484, 29);
			this.tableLayoutPanel4.TabIndex = 21;
			// 
			// txtOutputDir
			// 
			this.txtOutputDir.BackColor = System.Drawing.SystemColors.Window;
			this.txtOutputDir.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtOutputDir.Location = new System.Drawing.Point(187, 3);
			this.txtOutputDir.Name = "txtOutputDir";
			this.txtOutputDir.ReadOnly = true;
			this.txtOutputDir.Size = new System.Drawing.Size(244, 23);
			this.txtOutputDir.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(178, 29);
			this.label2.TabIndex = 1;
			this.label2.Text = "识别结果输出文件夹：";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnChooseOutputDir
			// 
			this.btnChooseOutputDir.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnChooseOutputDir.Location = new System.Drawing.Point(437, 3);
			this.btnChooseOutputDir.Name = "btnChooseOutputDir";
			this.btnChooseOutputDir.Size = new System.Drawing.Size(44, 23);
			this.btnChooseOutputDir.TabIndex = 4;
			this.btnChooseOutputDir.Text = "...";
			this.btnChooseOutputDir.UseVisualStyleBackColor = true;
			this.btnChooseOutputDir.Click += new System.EventHandler(this.btnChooseOutputDir_Click);
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 3;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel5.Controls.Add(this.txtTesseractOcrDir, 1, 0);
			this.tableLayoutPanel5.Controls.Add(this.btnChooseTesseractOcrDir, 2, 0);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 1;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(484, 29);
			this.tableLayoutPanel5.TabIndex = 20;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(178, 29);
			this.label1.TabIndex = 6;
			this.label1.Text = "tesseract.exe 所在文件夹：";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtTesseractOcrDir
			// 
			this.txtTesseractOcrDir.BackColor = System.Drawing.SystemColors.Window;
			this.txtTesseractOcrDir.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTesseractOcrDir.Location = new System.Drawing.Point(187, 3);
			this.txtTesseractOcrDir.Name = "txtTesseractOcrDir";
			this.txtTesseractOcrDir.ReadOnly = true;
			this.txtTesseractOcrDir.Size = new System.Drawing.Size(244, 23);
			this.txtTesseractOcrDir.TabIndex = 4;
			// 
			// btnChooseTesseractOcrDir
			// 
			this.btnChooseTesseractOcrDir.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnChooseTesseractOcrDir.Location = new System.Drawing.Point(437, 3);
			this.btnChooseTesseractOcrDir.Name = "btnChooseTesseractOcrDir";
			this.btnChooseTesseractOcrDir.Size = new System.Drawing.Size(44, 23);
			this.btnChooseTesseractOcrDir.TabIndex = 5;
			this.btnChooseTesseractOcrDir.Text = "...";
			this.btnChooseTesseractOcrDir.UseVisualStyleBackColor = true;
			this.btnChooseTesseractOcrDir.Click += new System.EventHandler(this.btnChooseTesseractOcrDir_Click);
			// 
			// trainingOptions
			// 
			this.trainingOptions.Controls.Add(this.tableLayoutPanel6);
			this.trainingOptions.Location = new System.Drawing.Point(4, 26);
			this.trainingOptions.Name = "trainingOptions";
			this.trainingOptions.Padding = new System.Windows.Forms.Padding(3);
			this.trainingOptions.Size = new System.Drawing.Size(490, 76);
			this.trainingOptions.TabIndex = 1;
			this.trainingOptions.Text = "训练设置";
			this.trainingOptions.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel3.Controls.Add(this.btnDefault, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.btnCancel, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.btnOK, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 115);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(498, 43);
			this.tableLayoutPanel3.TabIndex = 20;
			// 
			// btnDefault
			// 
			this.btnDefault.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnDefault.Location = new System.Drawing.Point(335, 3);
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.Size = new System.Drawing.Size(160, 37);
			this.btnDefault.TabIndex = 3;
			this.btnDefault.Text = "默认(&D)";
			this.btnDefault.UseVisualStyleBackColor = true;
			this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnCancel.Location = new System.Drawing.Point(169, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(160, 37);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "取消(&C)";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnOK.Location = new System.Drawing.Point(3, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(160, 37);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "确定(&O)";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// tableLayoutPanel6
			// 
			this.tableLayoutPanel6.ColumnCount = 1;
			this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 490F));
			this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 1);
			this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel8, 0, 0);
			this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel6.Name = "tableLayoutPanel6";
			this.tableLayoutPanel6.RowCount = 2;
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel6.Size = new System.Drawing.Size(484, 70);
			this.tableLayoutPanel6.TabIndex = 2;
			// 
			// tableLayoutPanel7
			// 
			this.tableLayoutPanel7.ColumnCount = 3;
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
			this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 38);
			this.tableLayoutPanel7.Name = "tableLayoutPanel7";
			this.tableLayoutPanel7.RowCount = 1;
			this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel7.Size = new System.Drawing.Size(484, 29);
			this.tableLayoutPanel7.TabIndex = 21;
			// 
			// tableLayoutPanel8
			// 
			this.tableLayoutPanel8.ColumnCount = 3;
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
			this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel8.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel8.Controls.Add(this.txtImageMagickDir, 1, 0);
			this.tableLayoutPanel8.Controls.Add(this.btnChooseImageMagickDir, 2, 0);
			this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel8.Name = "tableLayoutPanel8";
			this.tableLayoutPanel8.RowCount = 1;
			this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel8.Size = new System.Drawing.Size(484, 29);
			this.tableLayoutPanel8.TabIndex = 20;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Location = new System.Drawing.Point(3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(178, 29);
			this.label4.TabIndex = 6;
			this.label4.Text = "ImageMagick 所在文件夹：";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtImageMagickDir
			// 
			this.txtImageMagickDir.BackColor = System.Drawing.SystemColors.Window;
			this.txtImageMagickDir.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtImageMagickDir.Location = new System.Drawing.Point(187, 3);
			this.txtImageMagickDir.Name = "txtImageMagickDir";
			this.txtImageMagickDir.ReadOnly = true;
			this.txtImageMagickDir.Size = new System.Drawing.Size(244, 23);
			this.txtImageMagickDir.TabIndex = 4;
			// 
			// btnChooseImageMagickDir
			// 
			this.btnChooseImageMagickDir.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnChooseImageMagickDir.Location = new System.Drawing.Point(437, 3);
			this.btnChooseImageMagickDir.Name = "btnChooseImageMagickDir";
			this.btnChooseImageMagickDir.Size = new System.Drawing.Size(44, 23);
			this.btnChooseImageMagickDir.TabIndex = 5;
			this.btnChooseImageMagickDir.Text = "...";
			this.btnChooseImageMagickDir.UseVisualStyleBackColor = true;
			this.btnChooseImageMagickDir.Click += new System.EventHandler(this.btnChooseImageMagickDir_Click);
			// 
			// Options
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(504, 161);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Options";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "选项";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Options_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.basicOptions.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.trainingOptions.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel6.ResumeLayout(false);
			this.tableLayoutPanel8.ResumeLayout(false);
			this.tableLayoutPanel8.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.FolderBrowserDialog fbdChooseDir;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Button btnDefault;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage basicOptions;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.TextBox txtOutputDir;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnChooseOutputDir;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtTesseractOcrDir;
		private System.Windows.Forms.Button btnChooseTesseractOcrDir;
		private System.Windows.Forms.TabPage trainingOptions;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtImageMagickDir;
		private System.Windows.Forms.Button btnChooseImageMagickDir;

	}
}