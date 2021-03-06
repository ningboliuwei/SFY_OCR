﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SFY_OCR.Properties;

namespace SFY_OCR
{
	public partial class Options : Form
	{
		public Options()
		{
			InitializeComponent();
		}


		/// <summary>
		/// 点击“默认”按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDefault_Click(object sender, EventArgs e)
		{
			if (
				MessageBox.Show(this, "确定恢复为默认设置吗?", "问题", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
					MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				//恢复到默认设置
				Settings.Default.Reset();
				LoadSettings();
			}
		}

		/// <summary>
		/// 点击“确定”按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOK_Click(object sender, EventArgs e)
		{
			SaveSettings();
			Close();
		}

		/// <summary>
		/// 加载当前窗体（选项对话框）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Options_Load(object sender, EventArgs e)
		{
			LoadSettings();
		}

		/// <summary>
		/// 点击“取消”按钮
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// 保存当前设置
		/// </summary>
		private void SaveSettings()
		{
			Settings settings = Settings.Default;

			settings.TesseractOcrDir = txtTesseractOcrDir.Text.Trim();
			settings.OutputDir = txtOutputDir.Text.Trim();

			settings.Save();
		}

		/// <summary>
		/// 载入当前设置
		/// </summary>
		private void LoadSettings()
		{
			Settings settings = Settings.Default;

			txtTesseractOcrDir.Text = settings.TesseractOcrDir;
			txtOutputDir.Text = settings.OutputDir;
		}

		private void btnChooseTesseractOcrDir_Click(object sender, EventArgs e)
		{
			//选择Tesseract-OCR所在文件夹
			if (fbdChooseDir.ShowDialog() == DialogResult.OK)
			{
				try
				{
					txtTesseractOcrDir.Text = fbdChooseDir.SelectedPath;
				}
				catch (Exception exception)
				{
					throw new Exception(exception.Message);
				}
			}
		}

		private void btnChooseOutputDir_Click(object sender, EventArgs e)
		{
			//选择输出文件夹
			if (fbdChooseDir.ShowDialog() == DialogResult.OK)
			{
				try
				{
					txtOutputDir.Text = fbdChooseDir.SelectedPath;
				}
				catch (Exception exception)
				{
					throw new Exception(exception.Message);
				}
			}
		}
	}
}
