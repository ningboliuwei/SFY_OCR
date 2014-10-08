namespace SFY_OCR
{
	partial class Options
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.basicOptions = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.txtOutputDir = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnChooseOutputDir = new System.Windows.Forms.Button();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.btnDefault = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.txtTesseractOcrDir = new System.Windows.Forms.TextBox();
			this.btnChooseTesseractOcrDir = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.fbdChooseDir = new System.Windows.Forms.FolderBrowserDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.basicOptions.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.basicOptions);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(504, 161);
			this.tabControl1.TabIndex = 0;
			// 
			// basicOptions
			// 
			this.basicOptions.Controls.Add(this.tableLayoutPanel1);
			this.basicOptions.Location = new System.Drawing.Point(4, 26);
			this.basicOptions.Name = "basicOptions";
			this.basicOptions.Padding = new System.Windows.Forms.Padding(3);
			this.basicOptions.Size = new System.Drawing.Size(496, 131);
			this.basicOptions.TabIndex = 0;
			this.basicOptions.Text = "基本设置";
			this.basicOptions.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 378F));
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(490, 125);
			this.tableLayoutPanel1.TabIndex = 1;
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
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel2.Controls.Add(this.btnDefault, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnCancel, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.btnOK, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 73);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(484, 49);
			this.tableLayoutPanel2.TabIndex = 19;
			// 
			// btnDefault
			// 
			this.btnDefault.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnDefault.Location = new System.Drawing.Point(325, 3);
			this.btnDefault.Name = "btnDefault";
			this.btnDefault.Size = new System.Drawing.Size(156, 43);
			this.btnDefault.TabIndex = 3;
			this.btnDefault.Text = "默认(&D)";
			this.btnDefault.UseVisualStyleBackColor = true;
			this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnCancel.Location = new System.Drawing.Point(164, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(155, 43);
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
			this.btnOK.Size = new System.Drawing.Size(155, 43);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "确定(&O)";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 250F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.txtTesseractOcrDir, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.btnChooseTesseractOcrDir, 2, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(484, 29);
			this.tableLayoutPanel3.TabIndex = 20;
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
			// Options
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(504, 161);
			this.Controls.Add(this.tabControl1);
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
			this.tabControl1.ResumeLayout(false);
			this.basicOptions.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage basicOptions;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.TextBox txtOutputDir;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnChooseOutputDir;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button btnDefault;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TextBox txtTesseractOcrDir;
		private System.Windows.Forms.Button btnChooseTesseractOcrDir;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.FolderBrowserDialog fbdChooseDir;
		private System.Windows.Forms.Label label1;

	}
}