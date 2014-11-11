namespace SFY_OCR
{
	partial class frmProgressBar
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
			this.pbMain = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// pbMain
			// 
			this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbMain.Location = new System.Drawing.Point(0, 0);
			this.pbMain.Name = "pbMain";
			this.pbMain.Size = new System.Drawing.Size(284, 35);
			this.pbMain.TabIndex = 0;
			// 
			// frmProgressBar
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 35);
			this.ControlBox = false;
			this.Controls.Add(this.pbMain);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "frmProgressBar";
			this.Text = "进度";
			this.Load += new System.EventHandler(this.ProgressBar_Load);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.ProgressBar pbMain;



	}
}