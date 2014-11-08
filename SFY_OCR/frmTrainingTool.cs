#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		//Box 左上角的那个点
		private Point _boxLeftTopPoint;
		//矩形图层
		private Bitmap _boxesImage;
		//当前鼠标移动状态（for 功能），初始值为 DoNothing
		private MouseMoveState _currentMouseMoveState = MouseMoveState.DoNothing;
		private Point _currentPoint;
		private Cursor _cursorWhenStartResizing;
		private Pen _dashPen;
		private bool _isDrawingBox;
		//当前鼠标是否在移动
		//是否在移动 Box
		private bool _isMovingBox;
		//是否在改变 Box 大小
		private bool _isSizingBox;
		private OcrImage _ocrImage;
		//之前的图层（用于擦除矩形）
		private Bitmap _previousImage;
		//按下鼠标左键时鼠标的位置
		private Point _startPoint;
		//画Box，起始坐标点（左上角）
		//当前鼠标所在的锚点位置（拖动 Box 大小时）。初始值为 None
		private AnchorPositionType anchorPosition = AnchorPositionType.None;
		private Bitmap bitmap;
		private frmProgressBar progressBar;
		//画 Box 与移动 Box 用到
		//当前激活的 Box （用于移动）
		//private Box _activingBox;

		public frmTrainingTool()
		{
			InitializeComponent();

			_dashPen = new Pen(new SolidBrush(Color.Red));
			_dashPen.Width = 1;
			_dashPen.DashStyle = DashStyle.Dash;
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
				background = Image.FromFile(_ocrImage.TempImageInfo.FilePath) as Bitmap;
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
				try
				{
					_ocrImage.LoadFromBoxFile();
					RefreshBoxImageInPictureBox();


					dgvBoxes.RowCount = _ocrImage.ImageBoxList.Boxes.Count + 1;
					//清楚网格中所有选择
					dgvBoxes.ClearSelection();
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
		}

		private void RefreshBoxImageInPictureBox()
		{
			pbxExample.Image = _ocrImage.GetBoxesImage((pbxExample.Image as Bitmap).Size);
			pbxExample.Update();
		}

		private void ShowBoxInfoInDataGridView()
		{
			//dgvBoxes.DataSource = _ocrImage.ImageBoxList.GetBoxesDataTable();
			//dgvBoxes.Rows.Clear();

			//foreach (Box box in _ocrImage.ImageBoxList.Boxes)
			//{
			//	AddBoxToDataGridView(box);
			//}

			//dgvBoxes.Columns["sn"].HeaderText = "编号";
			//dgvBoxes.Columns["character"].HeaderText = "字符";
			//dgvBoxes.Columns["x"].HeaderText = "X";
			//dgvBoxes.Columns["y"].HeaderText = "Y";
			//dgvBoxes.Columns["width"].HeaderText = "宽度";
			//dgvBoxes.Columns["height"].HeaderText = "高度";
		}

		/// <summary>
		///     对数据网格进行初始化
		/// </summary>
		private void InitializeDataGridView()
		{
			dgvBoxes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvBoxes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			dgvBoxes.AutoResizeColumns();
			dgvBoxes.AutoResizeRows();
			dgvBoxes.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			dgvBoxes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

			dgvBoxes.Columns.Add("sn", "编号");
			dgvBoxes.Columns.Add("character", "字符");
			dgvBoxes.Columns.Add("x", "X");
			dgvBoxes.Columns.Add("y", "Y");
			dgvBoxes.Columns.Add("width", "宽度");
			dgvBoxes.Columns.Add("height", "高度");

			dgvBoxes.Columns["sn"].Visible = true;

			dgvBoxes.VirtualMode = true;
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
			pnlPictureBox.AutoScroll = true;
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
			//dgvBoxes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			//dgvBoxes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
			//dgvBoxes.AutoResizeColumns();
			//dgvBoxes.AutoResizeRows();
			//dgvBoxes.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			//dgvBoxes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			//设置 NumericUpDown 的最大值（最小值默认都为 0 ）

			InitializeDataGridView();
			nudX.Maximum = int.MaxValue;
			nudY.Maximum = int.MaxValue;
			nudWidth.Maximum = int.MaxValue;
			nudHeight.Maximum = int.MaxValue;

			//设置图片框所在的Panel的AutoScroll属性
			pnlPictureBox.AutoScroll = true;
			pnlPictureBox.BorderStyle = BorderStyle.FixedSingle;
			;
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
			RefreshBoxImageInPictureBox();
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
			_currentPoint = new Point(e.X, e.Y);

			if (ModifierKeys == Keys.None && e.Button == MouseButtons.Left)
			{
				//记录下按下鼠标左键时的位置
				_startPoint = new Point(e.X, e.Y);

				//若当前光标是默认状态
				if (anchorPosition == AnchorPositionType.None)
				{
					//切换到画 Box 模式
					_currentMouseMoveState = MouseMoveState.DrawingBox;
				}
				else if (anchorPosition == AnchorPositionType.MiddleMiddle)
				{
					//切换到移动 Box 模式
					_currentMouseMoveState = MouseMoveState.MovingBox;
				}
				else
				{
					//切换到改变 Box 大小状态
					_currentMouseMoveState = MouseMoveState.ResizingBox;
					//记住切换到改变大小状态时的光标（决定改变大小的方式）
					_cursorWhenStartResizing = pbxExample.Cursor;
				}
			}
			else if (ModifierKeys == Keys.Control && e.Button == MouseButtons.Left)
			{
			}
		}

		/// <summary>
		///     在图片上绘制用于识别的矩形
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbxExample_MouseMove(object sender, MouseEventArgs e)
		{
			_currentPoint = new Point(e.X, e.Y);

			Bitmap buffer = _ocrImage.GetBoxesImage((pbxExample.Image as Bitmap).Size);
			Graphics g = Graphics.FromImage(buffer);
			g.SmoothingMode = SmoothingMode.HighSpeed;


			ChangeCursor(_currentPoint);

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
			//ChangeBoxCursor(_currentPoint);

			//若当前处于画 Box 状态
			if (_currentMouseMoveState == MouseMoveState.DrawingBox)
			{
				//判断不同的拖动方向，并计算画 box 的起始点
				if (_currentPoint.X > _startPoint.X)
				{
					_boxLeftTopPoint.X = _startPoint.X;
				}
				else
				{
					_boxLeftTopPoint.X = _currentPoint.X;
				}

				if (_currentPoint.Y > _startPoint.Y)
				{
					_boxLeftTopPoint.Y = _startPoint.Y;
				}
				else
				{
					_boxLeftTopPoint.Y = _currentPoint.Y;
				}

				g.DrawRectangle(
					_dashPen,
					_boxLeftTopPoint.X,
					_boxLeftTopPoint.Y,
					Math.Abs(_currentPoint.X - _startPoint.X),
					Math.Abs(_currentPoint.Y - _startPoint.Y));
			}
			else if (_currentMouseMoveState == MouseMoveState.MovingBox) //移动 Box 状态
			{
				//只允许选中一个Box进行移动
				if (_ocrImage.ImageBoxList.SelectedBoxes.Count != 0) //补丁代码
				{
					Box box = _ocrImage.ImageBoxList.SelectedBoxes[0];
					int newBoxLeftTopPointX = box.X + (_currentPoint.X - _startPoint.X);
					int newBoxLeftTopPointY = box.Y + (_currentPoint.Y - _startPoint.Y);

					g.DrawRectangle(
						_dashPen,
						newBoxLeftTopPointX, newBoxLeftTopPointY, box.Width, box.Height);
				}
			}
			else if (_currentMouseMoveState == MouseMoveState.ResizingBox)
			{
				if (_ocrImage.ImageBoxList.SelectedBoxes.Count != 0) //补丁代码
				{
					Box box = _ocrImage.ImageBoxList.SelectedBoxes[0];
					int newBoxLeftTopPointX = box.X;
					int newBoxLeftTopPointY = box.Y;
					int newBoxWidth = box.Width;
					int newBoxHeight = box.Height;
					if (anchorPosition == AnchorPositionType.TopLeft)
					{
						newBoxLeftTopPointX = _currentPoint.X;
						newBoxLeftTopPointY = _currentPoint.Y;
						newBoxWidth -= _currentPoint.X - _startPoint.X;
						newBoxHeight -= _currentPoint.Y - _startPoint.Y;
					}
					else if (anchorPosition == AnchorPositionType.TopMiddle)
					{
						newBoxLeftTopPointY = _currentPoint.Y;
						newBoxHeight -= _currentPoint.Y - _startPoint.Y;
					}
					else if (anchorPosition == AnchorPositionType.TopRight)
					{
						newBoxLeftTopPointY = _currentPoint.Y;
						newBoxWidth += _currentPoint.X - _startPoint.X;
						newBoxHeight -= _currentPoint.Y - _startPoint.Y;
					}
					else if (anchorPosition == AnchorPositionType.MiddleLeft)
					{
						newBoxLeftTopPointX = _currentPoint.X;
						newBoxWidth -= _currentPoint.X - _startPoint.X;
					}
					else if (anchorPosition == AnchorPositionType.MiddleRight)
					{
						newBoxWidth += _currentPoint.X - _startPoint.X;
					}
					else if (anchorPosition == AnchorPositionType.BottomLeft)
					{
						newBoxLeftTopPointX = _currentPoint.X;
						newBoxWidth += _startPoint.X - _currentPoint.X;
						newBoxHeight += _currentPoint.Y - _startPoint.Y;
					}
					else if (anchorPosition == AnchorPositionType.BottomMiddle)
					{
						newBoxHeight += _currentPoint.Y - _startPoint.Y;
					}
					else if (anchorPosition == AnchorPositionType.BottomRight)
					{
						newBoxWidth += _currentPoint.X - _startPoint.X;
						newBoxHeight += _currentPoint.Y - _startPoint.Y;
					}

					g.DrawRectangle(
						_dashPen,
						newBoxLeftTopPointX, newBoxLeftTopPointY, newBoxWidth, newBoxHeight);
				}
			}


			pbxExample.Image = buffer;
			pbxExample.Update();
		}

		/// <summary>
		///     改变拖动Box时光标形状
		/// </summary>
		private void ChangeCursor(Point currentPoint)
		{
			//选中的Box的边框宽度
			int bw = Convert.ToInt32(StringResourceManager.SelectedBoxBorderWidth) * 3; //给了3倍的宽容度

			//若还没有打开图片，或已打开，但没选中任何 Box
			if (_ocrImage == null || _ocrImage.ImageBoxList.SelectedBoxes.Count != 1)
			{
				pbxExample.Cursor = Cursors.Default;
				anchorPosition = AnchorPositionType.None;
			}
			else //只选中了一个 Box 
			{
				//在不处于 Resizing 模式下下才变化光标样式
				if (_currentMouseMoveState != MouseMoveState.ResizingBox) //补丁代码
				{
					Box box = _ocrImage.ImageBoxList.SelectedBoxes[0];
					if (
						new Rectangle(box.X, box.Y, bw, bw).Contains(currentPoint))
					{
						// ↖↘ 箭头
						pbxExample.Cursor = Cursors.SizeNWSE;
						anchorPosition = AnchorPositionType.TopLeft;
					}
					//右下
					else if (new Rectangle(box.X + box.Width - bw, box.Y + box.Height - bw, bw, bw).Contains(currentPoint))
					{
						// ↖↘ 箭头
						pbxExample.Cursor = Cursors.SizeNWSE;
						anchorPosition = AnchorPositionType.BottomRight;
					}
					//左下
					else if (
						new Rectangle(box.X, box.Y + box.Height - bw, bw, bw).Contains(currentPoint))
					{
						// ↗↙ 箭头
						pbxExample.Cursor = Cursors.SizeNESW;
						anchorPosition = AnchorPositionType.BottomLeft;
					}
					//右上
					else if (new Rectangle(box.X + box.Width - bw, box.Y, bw, bw).Contains(currentPoint))
					{
						// ↗↙ 箭头
						pbxExample.Cursor = Cursors.SizeNESW;
						anchorPosition = AnchorPositionType.TopRight;
					}
					//上边
					else if (new Rectangle(box.X + bw, box.Y, box.Width - bw * 2, bw).Contains(currentPoint))
					{
						//上下箭头
						pbxExample.Cursor = Cursors.SizeNS;
						anchorPosition = AnchorPositionType.TopMiddle;
					}
					//下边
					else if (new Rectangle(box.X + bw, box.Y + box.Height - bw, box.Width - bw * 2, bw).Contains(currentPoint))
					{
						//上下箭头
						pbxExample.Cursor = Cursors.SizeNS;
						anchorPosition = AnchorPositionType.BottomMiddle;
					}
					//左边
					else if (new Rectangle(box.X, box.Y + bw, bw, box.Height - bw * 2).Contains(currentPoint))
					{
						//左右箭头
						pbxExample.Cursor = Cursors.SizeWE;
						anchorPosition = AnchorPositionType.MiddleLeft;
					}
					//右边
					else if (new Rectangle(box.X + box.Width - bw, box.Y + bw, bw, box.Y + box.Height - bw * 2).Contains(currentPoint))
					{
						//左右箭头
						pbxExample.Cursor = Cursors.SizeWE;
						anchorPosition = AnchorPositionType.MiddleRight;
					}
					//Box内部（拖动位置）
					else if (new Rectangle(box.X + bw, box.Y + bw, box.Width - bw * 2, box.Height - bw * 2).Contains(currentPoint))
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
		}


		private void pbxExample_MouseUp(object sender, MouseEventArgs e)
		{
			_currentPoint = new Point(e.X, e.Y);

			if (ModifierKeys == Keys.None && e.Button == MouseButtons.Left)
			{
				//测试用 btnConvertToTiff.Text = _ocrImage.ImageBoxList.SelectedBoxes.Count.ToString();
				//如果松开鼠标左键之前是处于画矩形状态
				if (_currentMouseMoveState == MouseMoveState.DrawingBox)
				{
					if (_currentPoint.X != _startPoint.X && _currentPoint.Y != _startPoint.Y)
					{
						int newSn = _ocrImage.ImageBoxList.Boxes.Max(b => b.Sn) + 1;
						//根据当前 创建一个Box对象
						Box box = new Box(newSn, "※", _boxLeftTopPoint.X, _boxLeftTopPoint.Y,
							Math.Abs(_currentPoint.X - _startPoint.X),
							Math.Abs(_currentPoint.Y - _startPoint.Y));
						box.Selected = true;

						//取消所有 Box 的选中状态（为了选中新添加的 Box）
						_ocrImage.ImageBoxList.UnSelectAll();
						//将新的 Box 添加到 BoxList 中去
						_ocrImage.ImageBoxList.Add(box);
						//将新的 Box 添加到数据网格中去
						AddBoxToDataGridView(box);
						//跳转到刚添加的那行
						JumpToBoxRecordInDataGridView(box);
					}
				}
				//若之前处于移动 Box 状态
				else if (_currentMouseMoveState == MouseMoveState.MovingBox)
				{
					if (_ocrImage.ImageBoxList.SelectedBoxes.Count != 0) //补丁语句
					{
						Box box = _ocrImage.ImageBoxList.SelectedBoxes[0];
						box.X += (_currentPoint.X - _startPoint.X);
						box.Y += (_currentPoint.Y - _startPoint.Y);
					}
				}
				else if (_currentMouseMoveState == MouseMoveState.ResizingBox)
				{
					if (_ocrImage.ImageBoxList.SelectedBoxes.Count != 0) //补丁代码
					{
						Box box = _ocrImage.ImageBoxList.SelectedBoxes[0];

						if (anchorPosition == AnchorPositionType.TopLeft)
						{
							box.X = _currentPoint.X;
							box.Y = _currentPoint.Y;
							box.Width -= _currentPoint.X - _startPoint.X;
							box.Height -= _currentPoint.Y - _startPoint.Y;
						}
						else if (anchorPosition == AnchorPositionType.TopMiddle)
						{
							box.Y = _currentPoint.Y;
							box.Height -= _currentPoint.Y - _startPoint.Y;
						}
						else if (anchorPosition == AnchorPositionType.TopRight)
						{
							box.Y = _currentPoint.Y;
							box.Width += _currentPoint.X - _startPoint.X;
							box.Height -= _currentPoint.Y - _startPoint.Y;
						}
						else if (anchorPosition == AnchorPositionType.MiddleLeft)
						{
							box.X = _currentPoint.X;
							box.Width -= _currentPoint.X - _startPoint.X;
						}
						else if (anchorPosition == AnchorPositionType.MiddleRight)
						{
							box.Width += _currentPoint.X - _startPoint.X;
						}
						else if (anchorPosition == AnchorPositionType.BottomLeft)
						{
							box.X = _currentPoint.X;
							box.Width += _startPoint.X - _currentPoint.X;
							box.Height += _currentPoint.Y - _startPoint.Y;
						}
						else if (anchorPosition == AnchorPositionType.BottomMiddle)
						{
							box.Height += _currentPoint.Y - _startPoint.Y;
						}
						else if (anchorPosition == AnchorPositionType.BottomRight)
						{
							box.Width += _currentPoint.X - _startPoint.X;
							box.Height += _currentPoint.Y - _startPoint.Y;
						}
					}
				}

				RefreshBoxImageInPictureBox();
				_currentMouseMoveState = MouseMoveState.DoNothing;
				ChangeCursor(_currentPoint);
				//根据在图片框中的选中状态，刷新数据网格中的选中状态
				//RefreshBoxSelectionInDataGridView();
				//刷新图片框中 Box 的选中状态
				//RefreshBoxImageInPictureBox();
			}
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
			_currentPoint = new Point(e.X, e.Y);
			//只是点击鼠标左键时，若同时在多个 Box 以内，选择其中的一个
			//若点击了之前已经被选中的，变成不选
			//若没有在任何一个 Box 内，全部不选
			//TODO 特殊情况，选中三个，然后再在其中一个点，应该选中那个
			//只点击鼠标左键，单选

			if (ModifierKeys == Keys.None && e.Button == MouseButtons.Left)
			{
				Box clickedBox = null;
				foreach (Box box in _ocrImage.ImageBoxList.Boxes)
				{
					if (box.Contains(_currentPoint))
					{
						//找到第一个 Box 就结束
						clickedBox = box;
						break;
					}
				}
				//如果点中了某一个Box的话
				if (clickedBox != null)
				{
					//排除正在移动 Box 的状态和正在改变 Box 大小的状态
					if (_currentMouseMoveState != MouseMoveState.MovingBox && _currentMouseMoveState != MouseMoveState.ResizingBox)//补丁代码
					{
						//如果当时只选中一个
						if (_ocrImage.ImageBoxList.SelectedBoxes.Count == 1)
						{
							//反选此 Box
							clickedBox.Selected = !clickedBox.Selected;
						}
						else //如果当时已同时选中了多个
						{
							//变成只选中点的那个
							clickedBox.Selected = true;
						}

						//排除移动 Box 的情况和正在改变 Box 大小的状态，因为移动完毕后会点在别的地方

						//将所有非点中的Box设为不选中
						foreach (Box box in _ocrImage.ImageBoxList.Boxes)
						{
							if (box != clickedBox)
							{
								box.Selected = false;
							}
						}
					}
				}
				else //如果没点中任何一个，取消选择所有的
				{
					if (_currentMouseMoveState != MouseMoveState.MovingBox && _currentMouseMoveState != MouseMoveState.ResizingBox)
					{
						_ocrImage.ImageBoxList.UnSelectAll();
					}
				}
				if (_ocrImage.ImageBoxList.SelectedBoxes.Count != 0)
				{
					//JumpToInDataGridViewByBox(_ocrImage.ImageBoxList.SelectedBoxes.OrderByDescending(b => b.Sn).First());
				}
				RefreshBoxImageInPictureBox();
				RefreshBoxSelectionInDataGridView();

				btnConvertToTiff.Text = _ocrImage.ImageBoxList.SelectedBoxes.Count.ToString();
			}
			//按住 Ctrl + 鼠标左键，可多选
			else if (ModifierKeys == Keys.Control && e.Button == MouseButtons.Left)
			{
				Box clickedBox = null;
				foreach (Box box in _ocrImage.ImageBoxList.Boxes)
				{
					if (box.Contains(_currentPoint))
					{
						//找到第一个 Box 就结束
						clickedBox = box;
						break;
					}
				}
				if (clickedBox != null)
				{
					clickedBox.Selected = !clickedBox.Selected;
				}
				else
				{
					_ocrImage.ImageBoxList.UnSelectAll();
				}

				if (_ocrImage.ImageBoxList.SelectedBoxes.Count != 0)
				{
					//JumpToInDataGridViewByBox(_ocrImage.ImageBoxList.SelectedBoxes.OrderByDescending(b => b.Sn).First());
				}
				RefreshBoxImageInPictureBox();
				RefreshBoxSelectionInDataGridView();
			}
			//如果当前有选中 Box ，数据网格跳到那个Sn最大的Box对应的行
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
			}
		}


		//public void RefreshBoxImageInGridView()
		//{
		//	pbxExample.Image = _ocrImage.GetBoxesImage();
		//}


		private Bitmap GetNewBoxesImage()
		{
			Bitmap bitmap = new Bitmap(_ocrImage.TempImageInfo.FilePath);
			//将临时文件读取到内存中
			//_ocrImage.GetBoxesImage(bitmap);
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


		//private void ChangeBoxSelectionInImageBox()
		//{
		//	//遍历数据网格中每一行，根据当前行坐标找对应的Box，然后改变行的选中状态
		//	foreach (DataGridViewRow row in dgvBoxes.Rows)
		//	{
		//		Box box = _ocrImage.ImageBoxList.GetBoxByCoordinate(Convert.ToInt32(row.Cells["x"].Value),
		//			Convert.ToInt32(row.Cells["y"].Value), Convert.ToInt32(row.Cells["width"].Value),
		//			Convert.ToInt32(row.Cells["height"].Value));

		//		if (box != null)
		//		{
		//			row.Selected = box.Selected;
		//		}
		//	}
		//}

		/// <summary>
		///     在数据网格中跳转到指定 Box 对应的记录
		/// </summary>
		/// <param name="box"></param>
		private void JumpToBoxRecordInDataGridView(Box box)
		{
			int index = _ocrImage.ImageBoxList.Boxes.IndexOf(box);
			dgvBoxes.CurrentCell = dgvBoxes.Rows[index].Cells[1];
		}

		/// <summary>
		///     显示当前Box信息，在上方的各UpDown控件中
		/// </summary>
		//private void ChangeBoxSelectionInDataGridView()
		//{
		//	//清除之前选择的所有Box
		//	_ocrImage.ImageBoxList.UnSelectAll();

		//	foreach (DataGridViewRow row in dgvBoxes.SelectedRows)
		//	{
		//		Box currentBox = _ocrImage.ImageBoxList.GetBoxByCoordinate(Convert.ToInt32(row.Cells["x"].Value),
		//			Convert.ToInt32(row.Cells["y"].Value), Convert.ToInt32(row.Cells["width"].Value),
		//			Convert.ToInt32(row.Cells["height"].Value));
		//		//同时改变当前Box的选中状态
		//		currentBox.Selected = true;
		//	}

		//	//RefreshBoxInfoInHeader();
		//}
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
			//删除所有选中的Box
			foreach (Box box in _ocrImage.ImageBoxList.SelectedBoxes)
			{
				DeleteBoxInDataGridView(box);
				_ocrImage.ImageBoxList.Remove(box);
			}
			dgvBoxes.ClearSelection();
			RefreshBoxImageInPictureBox();
		}


		//在改变数据网格中的选中状态时，改变 Box 选中状态
		private void dgvBoxes_SelectionChanged(object sender, EventArgs e)
		{
			//TODO 会引起问题，先不做这个功能了
			//保证加载完毕后
			//if (dgvBoxes.Rows.Count == _ocrImage.ImageBoxList.Boxes.Count + 1)
			//{
			//	DataGridViewRow row = dgvBoxes.Rows[dgvBoxes.CurrentRow.Index];
			//	int sn = Convert.ToInt32(row.Cells["sn"].Value);
			//	_ocrImage.ImageBoxList.GetBoxBySn(sn).Selected = row.Selected;
			//	RefreshBoxImageInPictureBox();
			//}
		}


		/// <summary>
		///     在图片框中滚动到指定 Box 位置(使其在图片框中心)
		/// </summary>
		/// <param name="box"></param>
		private void ScrollToInPictureBoxByBox(Box box)
		{
			//注意：picturebox会被撑大，所以需要用 Panel
			int targetX = Convert.ToInt32(box.X + 0.5 * box.Width - 0.5 * pnlPictureBox.Width);
			int targetY = Convert.ToInt32(box.Y + 0.5 * box.Height - 0.5 * pnlPictureBox.Height);

			pnlPictureBox.AutoScrollPosition = new Point(targetX, targetY);
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

			string newCharacter = "";
			foreach (Box box in _ocrImage.ImageBoxList.SelectedBoxes)
			{
				newCharacter += box.Character;
			}

			//改变第一个选中Box的大小，并将字符改为所有选中 Box 的字符的拼接

			Box firstBox = _ocrImage.ImageBoxList.SelectedBoxes[0];
			firstBox.Character = newCharacter;
			firstBox.X = minX;
			firstBox.Y = minY;
			firstBox.Width = maxX - minX;
			firstBox.Height = maxY - minY;


			//删除其他的
			for (int i = _ocrImage.ImageBoxList.SelectedBoxes.Count - 1; i >= 1; i--)
			{
				Box box = _ocrImage.ImageBoxList.SelectedBoxes[i];
				DeleteBoxInDataGridView(box);
				_ocrImage.ImageBoxList.Remove(box);
			}
			RefreshBoxImageInPictureBox();
		}

		/// <summary>
		///     从数据网格中删除指定 Box 对应的数据
		/// </summary>
		/// <param name="box"></param>
		private void DeleteBoxInDataGridView(Box box)
		{
			int rowCount = dgvBoxes.RowCount;
			for (int i = rowCount - 1; i >= 0; i--)
			{
				DataGridViewRow row = dgvBoxes.Rows[i];
				if (Convert.ToInt32(row.Cells["sn"].Value) == box.Sn)
				{
					dgvBoxes.Rows.Remove(row);
				}
			}
		}

		/// <summary>
		///     在数据网格中选择指定的 Box 对应的数据
		/// </summary>
		/// <param name="box"></param>
		//private void RefreshBoxSelectionInDataGridView()
		//{
		//	int boxCount = _ocrImage.ImageBoxList.Boxes.Count;
		//	int lastSelectedBoxIndex = -1;
		//	//for (int i = 0; i < boxCount; i++)
		//	//{
		//	//	dgvBoxes.Rows[i].Selected = _ocrImage.ImageBoxList.Boxes[i].Selected;

		//	//	if (dgvBoxes.Rows[i].Selected)
		//	//	{
		//	//		lastSelectedBoxIndex = i;
		//	//	}
		//	//}
		//	dgvBoxes.CurrentCell = dgvBoxes.Rows[lastSelectedBoxIndex].Cells[1];
		//	for (int i = 0; i < boxCount; i++)
		//	{
		//		dgvBoxes.Rows[i].Selected = _ocrImage.ImageBoxList.Boxes[i].Selected;

		//		if (dgvBoxes.Rows[i].Selected)
		//		{
		//			lastSelectedBoxIndex = i;
		//		}
		//	}
		//}
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

			if (!bgwProcess.IsBusy)
			{
				bgwProcess.RunWorkerAsync(makeBoxProcess);
			}

			//等待处理完毕
			while (bgwProcess.IsBusy)
			{
				Application.DoEvents();
			}

			_ocrImage.LoadFromBoxFile();


			//pbxExample.BackgroundImage = (pbxExample.Image.Clone() as Bitmap);
			//pbxExample.Image = _ocrImage.GetBoxesImage(pbxExample.BackgroundImage as Bitmap);
			RefreshBoxImageInPictureBox();

			//重新显示


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
			//使旋转按钮失效（防止多次点造成存文件不正常）
			DisableAllFileRelatedControls();
			//进行逆时针旋转90°操作
			//先不旋转图片，因为会把图片文件方向也改变
			RotateImage(RotateFlipType.Rotate270FlipNone);
			_ocrImage.ChangeBoxCoordinates(RotateFlipType.Rotate270FlipNone);
			ShowBoxInfoInDataGridView();
			EnableAllFileRelatedControls();
		}

		/// <summary>
		///     把所有可能会造成文件冲突的控件失效
		/// </summary>
		private void DisableAllFileRelatedControls()
		{
			btnRotateLeft.Enabled = false;
			btnRotateRight.Enabled = false;
		}

		/// <summary>
		///     把所有可能会造成文件冲突的控件生效
		/// </summary>
		private void EnableAllFileRelatedControls()
		{
			//等待文件占用完毕
			while (Common.IsInUse(_ocrImage.TempImageInfo.FilePath) || Common.IsInUse(_ocrImage.OriginalImageInfo.FilePath))
			{
				Application.DoEvents();
			}
			btnRotateLeft.Enabled = true;
			btnRotateRight.Enabled = true;
		}


		/// <summary>
		///     根据 Box 的选中状态，改变数据网格中所有行的选中状态
		/// </summary>
		private void RefreshBoxSelectionInDataGridView()
		{
			int boxCount = _ocrImage.ImageBoxList.Boxes.Count;
			for (int i = 0; i < boxCount; i++)
			{
				dgvBoxes.Rows[i].Selected = _ocrImage.ImageBoxList.Boxes[i].Selected;
			}
		}

		private void RotateImage(RotateFlipType rotateFilpType)
		{
			int previousHeight = pbxExample.BackgroundImage.Height;
			int previousWidth = pbxExample.BackgroundImage.Width;


			try
			{
				//若文件正在使用，等一会儿
				//if (!Common.IsInUse(_ocrImage.TempImageInfo.FilePath) && !Common.IsInUse(_ocrImage.OriginalImageInfo.FilePath))
				//{
				pbxExample.BackgroundImage.RotateFlip(rotateFilpType);
				pbxExample.Image.RotateFlip(rotateFilpType);

				//旋转时，图片框大小也要变化（否则会造成背景图片不对）
				pbxExample.Height = previousWidth;
				pbxExample.Width = previousHeight;
				pbxExample.Refresh();
				//保存旋转后的临时文件
				pbxExample.BackgroundImage.Save(_ocrImage.TempImageInfo.FilePath,
					Common.GetImageFormatByExtName(_ocrImage.TempImageInfo.ExtFileName));

				//将原文件也旋转后保存
				Bitmap originalBitmap = _ocrImage.GetOriginalImage();
				originalBitmap.RotateFlip(rotateFilpType);
				originalBitmap.Save(_ocrImage.OriginalImageInfo.FilePath,
					Common.GetImageFormatByExtName(_ocrImage.OriginalImageInfo.ExtFileName));
				//}
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
			//使旋转按钮失效（防止多次点造成存文件不正常）
			DisableAllFileRelatedControls();
			RotateImage(RotateFlipType.Rotate90FlipNone);
			_ocrImage.ChangeBoxCoordinates(RotateFlipType.Rotate90FlipNone);
			ShowBoxInfoInDataGridView();
			//使旋转按钮失效（防止多次点造成存文件不正常）
			EnableAllFileRelatedControls();
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
				DisposeAllResources();

				_ocrImage = new OcrImage(ofdFile.FileName);
				OpenImage(_ocrImage);

				dgvBoxes.Focus();
				//RefreshBoxesInPictureBox();
				//RefreshBoxesInfoInGridView();
			}
		}

		private void frmTrainingTool_FormClosed(object sender, FormClosedEventArgs e)
		{
			DisposeAllResources();
		}

		/// <summary>
		///     释放所有占用的资源
		/// </summary>
		private void DisposeAllResources()
		{
			if (pbxExample.Image != null)
			{
				pbxExample.Image.Dispose();
			}

			if (pbxExample.BackgroundImage != null)
			{
				pbxExample.BackgroundImage.Dispose();
			}
		}


		private void button1_Click_1(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in dgvBoxes.Rows)
			{
				foreach (DataGridViewCell cell in row.Cells)
				{
					cell.Value = "1";
				}
			}
		}

		private void AddBoxToDataGridView(Box box)
		{
			int index = dgvBoxes.Rows.Add();

			dgvBoxes.Rows[index].Cells["sn"].Value = box.Sn;
			dgvBoxes.Rows[index].Cells["character"].Value = box.Character;
			dgvBoxes.Rows[index].Cells["x"].Value = box.X;
			dgvBoxes.Rows[index].Cells["y"].Value = box.Y;
			dgvBoxes.Rows[index].Cells["width"].Value = box.Width;
			dgvBoxes.Rows[index].Cells["height"].Value = box.Height;
		}

		private void dgvBoxes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void dgvBoxes_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
		{
			//if (e.RowIndex == dgvBoxes.RowCount)
			//{
			//	return;
			//}
			//else
			//{

			Box box = _ocrImage.ImageBoxList.Boxes[e.RowIndex];
			switch (e.ColumnIndex)
			{
				case 0:
					e.Value = box.Sn;
					break;
				case 1:
					e.Value = box.Character;
					break;
				case 2:
					e.Value = box.X;
					break;
				case 3:
					e.Value = box.Y;
					break;
				case 4:
					e.Value = box.Width;
					break;
				case 5:
					e.Value = box.Height;
					break;
				default:
					break;
			}
			//}
		}

		private void dgvBoxes_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			dgvBoxes.Rows[e.RowIndex].HeaderCell.Value = e.RowIndex;
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