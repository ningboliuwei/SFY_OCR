#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
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
		private Point _boxLeftTopPoint;
		private Point _boxStartPoint;
		//矩形图层
		private Bitmap _boxesImage;
		private Point _currentPoint;
		private bool _isDrawingBox;

		//当前鼠标是否在移动
		private OcrImage _ocrImage;
		//之前的图层（用于擦除矩形）
		private Bitmap _previousImage;

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
				_ocrImage = new OcrImage(ofdFile.FileName);
				OpenImage(_ocrImage);
				RefreshBoxesInPictureBox();
				RefreshBoxesInfoInGridView();
			}
		}


		/// <summary>
		///     打开样本图片，自动复制一份到输出目录，并将该副本显示在文本框中。
		/// </summary>
		/// <param name="sourceImage">原本（被打开）的图片（OcrImage）对象</param>
		private void OpenImage(OcrImage sourceImage)
		{
			//将原本复制一份副本到输出目录
			try
			{
				File.Copy(sourceImage.OriginalImageInfo.FilePath, sourceImage.TempImageInfo.FilePath, true);
				//显示该副本图片
				pbxExample.ImageLocation = _ocrImage.TempImageInfo.FilePath;

				//若box文件不存在，则创建；否则加载并显示
				if (!File.Exists(_ocrImage.BoxFileInfo.FilePath))
				{
					_ocrImage.CreateBoxFile();
				}
				else
				{
					_ocrImage.LoadFromBoxFile();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
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
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			//设置选择模式为全行
			dgvBoxes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvBoxes.ReadOnly = true;
			//设置用于清除之前画的矩形的定时器间隔（ms）
			//tmrClearBox.Interval = 10;

			//注册Box选中状态改变事件
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
					sourceImage.TempImageInfo.FilePath, convertArgs["filter_size"], convertArgs["off_set"], convertArgs["bgcolor"],
					convertArgs["deskew"], sourceImage.TempImageInfo.FilePath);

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
			ConvertDelegatePackage package = (ConvertDelegatePackage)e.Argument;

			package.ConvertImageDelegate.Invoke(_ocrImage, package.ConvertImageArgs);
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
		private void bgwProcessImage_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//当背景线程处理完毕后，显示处理完的图像
			pbxExample.ImageLocation = _ocrImage.TempImageInfo.FilePath;
		}

		private void pbxExample_MouseDown(object sender, MouseEventArgs e)
		{
			_boxStartPoint = new Point(e.X, e.Y);
			_boxEndPoint = new Point(e.X, e.Y);

			//按下左键才进入画 box 模式
			if (e.Button == MouseButtons.Left)
			{
				_isDrawingBox = true;
			}
		}


		/// <summary>
		///     在图片上绘制用于识别的矩形
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pbxExample_MouseMove(object sender, MouseEventArgs e)
		{
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


		private void pbxExample_MouseUp(object sender, MouseEventArgs e)
		{
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

		public void RefreshBoxesInfoInGridView()
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

				dataRow["sn"] = i + 1;
				dataRow["character"] = currentBox.Character;
				dataRow["x"] = currentBox.X;
				dataRow["y"] = currentBox.Y;
				dataRow["width"] = currentBox.Width;
				dataRow["height"] = currentBox.Height;

				boxesTable.Rows.Add(dataRow);
			}

			dgvBoxes.DataSource = boxesTable;

			dgvBoxes.Columns["sn"].HeaderText = "序号";
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

			//默认按照顺序排序
			dgvBoxes.Sort(dgvBoxes.Columns["sn"], ListSortDirection.Ascending);
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
			if (Control.ModifierKeys == Keys.Control && e.Button == MouseButtons.Left)//点击鼠标左键时
			{
				//_ocrImage.ImageBoxList.UnSelectAll();

				foreach (Box box in _ocrImage.ImageBoxList.Boxes)
				{
					if (box.Contains(e.X, e.Y))
					{
						box.Selected = !box.Selected;
					}
				}

				if (_ocrImage.ImageBoxList.SelectedBoxes.Count == 0)
				{
					//?s
				}

				RefreshBoxesInPictureBox();
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
				_ocrImage.ImageBoxList.Add(new Box(initialCharacter, initialX, initialY, initialWidth, initialHeight));
				//选中刚添加的Box
				_ocrImage.ImageBoxList.GetBoxByCoordinate(initialX, initialY, initialWidth, initialHeight).Selected = true;
				RefreshBoxesInPictureBox();
				RefreshBoxesInfoInGridView();
			}
		}

		private void RefreshBoxesInPictureBox()
		{
			Bitmap background = new Bitmap(_ocrImage.TempImageInfo.FilePath);
			//将临时文件读取到内存中
			_ocrImage.DrawBoxesOnBitmap(background);
			//将内存中的Bitmap（Box图层）绘制到PictureBox控件中
			pbxExample.Image = background;
		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			_ocrImage.SaveToBoxFile();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			RefreshBoxesInPictureBox();
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

			ShowBoxInfoInHeader();


		}

		private void ShowBoxInfoInHeader()
		{
			if (dgvBoxes.SelectedRows.Count == 1) //若只选中一行，显示信息
			{
				DataGridViewRow row = dgvBoxes.CurrentRow;

				Box currentBox = _ocrImage.ImageBoxList.GetBoxByCoordinate(Convert.ToInt32(row.Cells["x"].Value),
					Convert.ToInt32(row.Cells["y"].Value), Convert.ToInt32(row.Cells["width"].Value),
					Convert.ToInt32(row.Cells["height"].Value));

				txtCharacter.Text = currentBox.Character;
				nudX.Text = currentBox.X.ToString();
				nudY.Text = currentBox.Y.ToString();
				nudWidth.Text = currentBox.Width.ToString();
				nudHeight.Text = currentBox.Height.ToString();

				txtCharacter.Enabled = true;
				nudX.Enabled = true;
				nudY.Enabled = true;
				nudWidth.Enabled = true;
				nudHeight.Enabled = true;
			}
			else //否则不显示
			{
				txtCharacter.Text = "";
				nudX.Text = "";
				nudY.Text = "";
				nudWidth.Text = "";
				nudHeight.Text = "";

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
			RefreshBoxesInPictureBox();
			//刷新选中的Box信息
			RefreshBoxesInfoInGridView();
			//删除后清除所有的选择
			dgvBoxes.ClearSelection();
		}

		/// <summary>
		///     根据数据网格中的选中项删除对应的Box
		/// </summary>
		private void dgvBoxes_SelectionChanged(object sender, EventArgs e)
		{
			ChangeBoxSelectionInDataGridView();
			RefreshBoxesInPictureBox();
		}
		/// <summary>
		/// 当Box的五个参数中的任何一个变化的时候，刷新图片框及网格中的显示
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
				box.X = Convert.ToInt32(nudX.Text);
				box.Y = Convert.ToInt32(nudY.Text);
				box.Width = Convert.ToInt32(nudWidth.Text);
				box.Height = Convert.ToInt32(nudHeight.Text);

				RefreshBoxesInPictureBox();
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
			if (e.KeyChar == (char) Keys.Return)
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

	}
}