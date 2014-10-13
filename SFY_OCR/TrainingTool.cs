using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using SFY_OCR.Properties;
using SFY_OCR.Untilities;

namespace SFY_OCR
{
	public partial class TrainingTool : Form
	{
		//当前显示的图片OcrImage对象
		private OcrImage displayingImage;
		//原本的图片OcrImage对象
		private OcrImage originalImage;

		//矩形开始点
		private Point boxStartPoint;
		//矩形结束点
		private Point boxEndPoint;
		//画矩形的状态
		private bool boxIsDrawing;

		Graphics g ;
		private GraphicsState state;

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



		/// <summary>
		/// 打开样本图片，自动复制一份到输出目录，并将该副本显示在文本框中。
		/// </summary>
		/// <param name="sourceImage">原本（被打开）的图片（OcrImage）对象</param>
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


			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);




		}

		//private void ConvertToTiff(OcrImage sourceImage)
		//{
		//	string args = string.Format("-compress none {0} {1}", sourceImage.FilePath, sourceImage.FilePath);

		//	Common.InvokeImageMagickConvertCommandLine(args);
		//}

		//private void ConvertToGrayColorSpace(string sourceImagePath, string resultImagePath)
		//{
		//	string args = string.Format("-colorspace Gray {0} {1}", sourceImagePath, resultImagePath);

		//	Common.InvokeImageMagickConvertCommandLine(args);
		//}

		//private void ConvertToBlackAndWhite(string sourceImagePath, string resultImagePath)
		//{
		//	string args = string.Format("-monochrome {0} {1}", sourceImagePath, resultImagePath);

		//	Common.InvokeImageMagickConvertCommandLine(args);
		//}

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

			Common.InvokeImageMagickConvertCommandLine(commandLineArgs);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			TextCleanerProcess();
		}

		/// <summary>
		/// 进行文本降噪黑白高对比度操作
		/// </summary>
		private void TextCleanerProcess()
		{
			ConvertDelegatePackage package = new ConvertDelegatePackage();

			//效果参数
			package.ConvertImageArgs.Add("filter_size", trackBar1.Value);
			package.ConvertImageArgs.Add("off_set", 10);
			package.ConvertImageArgs.Add("bgcolor", "white");
			//裁剪时候倾斜角
			package.ConvertImageArgs.Add("deskew", 0);

			//调用指向TextCleaner
			package.ConvertImageDelegate = ConvertToTextCleaner;
			bgwProcessImage.RunWorkerAsync(package);
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			//ConvertToTextCleaner(pbxExample.ImageLocation, pbxExample.ImageLocation, trackBar1.Value, 10, "white");

			if (File.Exists(pbxExample.ImageLocation))
			{
				pbxExample.ImageLocation = pbxExample.ImageLocation;
			}
		}

		/// <summary>
		/// 背景线程处理图像
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bgwProcessImage_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			//拆箱，还原出delegate和hashtable形式的参数列表
			ConvertDelegatePackage package = (ConvertDelegatePackage)e.Argument;

			package.ConvertImageDelegate.Invoke(displayingImage, package.ConvertImageArgs);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//恢复为最初的图片（副本）
			ResetImage();


		}

		/// <summary>
		/// 复原到样本图片（重新进行一次打开图片处理）
		/// </summary>
		private void ResetImage()
		{
			OpenImage(originalImage);
		}

		/// <summary>
		/// 背景线程处理完毕
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bgwProcessImage_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			//当背景线程处理完毕后，显示处理完的图像
			pbxExample.ImageLocation = displayingImage.FilePath;
		}

		private void pbxExample_MouseDown(object sender, MouseEventArgs e)
		{

			//开始进入画矩形状态
			boxStartPoint = new Point(e.X, e.Y);
			boxEndPoint = new Point(e.X, e.Y);
			boxIsDrawing = true;
			g = pbxExample.CreateGraphics();
			
		}


		/// <summary>
		/// 要分拖动的方向
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbxExample_MouseMove(object sender, MouseEventArgs e)
		{
			
			
			if (e.Button == MouseButtons.Left)
			{
				if (boxIsDrawing)
				{
					// 初始化画板
					Bitmap image = new Bitmap(pbxExample.ClientSize.Width, pbxExample.ClientSize.Height);

					// 获取背景层
					//Bitmap bg = (Bitmap)pbxExample.Image;

					// 初始化画布
					//Bitmap canvas = new Bitmap(pbxExample.ClientSize.Width, pbxExample.ClientSize.Height);

					// 初始化图形面板
					//Graphics g = Graphics.FromImage(image);
					//Graphics gb = Graphics.FromImage(canvas);

					// 绘图部分 Begin

					//得到拖动结束点坐标
					boxEndPoint.X = e.X;
					boxEndPoint.Y = e.Y;

					//判断矩形左上角的点的位置
					Point boxLeftTopPoint = new Point();

					if (boxEndPoint.X > boxStartPoint.X)
					{
						boxLeftTopPoint.X = boxStartPoint.X;
					}
					else
					{
						boxLeftTopPoint.X = boxEndPoint.X;
					}

					if (boxEndPoint.Y > boxStartPoint.Y)
					{
						boxLeftTopPoint.Y = boxStartPoint.Y;
					}
					else
					{
						boxLeftTopPoint.Y = boxEndPoint.Y;
					}

					//绘制矩形
					Graphics.FromImage(image).DrawRectangle(new Pen(Color.Red, 3), boxLeftTopPoint.X, boxLeftTopPoint.Y, Math.Abs(boxEndPoint.X - boxStartPoint.X),
				Math.Abs(boxEndPoint.Y - boxStartPoint.Y));

					//显示当前鼠标坐标
					button1.Text = boxStartPoint.X + "," + boxStartPoint.Y + "," + Math.Abs(boxEndPoint.X - boxStartPoint.X) + "," +
								   Math.Abs(boxEndPoint.Y - boxStartPoint.Y);
					
					g.SmoothingMode = SmoothingMode.HighSpeed;


					//pbxExample.Invalidate();
					//g.SmoothingMode = SmoothingMode.AntiAlias;//消除锯齿  
					//g.DrawEllipse(new Pen(Color.Red),e.X,e.Y,10,10 );
					//state = g.Save();
					//pbxExample.Refresh();
					// 绘图部分 End

					//gb.DrawImage(bg, 0, 0); // 先绘制背景层
					//gb.DrawImage(image, 0, 0); // 再绘制绘画层

					//pbxExample.BackgroundImage = canvas; // 设置为背景层

					//pbxExample.Refresh();
					//pbxExample.CreateGraphics().DrawImage(canvas, 0, 0);

					pbxExample.Image = image;




				}

			}
		}

		private void DrawBox(Point boxLeftTopPoint)
		{
			
		}

		private void pbxExample_MouseUp(object sender, MouseEventArgs e)
		{
			boxIsDrawing = false;
			

		}

		private void pbxExample_Paint(object sender, PaintEventArgs e)
		{
			//state = e.Graphics.Save();
			//Invalidate();
			//e.Graphics.Clear(Color.White);
		}


	}
}