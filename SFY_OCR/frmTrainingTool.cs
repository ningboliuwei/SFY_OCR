#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SFY_OCR.Untilities;

#endregion

namespace SFY_OCR
{
	public partial class frmTrainingTool : Form
	{
		//当前显示的图片OcrImage对象
		//矩形结束点
		//private Point _currentPoint;
		//画矩形的状态
		public delegate Bitmap GetNewBoxImageDelegate(Bitmap bitmap);

		public enum MouseMoveState
		{
			DoNothing,
			ResizingBox,
			DrawingBox,
			MovingBox
		}

		private Point _boxLeftTopPoint;
		//矩形图层
		private Bitmap _boxesImage;
		private MouseMoveState _currentMouseMoveState = MouseMoveState.DoNothing;
		private Point _currentPoint;
		private Cursor _cursor;
		private bool _isDrawingBox;

		//当前鼠标是否在移动
		//是否在移动 Box
		private bool _isMovingBox;
		//是否在改变 Box 大小
		private bool _isSizingBox;
		private OcrImage _ocrImage;
		//之前的图层（用于擦除矩形）
		private Bitmap _previousImage;
		private Point _startPoint;
		//按下鼠标左键之前的光标形状

		private AnchorPositionType anchorPosition;
		private Bitmap bitmap;

		private frmProgressBar progressBar;


		public frmTrainingTool()
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
			else
			{
				GC.Collect();
			}
		}


		/// <summary>
		///     打开样本图片，自动复制一份到输出目录，并将该副本显示在文本框中。
		/// </summary>
		/// <param name="sourceImage">原本（被打开）的图片（OcrImage）对象</param>
		private void OpenImage(OcrImage sourceImage)
		{
			//将原本复制一份副本到输出目录
			Bitmap background;
			try
			{
				File.Copy(sourceImage.OriginalImageInfo.FilePath, sourceImage.TempImageInfo.FilePath, true);

				//将要识别的图片作为背景显示到图片框中
				background = new Bitmap(_ocrImage.TempImageInfo.FilePath);

				
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			pbxExample.Width = background.Width;
			pbxExample.Height = background.Height;
			pbxExample.BackgroundImage = background;
			//建立一个透明的前景图片，用于显示 Box
			pbxExample.Image = new Bitmap(background.Width, background.Height);

			//若box存在，则加载对应的 box 文件
			if (File.Exists(_ocrImage.BoxFileInfo.FilePath))
			{
				//显示 Box
				_ocrImage.LoadFromBoxFile();
				pbxExample.Image = _ocrImage.GetBoxesImage(pbxExample.Image as Bitmap);
				pbxExample.Update();

				//在数据网格中显示 Box 信息
				BindBoxesInfoInGridView();
			}
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

			//双缓存，防绘图闪烁
			//SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			//SetStyle(ControlStyles.UserPaint, true);
			//SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			//设置选择模式为全行
			dgvBoxes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvBoxes.ReadOnly = true;
			//提高显示性能
			dgvBoxes.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dgvBoxes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

			//设置 NumericUpDown 的最大值（最小值默认都为 0 ）
			nudX.Maximum = int.MaxValue;
			nudY.Maximum = int.MaxValue;
			nudWidth.Maximum = int.MaxValue;
			nudHeight.Maximum = int.MaxValue;
		}


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


		private void btnTextCleaner_Click(object sender, EventArgs e)
		{
			pbxExample.Image.Dispose();
			//进行文本降噪黑白高对比度操作
			Dictionary<string, string> args = new Dictionary<string, string>
			{
				{"sourceImagePath", _ocrImage.TempImageInfo.FilePath},
				{"destImagePath", _ocrImage.TempImageInfo.FilePath},
				{"filter_size", trackBar1.Value.ToString()},
				{"off_set", 10.ToString()},
				{"bgcolor", "white"},
				//裁剪时候倾斜角
				{"deskew", 0.ToString()}
			};

			//调用指向TextCleaner
			ImageProcess imageProcess = new TextCleanerImageProcess(args);
			bgwProcess.RunWorkerAsync(imageProcess);

			while (bgwProcess.IsBusy)
			{
				Application.DoEvents();
			}

			pbxExample.BackgroundImage = new Bitmap(_ocrImage.TempImageInfo.FilePath);
			pbxExample.Image = _ocrImage.GetBoxesImage(pbxExample.BackgroundImage as Bitmap);
			pbxExample.Refresh();
		}


		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			//button1.Text = trackBar1.Value.ToString();
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
		private void bgwProcess_DoWork(object sender, DoWorkEventArgs e)
		{
			if (e.Argument is CommandLineProcess)
			{
				//拆箱，还原出delegate和hashtable形式的参数列表
				(e.Argument as CommandLineProcess).Process();
			}
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
			OpenImage(_ocrImage);
		}

		/// <summary>
		///     背景线程处理完毕
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bgwProcess_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//当背景线程处理完毕后，显示处理完的（临时）图像
			//try
			//{
			//	//pbxExample.Image = new Bitmap(_ocrImage.TempImageInfo.FilePath);

			//	//if (File.Exists(_ocrImage.BoxFileInfo.FilePath))
			//	//{
			//	//	RefreshBoxesInPictureBox();
			//	//}
			//}
			//catch (Exception ex)
			//{
			//	throw new Exception(ex.Message);
			//}
			if (progressBar != null)
			{
				progressBar.pbMain.Value = 100;
				Thread.Sleep(1000);
				progressBar.Close();
			}
		}

