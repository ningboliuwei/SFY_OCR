using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SFY_OCR
{
	public partial class frmProgressBar : Form
	{
		public frmProgressBar()
		{
			InitializeComponent();

			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.TopMost = true;
			this.ControlBox = false;
			this.StartPosition = FormStartPosition.CenterScreen;
		}

		private void ProgressBar_Load(object sender, EventArgs e)
		{
			
		}
	}
}
