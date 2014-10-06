using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SFY_OCR.Properties;

namespace SFY_OCR
{
	public partial class Main : Form
	{
		//用于保存所有选中的图片文件
		private readonly List<string> imageFilePaths = new List<string>();

		// 与PictureBox缩放相关
		#region
		private Point mouseOriginalLocation; //鼠标原始位置
		private int mouse_move_offset_x; //鼠标移动x方向上的偏移量
		private int mouse_move_offset_y; //鼠标移动y方向上的偏移量
		private int mouse_offset_x; // 鼠标x位置与位置中心的偏移量
		private int mouse_offset_y; // 鼠标y位置与位置中心的偏移量
		private Point picLocation; //图片当前位置
		private float scale_x = 1f; //图片x位置变化幅度
		private float scale_y = 1f; //图片y位置变化幅度
		private Size stopScalingSize; // 图片停止缩放时图片大小（如图片大于窗体大小则为窗体大小）
		#endregion

		public Main()
		{
			InitializeComponent();

			//防止闪屏
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint,
				true);
			base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
		}


		/// <summary>
		///     点击 文件 → 打开
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//导入图片
			if (ofdPicture.ShowDialog() == DialogResult.OK)
			{
				//将选择的所有文件的文件名加入列表框，并在 imageFileNames 中保存文件路径
				foreach (string fileName in ofdPicture.FileNames)
				{
					//lbxImages.Items.Add(fileName.Substring(fileName.LastIndexOf("\\") + 1));
					lbxImages.Items.Add(fileName);
					imageFilePaths.Add(fileName);
				}

				lbxImages.SelectedIndex = 0;

			}
		}

		private void 选项OToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//打开选项对话框
			var frmOptions = new Options();

			frmOptions.ShowDialog();
		}


		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			//关闭窗体
			if (MessageBox.Show(this, "确定退出程序吗?", "问题", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2) == DialogResult.No)
			{
				e.Cancel = true;
			}
		}


		private void lbxImages_SelectedIndexChanged(object sender, EventArgs e)
		{
			//显示图片
			ShowImage(imageFilePaths[lbxImages.SelectedIndex]);

			//显示结果文件的内容
			ShowResultFile(imageFilePaths[lbxImages.SelectedIndex] + ".txt");


		}

		private void Main_Load(object sender, EventArgs e)
		{
			//设置OpenFileDialog的filter属性
			ofdPicture.Filter = "JPG图片文件|*.JPG;*.JPEG|所有文件|*.*";

			//设置图片列表框的DisplayMember和ValueMember属性
			lbxImages.DisplayMember = "Text";
			lbxImages.ValueMember = "Value";

			//注册PictureBox的鼠标滚轮事件
			pbxForOcr.MouseWheel += pbxForOcr_MouseWheel; //鼠标滚轮事件

			//设置backgroundWorker的属性
			backgroundWorkerInvokeCommand.WorkerReportsProgress = true;
			backgroundWorkerInvokeCommand.WorkerSupportsCancellation = true;

			//设置“取消”按钮为不可用
			btnCancel.Enabled = false;
		}


		private void pbxForOcr_MouseWheel(object sender, MouseEventArgs e)
		{
			mouse_offset_x = e.X - pbxForOcr.Width / 2;
			mouse_offset_y = e.Y - pbxForOcr.Height / 2;

			scale_x = GetLocationScale(pbxForOcr.Width / 2, mouse_offset_x);
			scale_y = GetLocationScale(pbxForOcr.Height / 2, mouse_offset_y);

			Size t = pbxForOcr.Size;
			t.Width += e.Delta;
			t.Height += e.Delta;

			Point p = picLocation;
			p.X += (int)(((float)(pbxForOcr.Width - t.Width)) / 2 * scale_x);
			p.Y += (int)(((float)(pbxForOcr.Height - t.Height)) / 2 * scale_y);

			if (t.Width > stopScalingSize.Width || t.Height > stopScalingSize.Height)
			{
				pbxForOcr.Width = t.Width;
				pbxForOcr.Height = t.Height;
				picLocation = p;
			}
			pbxForOcr.Location = picLocation;
		}


		private float GetLocationScale(int range, int offset)
		{
			float s = 1f;
			if (offset < 0)
			{
				s = 1f - -offset / (float)range;
			}
			else if (offset > 0)
			{
				s = 1f + offset / (float)range;
			}
			return s;
		}


		private void 清空CToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "确定清空所有已导入的图片吗?", "问题", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				//清空所有导入的图片
				imageFilePaths.Clear();
				lbxImages.Items.Clear();
				pbxForOcr.ImageLocation = null;
			}
		}

		private void btnOcr_Click(object sender, EventArgs e)
		{
			const string langType = "chi_sim";


			if (lbxImages.Items.Count != 0)
			{
				try
				{
					//设置取消按钮为可用
					btnCancel.Enabled = true;

					List<object> arguments = new List<object>();

					//tesseract.exe 所在文件夹
					arguments.Add(Settings.Default.TesseractOcrDir);

					//输出文件夹
					arguments.Add(Settings.Default.OutputDir);

					//要识别的图片路径集(List<string格式>)
					arguments.Add(imageFilePaths);

					//语言包类型
					arguments.Add(langType);

					if (backgroundWorkerInvokeCommand.IsBusy == false)
					{
						backgroundWorkerInvokeCommand.RunWorkerAsync(arguments);
					}
				}
				catch (Exception exception)
				{
					throw new Exception(exception.Message);
				}
			}
			else
			{
				MessageBox.Show("请先导入要识别的图片", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void tmrProgressBar_Tick(object sender, EventArgs e)
		{
		}

		private void backgroundWorkerInvokeCommand_DoWork(object sender, DoWorkEventArgs e)
		{
			//将各参数拆箱
			string tesseractOcrDir = ((List<object>)e.Argument)[0].ToString();
			string outputDir = ((List<object>)e.Argument)[1].ToString();
			List<string> imageFilePaths = (List<string>)((List<object>)e.Argument)[2];
			string langType = ((List<object>)e.Argument)[3].ToString();

			//进度条前期空
			int beforeSpan = 10;
			//进度条后期空
			int afterSpan = 10;

			//先显示一点进度条
			backgroundWorkerInvokeCommand.ReportProgress(beforeSpan);

			for (int i = 0; i < imageFilePaths.Count; i++)
			{
				//取消当前操作
				if (backgroundWorkerInvokeCommand.CancellationPending)
				{
					e.Cancel = true;

					return;
				}

				//获取当前图片路径
				string imageFilePath = imageFilePaths[i];
				//获取图片对应的结果文件的文件路径（默认在图片路径后系统自动加.txt)
				string outputResultFilePath = outputDir + imageFilePath.Substring(imageFilePath.LastIndexOf("\\") + 1);

				Common.InvokeOcrCommandLine(tesseractOcrDir, imageFilePath, outputResultFilePath, langType);

				//报告进度
				backgroundWorkerInvokeCommand.ReportProgress(beforeSpan + (i + 1) * (100 - beforeSpan - afterSpan) / imageFilePaths.Count);
			}

			backgroundWorkerInvokeCommand.ReportProgress(100-afterSpan);
			//稍微暂停一下，以表现满格前最后一步动作
			Thread.Sleep(500);

			//进度条满格
			backgroundWorkerInvokeCommand.ReportProgress(100);
			//稍微暂停一下，以表现满格动作
			Thread.Sleep(500);
		

		}



		private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//退出程序
			Application.Exit();
		}

		private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//显示“关于”对话框
			var frmMyAboutBox = new MyAboutBox();

			frmMyAboutBox.ShowDialog();
		}

		private void pbxForOcr_MouseDown(object sender, MouseEventArgs e)
		{
			mouseOriginalLocation = e.Location;
			//记录下鼠标原始位置
			Cursor = Cursors.SizeAll;
		}

		private void pbxForOcr_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouse_move_offset_x = mouseOriginalLocation.X - e.Location.X;
				mouse_move_offset_y = mouseOriginalLocation.Y - e.Location.Y;

				picLocation.X = pbxForOcr.Location.X - mouse_move_offset_x;
				picLocation.Y = pbxForOcr.Location.Y - mouse_move_offset_y;
				pbxForOcr.Location = picLocation;
			}
			else
			{
				Cursor = Cursors.Default;
			}
		}

		private void pbxForOcr_MouseEnter(object sender, EventArgs e)
		{
			pbxForOcr.Focus();
		}

		private void backgroundWorkerInvokeCommand_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//进度条进度清零
			progressBarOcr.Value = 0;

			MessageBox.Show("识别完毕！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

			//识别完毕后，选中第一张图片
			lbxImages.SelectedIndex = 0;

			//重新设置“取消”按钮为不可用
			btnCancel.Enabled = false;
		}

		/// <summary>
		/// 用于在图片框中显示指定路径的图片
		/// </summary>
		/// <param name="resultFilePath">要显示的图片路径</param>
		private void ShowResultFile(string resultFilePath)
		{
			//在结果文件存在的前提下，显示其内容
			if (File.Exists(resultFilePath))
			{
				txtResult.Text = Common.GetFileContent(resultFilePath);
			}
			else
			{
				//MessageBox.Show("该图片对应的识别结果不存在，请重新识别。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 用于在文本框中显示指定路径的识别结果
		/// </summary>
		/// <param name="imageFilePath">要显示的识别结果路径</param>
		private void ShowImage(string imageFilePath)
		{
			if (File.Exists(imageFilePath))
			{
				pbxForOcr.ImageLocation = imageFilePath;
			}
			else
			{
				//MessageBox.Show("该图片不存在，请重新选择。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			//取消BackgroundWorker当前操作
			backgroundWorkerInvokeCommand.CancelAsync();
		}

		private void backgroundWorkerInvokeCommand_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			progressBarOcr.Value = e.ProgressPercentage;
		}
	}
}