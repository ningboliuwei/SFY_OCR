#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SFY_OCR.Properties;
using SFY_OCR.Untilities;

#endregion

namespace SFY_OCR
{
	public partial class TrainingTool : Form
	{
		//当前显示的图片OcrImage对象
		//矩形结束点
		private Point _boxEndPoint;
		//画矩形的状态
		private bool _boxIsDrawing;
		private Point _boxLeftTopPoint;
		private Point _boxStartPoint;
		//矩形图层
		private Bitmap _boxesImage;
		private Point _currentPoint;
		private OcrImage _displayingImage;
		//之前的图层（用于擦除矩形）

		//当前鼠标是否在移动
		private bool _isDrawingAndMoving;
		private OcrImage _originalImage;
		private Bitmap _previousImage;
		private BoxList boxList;

		//保存所有Box对象

		public TrainingTool()
		{
			InitializeComponent();
		}

		/// <summary>
		///     关闭窗体前的处理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
				_originalImage = new OcrImage(ofdFile.FileName);
				OpenImage(_originalImage);

				//得到样本图片文件的去掉后缀名的部分，再去掉 “_temp”后缀，并加上.box 后缀，得到对应的box文件的路径
				//string boxFilePath = _displayingImage.FilePath.Substring(0, _displayingImage.FilePath.LastIndexOf(StringResourceManager.TempImageSuffix)) + "." + _displayingImage.FilePath.Substring(_displayingImage.FilePath.LastIndexOf(".") + 1) + ".box";

				boxList = new BoxList(_originalImage.FilePath);

