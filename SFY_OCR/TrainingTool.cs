using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SFY_OCR.Properties;

namespace SFY_OCR
{
	public partial class TrainingTool : Form
	{
		public TrainingTool()
		{
			InitializeComponent();
		}

		

		

		private void TrainingTool_FormClosing(object sender, FormClosingEventArgs e)
		{
			//关闭窗体
			if (MessageBox.Show(this, "确定退出训练工具吗?", "问题", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				e.Cancel = true;
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			//当要打开图片时，设置OpenFileDialog的filter属性
			ofdFile.Filter = "JPG|*.JPG;*.JPEG|TIFF|*.TIFF;*.TIF|所有文件|*.*";

			//打开样本图片
			if (ofdFile.ShowDialog() == DialogResult.OK)
			{
				string resultImagePath = ProcessImage(ofdFile.FileName);

				//若文件存在，显示该图片
				if (resultImagePath != "")
				{
					pbxExample.ImageLocation = resultImagePath;
				}
			}
		}

		/// <summary>
		/// 对图像进行处理
		/// </summary>
		/// <param name="imagePath">要处理的图像的路径</param>
		/// <returns>处理后的图片的路径</returns>
		private string ProcessImage(string imagePath)
		{
			//获取图片文件名
			string imageFileName = imagePath.Substring(imagePath.LastIndexOf("\\") + 1);
			//获取图片主文件名
			string mainFileName = imageFileName.Substring(0,imageFileName.LastIndexOf("."));
			//获取图片扩展名
			string extFileName = imageFileName.Substring(imageFileName.LastIndexOf(".") + 1);
			//结果图片文件路径
			string resultImagePath = Settings.Default.OutputDir + mainFileName + ".tif";


			//如果勾选了 “转换为TIFF格式”，则进行转换
			if (chkToTiff.Checked)
			{
				ConvertToTiff(imagePath, resultImagePath);
			}

			//
			switch (cmbPreProcess.Text)
			{
				case "灰度化":
					ConvertToGrayColorSpace(resultImagePath, resultImagePath);
					break;
				case "黑白化":
					ConvertToBlackAndWhite(resultImagePath, resultImagePath);
					break;

			}

			//若输出的图片存在，则返回路径，否则返回空字符串
			if (File.Exists(resultImagePath))
			{
				return resultImagePath;
			}

			return "";
		}

		private void TrainingTool_Load(object sender, EventArgs e)
		{
		

			//使图片框在显示较大的图片时增加滚动条
			pbxExample.SizeMode = PictureBoxSizeMode.AutoSize;
			panel1.AutoScroll = true;
			pbxExample.Dock = DockStyle.None;

			//添加下拉菜单选项
			string[] options = {"灰度化", "黑白化"};

			cmbPreProcess.DataSource = options;
			cmbPreProcess.SelectedIndex = 0;



		}

		private void ConvertToTiff(string inImagePath, string outImagePath)
		{
			string args = string.Format("-compress none {0} {1}", inImagePath, outImagePath);

			Common.InvokeImageMagickConvertCommandLine(Settings.Default.ImageMagickDir, args);
		}

		private void ConvertToGrayColorSpace(string inImagePath, string outImagePath)
		{
			string args = string.Format("-colorspace Gray {0} {1}", inImagePath, outImagePath);

			Common.InvokeImageMagickConvertCommandLine(Settings.Default.ImageMagickDir, args);
		}

		private void ConvertToBlackAndWhite(string inImagePath, string outImagePath)
		{
			string args = string.Format("-monochrome {0} {1}", inImagePath, outImagePath);

			Common.InvokeImageMagickConvertCommandLine(Settings.Default.ImageMagickDir, args);
		}
	}
}