		private void pbxExample_MouseDown(object sender, MouseEventArgs e)
		{
			//按下左键才进入画 box 模式
			//if (e.Button == MouseButtons.Left)
			//{
			//	_isDrawingBox = true;
			//}


			if (e.Button == MouseButtons.Left)
			{
				Point currentPoint = new Point();

				_startPoint = new Point(e.X, e.Y);

				if (anchorPosition == AnchorPositionType.MiddleMiddle)
				{
					_isMovingBox = true;
				}
				else if (anchorPosition == AnchorPositionType.None)
				{
					//切换到画 Box 模式
					_currentMouseMoveState = MouseMoveState.DrawingBox;
					//bitmap = pbxExample.Image.Clone() as Bitmap;
				}
				else
				{
					_isSizingBox = true;
					_cursor = pbxExample.Cursor;
				}
			}
		}

		/// <summary>
		///     在图片上绘制用于识别的矩形
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbxExample_MouseMove(object sender, MouseEventArgs e)
		{
			Point currentPoint = new Point(e.X, e.Y);

			////不处于拖动 Box 大小情况下才改变光标
			//if (!_isSizingBox && _ocrImage.ImageBoxList.SelectedBoxes.Count == 1)
			//{
			//	ChangeBoxCursor(e.X, e.Y);
			//}
			////FIXME 多Box被选中情况下

			//Box box;

			//if (_ocrImage.ImageBoxList.SelectedBoxes.Count >= 1)
			//{
			//	box = _ocrImage.ImageBoxList.SelectedBoxes[0];


			//	//处于拖动状态
			//	if (_isMovingBox)
			//	{
			//		Pen dashPen = new Pen(Color.DarkRed, 1);
			//		dashPen.DashStyle = DashStyle.Dash;
			//		pbxExample.Invalidate();
			//		pbxExample.CreateGraphics()
			//			.DrawRectangle(dashPen, box.X + (e.X - _startPoint.X), box.Y + (e.Y - _startPoint.Y), box.Width, box.Height);
			//	}
			//}

			if (_currentMouseMoveState == MouseMoveState.DrawingBox)
			{
				//pbxExample.Refresh();
				//pbxExample.Image = bitmap;

				Pen pen = new Pen(new SolidBrush(Color.Red));
				pen.Width = 1;
				pen.DashStyle = DashStyle.Dash;

				Point boxLeftTopPoint = new Point();

				//判断不同的拖动方向，并计算画 box 的起始点
				if (currentPoint.X > _startPoint.X)
				{
					boxLeftTopPoint.X = _startPoint.X;
				}
				else
				{
					boxLeftTopPoint.X = currentPoint.X;
				}

				if (currentPoint.Y > _startPoint.Y)
				{
					boxLeftTopPoint.Y = _startPoint.Y;
				}
				else
				{
					boxLeftTopPoint.Y = currentPoint.Y;
				}

				Bitmap buffer = _ocrImage.GetBoxesImage(pbxExample.Image as Bitmap);
				Graphics g = Graphics.FromImage(buffer);

				g.DrawRectangle(
					pen,
					boxLeftTopPoint.X,
					boxLeftTopPoint.Y,
					Math.Abs(currentPoint.X - _startPoint.X),
					Math.Abs(currentPoint.Y - _startPoint.Y));
				pbxExample.Image = buffer;
				pbxExample.Update();
			}

			//Graphics g = pbxExample.CreateGraphics();
			//g.SmoothingMode = SmoothingMode.HighSpeed;


			//pbxExample.Invalidate();
			//pbxExample.Refresh();
			//TOFIX
			//if (e.Button == MouseButtons.Left)
			//{
			//	if (_isDrawingBox)
			//	{
			//		//将当前鼠标坐标与之前记录下的鼠标坐标作比较
			//		_currentPoint = new Point(e.X, e.Y);

			//		//if (e.X == _boxEndPoint.X && e.Y == _boxEndPoint.Y)
			//		//{
			//		//	_isDrawingAndMoving = false;
			//		//}
			//		//else
			//		//{
			//		//	_isDrawingAndMoving = true;
			//		//}

			//		//得到拖动结束点坐标
			//		_boxEndPoint.X = e.X;
			//		_boxEndPoint.Y = e.Y;

			//		_boxLeftTopPoint = new Point();

			//		//判断不同的拖动方向，并计算画 box 的范围
			//		if (_boxEndPoint.X > _boxStartPoint.X)
			//		{
			//			_boxLeftTopPoint.X = _boxStartPoint.X;
			//		}
			//		else
			//		{
			//			_boxLeftTopPoint.X = _boxEndPoint.X;
			//		}

			//		if (_boxEndPoint.Y > _boxStartPoint.Y)
			//		{
			//			_boxLeftTopPoint.Y = _boxStartPoint.Y;
			//		}
			//		else
			//		{
			//			_boxLeftTopPoint.Y = _boxEndPoint.Y;
			//		}

			//		Graphics g = pbxExample.CreateGraphics();

			//		g.SmoothingMode = SmoothingMode.HighSpeed;

			//		//只有在绘图且移动情况下才画新的矩形
			//		//_boxesImage = new Bitmap(pbxExample.ClientSize.Width, pbxExample.ClientSize.Height);
			//		//g = Graphics.FromImage(_boxesImage);

			//		g.SmoothingMode = SmoothingMode.HighSpeed;

			//		//g.DrawRectangle(
			//		//	new Pen(Color.Red, Convert.ToInt32(StringResourceManager.BoxBorderWidth)),
			//		//	_boxLeftTopPoint.X,
			//		//	_boxLeftTopPoint.Y,
			//		//	Math.Abs(_boxEndPoint.X - _boxStartPoint.X),
			//		//	Math.Abs(_boxEndPoint.Y - _boxStartPoint.Y));

			//		//;
			//		Rectangle rectangle = new Rectangle(_boxLeftTopPoint.X, _boxLeftTopPoint.Y,
			//			Math.Abs(_boxEndPoint.X - _boxStartPoint.X), Math.Abs(_boxEndPoint.Y - _boxStartPoint.Y));
			//		g.DrawRectangle(new Pen(Color.Red, Convert.ToInt32(StringResourceManager.BoxBorderWidth)), rectangle);
			//		pbxExample.Invalidate(rectangle);
			//		//显示当前鼠标坐标
			//		button1.Text = _boxStartPoint.X + "," + _boxStartPoint.Y + "," + Math.Abs(_boxEndPoint.X - _boxStartPoint.X) + ","
			//					   + Math.Abs(_boxEndPoint.Y - _boxStartPoint.Y);

			//		////获取画矩形之前的图片（以便擦除）
			//		//_previousImage = (Bitmap)pbxExample.Image;
			//		////将矩形真正画到picturebox上
			//		//pbxExample.CreateGraphics().DrawImage(_boxesImage, 0, 0);

			//		//Thread.Sleep(10);

			//		////只有在拖动矩形前提下，才清除
			//		//if (_isDrawingAndMoving)
			//		//{
			//		//	pbxExample.Image = _previousImage;
			//	}
			//}

			//只有在拖动矩形情况下才需要画矩形（并不断清除）
			//if (_isDrawingAndMoving)
			//{
			//	//tmrClearBox.Start();
			//	//判断矩形左上角的点的位置

			//}
		}