				//如果存在图片对应的box文件
				//if (File.Exists(boxFilePath))
				//{
				//将Box文件载入到BoxList对象中（窗体局部变量）
				//boxList.LoadFromFile(boxFilePath);
				//刷新，以显示图片
				//pbxExample.Refresh();
				//boxList.Display((Bitmap)pbxExample.Image);
				//pbxExample.Refresh();
				//pbxExample.Refresh();
				//pbxExample.CreateGraphics().DrawImage(boxesImage, 0, 0);
				//pbxExample.Image = boxList.Display((Bitmap)pbxExample.Image);
				//}
			}
		}


		/// <summary>
		///     打开样本图片，自动复制一份到输出目录，并将该副本显示在文本框中。
		/// </summary>
		/// <param name="sourceImage">原本（被打开）的图片（OcrImage）对象</param>
		private void OpenImage(OcrImage sourceImage)
		{
			string displayingImagePath = Settings.Default.OutputDir + sourceImage.MainFileName +
			                             StringResourceManager.TempImageSuffix + "." +
			                             sourceImage.ExtFileName;

			//将原本复制一份副本到输出目录
			File.Copy(sourceImage.FilePath, displayingImagePath, true);

			if (File.Exists(displayingImagePath))
			{
				_displayingImage = new OcrImage(displayingImagePath);
			}

			//显示该副本图片
			pbxExample.ImageLocation = _displayingImage.FilePath;
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
			string[] options = {"不处理", "灰度化", "黑白化"};

			cmbPreProcess.DataSource = options;
			cmbPreProcess.SelectedIndex = 0;

			//双缓存，防绘图闪烁
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			//设置用于清除之前画的矩形的定时器间隔（ms）
			tmrClearBox.Interval = 10;
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
		///     使用TextCleaner效果，放到背景线程调用
		/// </summary>
		/// <param name="sourceImage">要处理的OcrImage对象</param>
		/// <param name="convertArgs">效果处理参数</param>
		private void ConvertToTextCleaner(OcrImage sourceImage, Hashtable convertArgs)
		{
			string commandLineArgs =
				string.Format(
					"( {0} -colorspace gray -type grayscale -contrast-stretch 0 ) ( -clone 0 -colorspace gray -negate -lat {1}x{1}+{2}% -contrast-stretch 0 ) -compose copy_opacity -composite -fill \"{3}\" -opaque none +matte -deskew {4}% -sharpen 0x1  {5}",
					sourceImage.FilePath, convertArgs["filter_size"], convertArgs["off_set"], convertArgs["bgcolor"],
					convertArgs["deskew"], sourceImage.FilePath);

			Common.InvokeImageMagickConvertCommandLine(commandLineArgs);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			TextCleanerProcess();
		}

		/// <summary>
		///     进行文本降噪黑白高对比度操作
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
			button1.Text = trackBar1.Value.ToString();
			//ConvertToTextCleaner(pbxExample.ImageLocation, pbxExample.ImageLocation, trackBar1.Value, 10, "white");

			//if (File.Exists(pbxExample.ImageLocation))
			//{
			//	pbxExample.ImageLocation = pbxExample.ImageLocation;
			//}
		}

		/// <summary>
		///     背景线程处理图像
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bgwProcessImage_DoWork(object sender, DoWorkEventArgs e)
		{
			//拆箱，还原出delegate和hashtable形式的参数列表
			ConvertDelegatePackage package = (ConvertDelegatePackage) e.Argument;

			package.ConvertImageDelegate.Invoke(_displayingImage, package.ConvertImageArgs);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//恢复为最初的图片（副本）
			ResetImage();
		}

		/// <summary>
		///     复原到样本图片（重新进行一次打开图片处理）
		/// </summary>
		private void ResetImage()
		{
			OpenImage(_originalImage);
		}

		/// <summary>
		///     背景线程处理完毕
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bgwProcessImage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//当背景线程处理完毕后，显示处理完的图像
			pbxExample.ImageLocation = _displayingImage.FilePath;
		}

		private void pbxExample_MouseDown(object sender, MouseEventArgs e)
		{
			//开始进入画矩形状态
			_boxStartPoint = new Point(e.X, e.Y);
			_boxEndPoint = new Point(e.X, e.Y);
			_boxIsDrawing = true;
		}


		/// <summary>
		///     在图片上绘制用于识别的矩形
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbxExample_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (_boxIsDrawing)
				{
					//将当前鼠标坐标与之前记录下的鼠标坐标作比较
					_currentPoint = new Point(e.X, e.Y);

					if (e.X == _boxEndPoint.X && e.Y == _boxEndPoint.Y)
					{
						_isDrawingAndMoving = false;
					}
					else
					{
						_isDrawingAndMoving = true;
					}

					//得到拖动结束点坐标
					_boxEndPoint.X = e.X;
					_boxEndPoint.Y = e.Y;
				}
			}

			//只有在拖动矩形情况下才需要画矩形（并不断清除）
			if (_isDrawingAndMoving)
			{
				tmrClearBox.Start();
			}
			else
			{
				tmrClearBox.Stop();
			}
		}


		private void pbxExample_MouseUp(object sender, MouseEventArgs e)
		{
			_boxIsDrawing = false;

			//假如放松的是鼠标左键，停止清除矩形
			if (e.Button == MouseButtons.Left)
			{
				tmrClearBox.Stop();
			}

			if (_boxesImage != null)
			{
				pbxExample.CreateGraphics().DrawImage(_boxesImage, 0, 0);
			}


			//根据当前 创建一个Box对象
			Box box = new Box("", _boxLeftTopPoint.X, _boxLeftTopPoint.Y,
				Math.Abs(_boxEndPoint.X - _boxStartPoint.X),
				Math.Abs(_boxEndPoint.Y - _boxStartPoint.Y));

			boxList.Add(box);
			//boxList.Display((Bitmap)pbxExample.Image);
		}


		private void tmrClearBox_Tick(object sender, EventArgs e)
		{
			//判断矩形左上角的点的位置
			_boxLeftTopPoint = new Point();

			if (_boxEndPoint.X > _boxStartPoint.X)
			{
				_boxLeftTopPoint.X = _boxStartPoint.X;
			}
			else
			{
				_boxLeftTopPoint.X = _boxEndPoint.X;
			}

			if (_boxEndPoint.Y > _boxStartPoint.Y)
			{
				_boxLeftTopPoint.Y = _boxStartPoint.Y;
			}
			else
			{
				_boxLeftTopPoint.Y = _boxEndPoint.Y;
			}

			Graphics g = pbxExample.CreateGraphics();

			g.SmoothingMode = SmoothingMode.HighSpeed;

			//只有在绘图且移动情况下才画新的矩形
			if (_isDrawingAndMoving)
			{
				_boxesImage = new Bitmap(pbxExample.ClientSize.Width, pbxExample.ClientSize.Height);
				g = Graphics.FromImage(_boxesImage);
				g.SmoothingMode = SmoothingMode.HighSpeed;
				g.DrawRectangle(
					new Pen(Color.Red, Convert.ToInt32(StringResourceManager.BoxBorderWidth)),
					_boxLeftTopPoint.X,
					_boxLeftTopPoint.Y,
					Math.Abs(_boxEndPoint.X - _boxStartPoint.X),
					Math.Abs(_boxEndPoint.Y - _boxStartPoint.Y));


				//显示当前鼠标坐标
				button1.Text = _boxStartPoint.X + "," + _boxStartPoint.Y + "," + Math.Abs(_boxEndPoint.X - _boxStartPoint.X) + ","
				               + Math.Abs(_boxEndPoint.Y - _boxStartPoint.Y);

				//获取画矩形之前的图片（以便擦除）
				_previousImage = (Bitmap) pbxExample.Image;
				//将矩形真正画到picturebox上
				pbxExample.CreateGraphics().DrawImage(_boxesImage, 0, 0);

				Thread.Sleep(10);

				//只有在拖动矩形前提下，才清除
				if (_isDrawingAndMoving)
				{
					pbxExample.Image = _previousImage;
				}
			}
		}


		private void pbxExample_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				foreach (Box box in boxList.Boxes)
				{
					box.IsSelected = false;

					if (box.Contains(e.X, e.Y))
					{
						box.IsSelected = true;
					}
				}
			}

			boxList.Display((Bitmap) pbxExample.Image);
		}

		private void btnInsert_Click(object sender, EventArgs e)
		{
		}
	}
}