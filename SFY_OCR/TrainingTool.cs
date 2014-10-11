using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using SFY_OCR.Properties;
using SFY_OCR.Untilities;

namespace SFY_OCR
{
	public partial class TrainingTool : Form
	{
		private OcrImage displayingImage;
		private OcrImage originalImage;

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
			ofdFile.Filter = StringResourceManager.OpenImageFilterText;

			//打开样本图片
			if (ofdFile.ShowDialog() == DialogResult.OK)
			{
				//创建一个原本OcrImage对象（用于恢复）
				originalImage = new OcrImage(ofdFile.FileName);
				OpenImage(originalImage);

			}
		}

		public delegate void ConvertImageDelegate(OcrImage sourceImage, Hashtable convertArgs);



		/// <summary>
		/// 打开样本图片，自动复制一份到输出目录，并将该副本显示在文本框中。
		/// </summary>
		/// <param name="sourceImage"></param>
		private void OpenImage(OcrImage sourceImage)
		{
			string displayingImagePath = Settings.Default.OutputDir + sourceImage.MainFileName + "_temp." + sourceImage.ExtFileName;

			//将原本复制一份副本到输出目录
			File.Copy(sourceImage.FilePath, displayingImagePath, true);

			if (File.Exists(displayingImagePath))
			{
				displayingImage = new OcrImage(displayingImagePath);
			}

			//显示该副本图片
			pbxExample.ImageLocation = displayingImage.FilePath;
		}


		/// <summary>
		///     对图像进行处理
		/// </summary>
		/// <param name="imagePath">要处理的图像的路径</param>
		/// <returns>处理后的图片的路径</returns>
		private string ProcessImage(OcrImage sourceImage)
		{
			//复制一份源样本到输出目录并显示
			//File.Copy(imagePath, resultImagePath, true);

			//pbxExample.Image=


			//如果勾选了 “转换为TIFF格式”，则进行转换
			//注意后缀名的问题
			//if (chkToTiff.Checked)
			//{
			//	ConvertToTiff(imagePath, resultImagePath);
			//}

			//
			//switch (cmbPreProcess.Text)
			//{

			//	case "灰度化":
			//		ConvertToGrayColorSpace(resultImagePath, resultImagePath);
			//		break;
			//	case "黑白化":
			//		ConvertToBlackAndWhite(resultImagePath, resultImagePath);
			//		break;

			//}

			//if (chkDenoise.Checked)
			//{
			//	ConvertToTextCleaner(imagePath, resultImagePath, 45, 10, "white");
			//}

			//ConvertToTextCleaner(imagePath, resultImagePath, 45, 10, "white");

			//若输出的图片存在，则返回路径，否则返回空字符串
			//if (File.Exists(resultImagePath))
			//{
			//	return resultImagePath;
			//}

			return "";
		}

		private void TrainingTool_Load(object sender, EventArgs e)
		{
			//使图片框在显示较大的图片时增加滚动条
			pbxExample.SizeMode = PictureBoxSizeMode.AutoSize;
			panel1.AutoScroll = true;
			pbxExample.Dock = DockStyle.None;

			//添加下拉菜单选项
			string[] options = { "不处理", "灰度化", "黑白化" };

			cmbPreProcess.DataSource = options;
			cmbPreProcess.SelectedIndex = 0;
		}

		private void ConvertToTiff(OcrImage sourceImage)
		{
			string args = string.Format("-compress none {0} {1}", sourceImage.FilePath, sourceImage.FilePath);

			Common.InvokeImageMagickConvertCommandLine(Settings.Default.ImageMagickDir, args);
		}

		private void ConvertToGrayColorSpace(string sourceImagePath, string resultImagePath)
		{
			string args = string.Format("-colorspace Gray {0} {1}", sourceImagePath, resultImagePath);

			Common.InvokeImageMagickConvertCommandLine(Settings.Default.ImageMagickDir, args);
		}

		private void ConvertToBlackAndWhite(string sourceImagePath, string resultImagePath)
		{
			string args = string.Format("-monochrome {0} {1}", sourceImagePath, resultImagePath);

			Common.InvokeImageMagickConvertCommandLine(Settings.Default.ImageMagickDir, args);
		}

		/// <summary>
		/// 使用TextCleaner效果，放到背景线程调用
		/// </summary>
		/// <param name="sourceImage">要处理的OcrImage对象</param>
		/// <param name="convertArgs">效果处理参数</param>
		private void ConvertToTextCleaner(OcrImage sourceImage, Hashtable convertArgs)
		{
			string commandLineArgs =
				string.Format(
					"( {0} -colorspace gray -type grayscale -contrast-stretch 0 ) ( -clone 0 -colorspace gray -negate -lat {1}x{1}+{2}% -contrast-stretch 0 ) -compose copy_opacity -composite -fill \"{3}\" -opaque none +matte -deskew {4}% -sharpen 0x1  {5}",
					sourceImage.FilePath, convertArgs["filter_size"], convertArgs["off_set"], convertArgs["bgcolor"], convertArgs["deskew"], sourceImage.FilePath);

			Common.InvokeImageMagickConvertCommandLine(Settings.Default.OutputDir, commandLineArgs);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			TextCleaner();
		}

		/// <summary>
		/// 进行文本降噪黑白高对比度操作
		/// </summary>
		private void TextCleaner()
		{
			//效果参数
			Hashtable convertArgs = new Hashtable();

			convertArgs.Add("filter_size", trackBar1.Value);
			convertArgs.Add("off_set", 10);
			convertArgs.Add("bgcolor", "white");
			//裁剪时候倾斜角
			convertArgs.Add("deskew", 0);


			ConvertImageDelegate convertImageDelegate = ConvertToTextCleaner;

			bgwProcessImage.RunWorkerAsync(new ArrayList() { convertImageDelegate, convertArgs });

			//convertImageDelegate.Invoke(displayingImage, convertArgs);

			//pbxExample.ImageLocation = displayingImage.FilePath;

			//if (File.Exists(pbxExample.ImageLocation))
			//{
			//	pbxExample.ImageLocation = pbxExample.ImageLocation;
			//}
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			//ConvertToTextCleaner(pbxExample.ImageLocation, pbxExample.ImageLocation, trackBar1.Value, 10, "white");

			if (File.Exists(pbxExample.ImageLocation))
			{
				pbxExample.ImageLocation = pbxExample.ImageLocation;
			}
		}

		private void bgwProcessImage_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			//将传进来的Argument对象变为存放了调用Convert函数委托以及各种转换参数的ArrayList对象
			//第1个为委托
			//第2个为参数列表
			ArrayList arrayList = (ArrayList)e.Argument;

			((ConvertImageDelegate)arrayList[0]).Invoke(displayingImage, (Hashtable)arrayList[1]);


		}

		private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			ResetImage();
		}

		/// <summary>
		/// 复原到样本图片
		/// </summary>
		private void ResetImage()
		{
			OpenImage(originalImage);
		}
	}
}