		/// <summary>
		///     改变拖动Box时光标形状
		/// </summary>
		private void ChangeBoxCursor(int x, int y)
		{
			MouseEventArgs e;
			//选中的Box的边框宽度
			int bw = Convert.ToInt32(StringResourceManager.SelectedBoxBorderWidth);

			foreach (Box box in _ocrImage.ImageBoxList.SelectedBoxes)
			{
				//左上
				if (
					new Rectangle(box.X, box.Y, bw, bw).Contains(x, y))
				{
					// ↖↘ 箭头
					pbxExample.Cursor = Cursors.SizeNWSE;
					anchorPosition = AnchorPositionType.TopLeft;
				}
				//右下
				else if (new Rectangle(box.X + box.Width - bw, box.Y + box.Height - bw, bw, bw).Contains(x, y))
				{
					// ↖↘ 箭头
					pbxExample.Cursor = Cursors.SizeNWSE;
					anchorPosition = AnchorPositionType.BottomRight;
				}
				//左下
				else if (
					new Rectangle(box.X, box.Y + box.Height - bw, bw, bw).Contains(x, y))
				{
					// ↗↙ 箭头
					pbxExample.Cursor = Cursors.SizeNESW;
					anchorPosition = AnchorPositionType.BottomLeft;
				}
				//右上
				else if (new Rectangle(box.X + box.Width - bw, box.Y, bw, bw).Contains(x, y))
				{
					// ↗↙ 箭头
					pbxExample.Cursor = Cursors.SizeNESW;
					anchorPosition = AnchorPositionType.TopRight;
				}
				//上边
				else if (new Rectangle(box.X + bw, box.Y, box.Width - bw * 2, bw).Contains(x, y))
				{
					//上下箭头
					pbxExample.Cursor = Cursors.SizeNS;
					anchorPosition = AnchorPositionType.TopMiddle;
				}
				//下边
				else if (new Rectangle(box.X + bw, box.Y + box.Height - bw, box.Width - bw * 2, bw).Contains(x, y))
				{
					//上下箭头
					pbxExample.Cursor = Cursors.SizeNS;
					anchorPosition = AnchorPositionType.BottomMiddle;
				}
				//左边
				else if (new Rectangle(box.X, box.Y + bw, bw, box.Height - bw * 2).Contains(x, y))
				{
					//左右箭头
					pbxExample.Cursor = Cursors.SizeWE;
					anchorPosition = AnchorPositionType.MiddleLeft;
				}
				//右边
				else if (new Rectangle(box.X + box.Width - bw, box.Y + bw, bw, box.Y + box.Height - bw * 2).Contains(x, y))
				{
					//左右箭头
					pbxExample.Cursor = Cursors.SizeWE;
					anchorPosition = AnchorPositionType.MiddleRight;
				}
				//Box内部（拖动位置）
				else if (new Rectangle(box.X + bw, box.Y + bw, box.Width - bw * 2, box.Height - bw * 2).Contains(x, y))
				{
					pbxExample.Cursor = Cursors.SizeAll;
					anchorPosition = AnchorPositionType.MiddleMiddle;
				}

					//Box 外部
				else
				{
					pbxExample.Cursor = Cursors.Default;
					anchorPosition = AnchorPositionType.None;
				}
			}
		}


		private void pbxExample_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_currentMouseMoveState = MouseMoveState.DoNothing;

				//Box box = _ocrImage.ImageBoxList.SelectedBoxes[0];
				//_boxEndPoint = new Point(e.X, e.Y);

				//if (_isMovingBox)
				//{
				//	_isMovingBox = false;
				//	box.X += (_boxEndPoint.X - _boxStartPoint.X);
				//	box.Y += (_boxEndPoint.Y - _boxStartPoint.Y);
				//}
				//else if (_isDrawingBox)
				//{
				//	_isDrawingBox = false;
				//}
				//else if (_isSizingBox)
				//{
				//	//FIXME 按下鼠标左键后，移动到其他位置，又不能拖动了
				//	_isSizingBox = false;

				//	if (anchorPosition == AnchorPositionType.TopLeft)
				//	{
				//		box.X = _boxEndPoint.X;
				//		box.Y = _boxEndPoint.Y;
				//		box.Width -= _boxEndPoint.X - _boxStartPoint.X;
				//		box.Height -= _boxEndPoint.Y - _boxStartPoint.Y;
				//	}
				//	else if (anchorPosition == AnchorPositionType.TopMiddle)
				//	{
				//		box.Y = _boxEndPoint.Y;
				//		box.Height -= _boxEndPoint.Y - _boxStartPoint.Y;
				//	}
				//	else if (anchorPosition == AnchorPositionType.TopRight)
				//	{
				//		box.Y = _boxEndPoint.Y;
				//		box.Width += _boxEndPoint.X - _boxStartPoint.X;
				//		box.Height -= _boxEndPoint.Y - _boxStartPoint.Y;
				//	}
				//	else if (anchorPosition == AnchorPositionType.MiddleLeft)
				//	{
				//		box.X = _boxEndPoint.X;
				//		box.Width -= _boxEndPoint.X - _boxStartPoint.X;
				//	}
				//	else if (anchorPosition == AnchorPositionType.MiddleRight)
				//	{
				//		box.Width += _boxEndPoint.X - _boxStartPoint.X;
				//	}
				//	else if (anchorPosition == AnchorPositionType.BottomLeft)
				//	{
				//		box.X = _boxEndPoint.X;
				//		box.Width += _boxStartPoint.X - _boxEndPoint.X;
				//		box.Height += _boxEndPoint.Y - _boxStartPoint.Y;
				//	}
				//	else if (anchorPosition == AnchorPositionType.BottomMiddle)
				//	{
				//		box.Y = _boxEndPoint.Y;
				//		box.Height += _boxEndPoint.Y - _boxStartPoint.Y;
				//	}
				//	else if (anchorPosition == AnchorPositionType.BottomRight)
				//	{
				//		box.Width += _boxEndPoint.X - _boxStartPoint.X;
				//		box.Height += _boxEndPoint.Y - _boxStartPoint.Y;
				//	}
				//}

				////结束移动 Box 后显示新的 Box 位置
				//RefreshBoxesInPictureBox();
				//BindBoxesInfoInGridView();
			}


			//TOFIX
			//if (e.Button == MouseButtons.Left)
			//{
			//	////假如放松的是鼠标左键，停止清除矩形
			//	//if (e.Button == MouseButtons.Left)
			//	//{
			//	//	//tmrClearBox.Stop();
			//	//}

			//	//if (_boxesImage != null)
			//	//{
			//	//	pbxExample.CreateGraphics().DrawImage(_boxesImage, 0, 0);
			//	//}


			//	//if (_isDrawingBox)
			//	//{
			//	//只有在结束点相对于起始点有变化的情况下才添加Box（避免单纯单击）
			//	if (_boxEndPoint.X != _boxStartPoint.X || _boxEndPoint.Y != _boxStartPoint.Y)
			//	{
			//		//根据当前 创建一个Box对象
			//		Box box = new Box("※", _boxLeftTopPoint.X, _boxLeftTopPoint.Y,
			//			Math.Abs(_boxEndPoint.X - _boxStartPoint.X),
			//			Math.Abs(_boxEndPoint.Y - _boxStartPoint.Y));

			//		_ocrImage.ImageBoxList.Add(box);
			//		RefreshBoxesInfoInGridView();
			//		//使数据网格滚动到刚添加的那行
			//		dgvBoxes.CurrentCell = dgvBoxes.Rows[dgvBoxes.Rows.Count - 2].Cells[0];
			//		dgvBoxes.Rows[dgvBoxes.Rows.Count - 2].Selected = true;
			//		//退出画 Box 状态
			//		_isDrawingBox = false;
			//	}
			//	//boxList.Display((Bitmap)pbxExample.Image);
			//	//将box数据显示在数据网格中
			//	//}
			//}
		}

		public void BindBoxesInfoInGridView()
		{
			DataTable boxesTable = new DataTable();

			//添加所需的列
			boxesTable.Columns.Add("sn", Type.GetType("System.Int32"));
			boxesTable.Columns.Add("character", Type.GetType("System.String"));
			boxesTable.Columns.Add("x", Type.GetType("System.Int32"));
			boxesTable.Columns.Add("y", Type.GetType("System.Int32"));
			boxesTable.Columns.Add("width", Type.GetType("System.Int32"));
			boxesTable.Columns.Add("height", Type.GetType("System.Int32"));

			int boxCount = _ocrImage.ImageBoxList.Boxes.Count;

			//将数据添加到DataTable对象中
			for (int i = 0; i < boxCount; i++)
			{
				DataRow dataRow = boxesTable.NewRow();

				Box currentBox = _ocrImage.ImageBoxList.Boxes[i];

				dataRow["sn"] = currentBox.Sn;
				dataRow["character"] = currentBox.Character;
				dataRow["x"] = currentBox.X;
				dataRow["y"] = currentBox.Y;
				dataRow["width"] = currentBox.Width;
				dataRow["height"] = currentBox.Height;

				boxesTable.Rows.Add(dataRow);
			}

			dgvBoxes.DataSource = boxesTable;

			dgvBoxes.Columns["sn"].HeaderText = "编号";
			dgvBoxes.Columns["character"].HeaderText = "字符";
			dgvBoxes.Columns["x"].HeaderText = "X";
			dgvBoxes.Columns["y"].HeaderText = "Y";
			dgvBoxes.Columns["width"].HeaderText = "宽度";
			dgvBoxes.Columns["height"].HeaderText = "高度";

			//设置数据网格的自动宽度与高度
			dgvBoxes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvBoxes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dgvBoxes.AutoResizeColumns();
			dgvBoxes.AutoResizeRows();
		}


		private void tmrClearBox_Tick(object sender, EventArgs e)
		{
			////判断矩形左上角的点的位置
			//_boxLeftTopPoint = new Point();

			//if (_boxEndPoint.X > _boxStartPoint.X)
			//{
			//	_boxLeftTopPoint.X = _boxStartPoint.X;
			//}
			//else
			//{
			//	_boxLeftTopPoint.X = _boxEndPoint.X;
			//}

			//if (_boxEndPoint.Y > _boxStartPoint.Y)
			//{
			//	_boxLeftTopPoint.Y = _boxStartPoint.Y;
			//}
			//else
			//{
			//	_boxLeftTopPoint.Y = _boxEndPoint.Y;
			//}

			//Graphics g = pbxExample.CreateGraphics();

			//g.SmoothingMode = SmoothingMode.HighSpeed;

			////只有在绘图且移动情况下才画新的矩形
			//if (_isDrawingAndMoving)
			//{
			//	_boxesImage = new Bitmap(pbxExample.ClientSize.Width, pbxExample.ClientSize.Height);
			//	g = Graphics.FromImage(_boxesImage);
			//	g.SmoothingMode = SmoothingMode.HighSpeed;
			//	g.DrawRectangle(
			//		new Pen(Color.Red, Convert.ToInt32(StringResourceManager.BoxBorderWidth)),
			//		_boxLeftTopPoint.X,
			//		_boxLeftTopPoint.Y,
			//		Math.Abs(_boxEndPoint.X - _boxStartPoint.X),
			//		Math.Abs(_boxEndPoint.Y - _boxStartPoint.Y));

			//	;

			//	//显示当前鼠标坐标
			//	button1.Text = _boxStartPoint.X + "," + _boxStartPoint.Y + "," + Math.Abs(_boxEndPoint.X - _boxStartPoint.X) + ","
			//				   + Math.Abs(_boxEndPoint.Y - _boxStartPoint.Y);

			//	//获取画矩形之前的图片（以便擦除）
			//	_previousImage = (Bitmap) pbxExample.Image;
			//	//将矩形真正画到picturebox上
			//	pbxExample.CreateGraphics().DrawImage(_boxesImage, 0, 0);

			//	Thread.Sleep(10);

			//	//只有在拖动矩形前提下，才清除
			//	if (_isDrawingAndMoving)
			//	{
			//		pbxExample.Image = _previousImage;
			//	}
			//}
		}


		private void pbxExample_MouseClick(object sender, MouseEventArgs e)
		{
			//按住 Ctrl 并点击鼠标左键时
			if (ModifierKeys == Keys.Control && e.Button == MouseButtons.Left)
			{
				foreach (Box box in _ocrImage.ImageBoxList.Boxes)
				{
					if (box.Contains(e.X, e.Y))
					{
						box.Selected = !box.Selected;
					}
				}
				//GetNewBoxesImage();
				//pbxExample.Image = _ocrImage.DrawBoxesOnImage(pbxExample.Image as Bitmap);
				ChangeBoxSelectionInImageBox();
			}
		}

		private void btnInsert_Click(object sender, EventArgs e)
		{
			const string initialCharacter = "※";
			const int initialX = 20;
			const int initialY = 20;
			const int initialWidth = 50;
			const int initialHeight = 50;

			Box box = _ocrImage.ImageBoxList.GetBoxByCoordinate(initialX, initialY, initialWidth, initialHeight);

			//只有在不存在初始位置的新Box情况下才添加进去
			if (box == null)
			{
				_ocrImage.ImageBoxList.Add(new Box(_ocrImage.ImageBoxList.Boxes.Count + 1, initialCharacter, initialX, initialY,
					initialWidth, initialHeight));
				//选中刚添加的Box
				_ocrImage.ImageBoxList.GetBoxByCoordinate(initialX, initialY, initialWidth, initialHeight).Selected = true;
				GetNewBoxesImage();
				BindBoxesInfoInGridView();
			}
		}

		private Bitmap GetNewBoxesImage()
		{
			Bitmap bitmap = new Bitmap(_ocrImage.TempImageInfo.FilePath);
			//将临时文件读取到内存中
			_ocrImage.GetBoxesImage(bitmap);
			//将内存中的Bitmap（Box图层）绘制到PictureBox控件中
			return bitmap;
		}

		private void toolStripBtnSaveBox_Click(object sender, EventArgs e)
		{
			_ocrImage.SaveToBoxFile();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			GetNewBoxesImage();
		}

		private void pbxExample_Paint(object sender, PaintEventArgs e)
		{
			//_ocrImage.DisplayBoxes((Bitmap)pbxExample.Image);
		}

		private void dgvBoxes_Click(object sender, EventArgs e)
		{
		}

		private void ChangeBoxSelectionInImageBox()
		{
			//遍历数据网格中每一行，根据当前行坐标找对应的Box，然后改变行的选中状态
			foreach (DataGridViewRow row in dgvBoxes.Rows)
			{
				Box box = _ocrImage.ImageBoxList.GetBoxByCoordinate(Convert.ToInt32(row.Cells["x"].Value),
					Convert.ToInt32(row.Cells["y"].Value), Convert.ToInt32(row.Cells["width"].Value),
					Convert.ToInt32(row.Cells["height"].Value));

				if (box != null)
				{
					row.Selected = box.Selected;
				}
			}
		}

		/// <summary>
		///     显示当前Box信息，在上方的各UpDown控件中
		/// </summary>
		private void ChangeBoxSelectionInDataGridView()
		{
			//清除之前选择的所有Box
			_ocrImage.ImageBoxList.UnSelectAll();

			foreach (DataGridViewRow row in dgvBoxes.SelectedRows)
			{
				Box currentBox = _ocrImage.ImageBoxList.GetBoxByCoordinate(Convert.ToInt32(row.Cells["x"].Value),
					Convert.ToInt32(row.Cells["y"].Value), Convert.ToInt32(row.Cells["width"].Value),
					Convert.ToInt32(row.Cells["height"].Value));
				//同时改变当前Box的选中状态
				currentBox.Selected = true;
			}

			RefreshBoxInfoInHeader();
		}

		private void RefreshBoxInfoInHeader()
		{
			if (dgvBoxes.SelectedRows.Count == 1) //若只选中一行，显示信息
			{
				DataGridViewRow row = dgvBoxes.CurrentRow;

				Box currentBox = _ocrImage.ImageBoxList.GetBoxByCoordinate(Convert.ToInt32(row.Cells["x"].Value),
					Convert.ToInt32(row.Cells["y"].Value), Convert.ToInt32(row.Cells["width"].Value),
					Convert.ToInt32(row.Cells["height"].Value));

				if (currentBox != null)
				{
					txtCharacter.Text = currentBox.Character;
					nudX.Value = currentBox.X;
					nudY.Value = currentBox.Y;
					nudWidth.Value = currentBox.Width;
					nudHeight.Value = currentBox.Height;

					txtCharacter.Enabled = true;
					nudX.Enabled = true;
					nudY.Enabled = true;
					nudWidth.Enabled = true;
					nudHeight.Enabled = true;
				}
			}
			else //否则不显示
			{
				txtCharacter.Text = "";
				//nudX.Text = "";
				//nudY.Text = "";
				//nudWidth.Text = "";
				//nudHeight.Text = "";

				txtCharacter.Enabled = false;
				nudX.Enabled = false;
				nudY.Enabled = false;
				nudWidth.Enabled = false;
				nudHeight.Enabled = false;
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			//删除选中的Box
			foreach (DataGridViewRow row in dgvBoxes.SelectedRows)
			{
				Box box = _ocrImage.ImageBoxList.GetBoxByCoordinate(Convert.ToInt32(row.Cells["x"].Value),
					Convert.ToInt32(row.Cells["y"].Value), Convert.ToInt32(row.Cells["width"].Value),
					Convert.ToInt32(row.Cells["height"].Value));

				_ocrImage.ImageBoxList.Remove(box);
			}
			//刷新图片上的所有Box显示
			GetNewBoxesImage();
			//刷新选中的Box信息
			BindBoxesInfoInGridView();
			//删除后清除所有的选择
			dgvBoxes.ClearSelection();
		}

		/// <summary>
		///     根据数据网格中的选中项删除对应的Box
		/// </summary>
		private void dgvBoxes_SelectionChanged(object sender, EventArgs e)
		{
			ChangeBoxSelectionInDataGridView();
			GetNewBoxesImage();
		}

		/// <summary>
		///     当Box的五个参数中的任何一个变化的时候，刷新图片框及网格中的显示
		/// </summary>
		private void ChangeSingleBoxData()
		{
			//TOFIX
			if (dgvBoxes.SelectedRows.Count != 0)
			{
				DataGridViewRow selectedRow = dgvBoxes.SelectedRows[0];

				Box box = _ocrImage.ImageBoxList.GetBoxByCoordinate(Convert.ToInt32(selectedRow.Cells["x"].Value),
					Convert.ToInt32(selectedRow.Cells["y"].Value), Convert.ToInt32(selectedRow.Cells["width"].Value),
					Convert.ToInt32(selectedRow.Cells["height"].Value));
				box.Character = txtCharacter.Text;
				box.X = Convert.ToInt32(nudX.Value);
				box.Y = Convert.ToInt32(nudY.Value);
				box.Width = Convert.ToInt32(nudWidth.Value);
				box.Height = Convert.ToInt32(nudHeight.Value);

				GetNewBoxesImage();
				//RefreshBoxInfoInHeader();
				//RefreshBoxesInfoInGridView();


				////遍历数据网格中每一行，根据当前行坐标找对应的Box，然后改变行的选中状态
				//foreach (DataGridViewRow row in dgvBoxes.Rows)
				//{
				//	Box box = _ocrImage.ImageBoxList.GetBoxByCoordinate(Convert.ToInt32(row.Cells["x"].Value),
				//		Convert.ToInt32(row.Cells["y"].Value), Convert.ToInt32(row.Cells["width"].Value),
				//		Convert.ToInt32(row.Cells["height"].Value));

				//	if (box != null)
				//	{
				//		row.Selected = box.Selected;
				//	}
				//}
			}
		}

		private void txtCharacter_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				ChangeSingleBoxData();
			}
		}

		private void nudX_ValueChanged(object sender, EventArgs e)
		{
			if (nudX.Focused)
			{
				ChangeSingleBoxData();
			}
		}

		private void nudY_ValueChanged(object sender, EventArgs e)
		{
			if (nudY.Focused)
			{
				ChangeSingleBoxData();
			}
		}

		private void nudWidth_ValueChanged(object sender, EventArgs e)
		{
			if (nudWidth.Focused)
			{
				ChangeSingleBoxData();
			}
		}

		private void nudHeight_ValueChanged(object sender, EventArgs e)
		{
			if (nudHeight.Focused)
			{
				ChangeSingleBoxData();
			}
		}

		private void btnMerge_Click(object sender, EventArgs e)
		{
			//找出最靠左的X坐标
			int minX = _ocrImage.ImageBoxList.SelectedBoxes.Min(box => box.X);
			//找出最考上的Y坐标
			int minY = _ocrImage.ImageBoxList.SelectedBoxes.Min(box => box.Y);
			//找出最靠右的X坐标
			int maxX = _ocrImage.ImageBoxList.SelectedBoxes.Max(box => box.X + box.Width);
			//找出最靠下的Y坐标
			int maxY = _ocrImage.ImageBoxList.SelectedBoxes.Max(box => box.Y + box.Height);

			//改变第一个选中Box的大小
			_ocrImage.ImageBoxList.SelectedBoxes[0].X = minX;
			_ocrImage.ImageBoxList.SelectedBoxes[0].Y = minY;
			_ocrImage.ImageBoxList.SelectedBoxes[0].Width = maxX - minX;
			_ocrImage.ImageBoxList.SelectedBoxes[0].Height = maxY - minY;

			//删除其他的
			for (int i = _ocrImage.ImageBoxList.SelectedBoxes.Count - 1; i >= 1; i--)
			{
				_ocrImage.ImageBoxList.Remove(_ocrImage.ImageBoxList.SelectedBoxes[i]);
			}

			GetNewBoxesImage();
			//RefreshBoxesInfoInGridView();
		}

		private void pbxExample_MouseHover(object sender, EventArgs e)
		{
			//foreach (Box box in _ocrImage.ImageBoxList.Boxes)
			//{
			//	if (box.Contains(MousePosition.X, MousePosition.Y))
			//	{
			//		ToolTip toolTip = new ToolTip();

			//		toolTip.Tag = box.Character;
			//		toolTip.ShowAlways = true;
			//		toolTip.Show(box.Character, pbxExample, MousePosition.X, MousePosition.Y);
			//	}
			//}
		}

		private void btnMakeBox_Click(object sender, EventArgs e)
		{
			ShowProgressBar();


			//pbxExample.Image.Dispose();
			////进行旋转操作
			Dictionary<string, string> args = new Dictionary<string, string>
			{
				{"sourceImagePath", _ocrImage.TempImageInfo.FilePath},
				{"boxFilePath", _ocrImage.BoxFileInfo.Dir + _ocrImage.BoxFileInfo.MainFileName},
			};

			//生成 BOX 文件
			TessProcess makeBoxProcess = new MakeBoxTessProcess(args);
			bgwProcess.RunWorkerAsync(makeBoxProcess);

			//等待处理完毕
			while (bgwProcess.IsBusy)
			{
				Application.DoEvents();
			}

			_ocrImage.LoadFromBoxFile();


			//pbxExample.BackgroundImage = (pbxExample.Image.Clone() as Bitmap);
			//pbxExample.Image = _ocrImage.GetBoxesImage(pbxExample.BackgroundImage as Bitmap);
			pbxExample.Image = _ocrImage.GetBoxesImage(pbxExample.Image as Bitmap);
			pbxExample.Update();

			//GetNewBoxImageDelegate getNewBoxImageDelegate = new GetNewBoxImageDelegate(_ocrImage.GetNewBoxesImage);
			//bgwProcess.RunWorkerAsync(new object[]{getNewBoxImageDelegate,new Bitmap(_ocrImage.TempImageInfo.FilePath)});

			//while (bgwProcess.IsBusy)
			//{
			//	Application.DoEvents();
			//}
		}

		private void ShowProgressBar()
		{
			progressBar = new frmProgressBar();
			progressBar.Show();
		}

		private void btnRotateLeft_Click(object sender, EventArgs e)
		{
			#region 用ImageMagick处理旋转

			//pbxExample.Image.Dispose();
			//////进行旋转操作
			//Dictionary<string, string> args = new Dictionary<string, string>
			//{
			//	{"degree", 270.ToString()},
			//	{"sourceImagePath", _ocrImage.TempImageInfo.FilePath},
			//	{"destImagePath", _ocrImage.TempImageInfo.FilePath},
			//};
			////调用指向TextCleaner
			//ImageProcess imageProcess = new RotateImageProcess(args);
			//bgwProcess.RunWorkerAsync(imageProcess);

			#endregion

			//进行逆时针旋转90°操作
			RotateImage(RotateFlipType.Rotate270FlipNone);
		}

		private void RotateImage(RotateFlipType rotateFilpType)
		{
			pbxExample.BackgroundImage.RotateFlip(rotateFilpType);
			pbxExample.Image.RotateFlip(rotateFilpType);
			pbxExample.Refresh();

			try
			{
				pbxExample.BackgroundImage.Save(_ocrImage.OriginalImageInfo.FilePath);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private void btnConvertToTiff_Click(object sender, EventArgs e)
		{
			Dictionary<string, string> args = new Dictionary<string, string>
			{
				{"sourceImagePath", _ocrImage.OriginalImageInfo.FilePath},
				{"destImagePath", _ocrImage.TempImageInfo.Dir + _ocrImage.OriginalImageInfo.MainFileName + ".tiff"},
			};
			//调用指向TextCleaner
			ImageProcess imageProcess = new ToTiffImageProcess(args);
			bgwProcess.RunWorkerAsync(imageProcess);
		}

		private void btnRotateRight_Click(object sender, EventArgs e)
		{
			RotateImage(RotateFlipType.Rotate90FlipNone);
		}

		private void dgvBoxes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
		}

		private void button5_Click(object sender, EventArgs e)
		{
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			//回复图片为初始状态
			ResetImage();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
		}

		private void toolStripBtnOpenImage_Click(object sender, EventArgs e)
		{
			//当要打开图片时，设置OpenFileDialog的filter属性
			ofdFile.Filter = StringResourceManager.OpenImageFilterText;

			//打开样本图片
			if (ofdFile.ShowDialog() == DialogResult.OK)
			{
				_ocrImage = new OcrImage(ofdFile.FileName);
				OpenImage(_ocrImage);
				dgvBoxes.Focus();
				//RefreshBoxesInPictureBox();
				//RefreshBoxesInfoInGridView();
			}
		}

		private enum AnchorPositionType
		{
			None,
			TopLeft,
			TopMiddle,
			TopRight,
			MiddleLeft,
			MiddleMiddle,
			MiddleRight,
			BottomLeft,
			BottomMiddle,
			BottomRight
		}
	}
}