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
		private List<string> imageFilePaths = new List<string>();
		private const string NO_LANGUAGE_DATA_COMBOBOX_TEXT= "无语言文件";

		public Main()
		{
			InitializeComponent();
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

				//导入图片后马上检测结果文件状态
				CheckoutputResultFileStatus();

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


		/// <summary>
		/// 在更改选中的图片同时显示图片及对应的结果文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lbxImages_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lbxImages.SelectedIndex != -1)
			{
				//获取当前选中的图片的路径
				string imageFilePath = imageFilePaths[lbxImages.SelectedIndex];
				//显示图片
				ShowImage(imageFilePath);

				string resultFilePath = Settings.Default.OutputDir + imageFilePath.Substring(imageFilePath.LastIndexOf("\\") + 1) +
				                        ".txt";
				//显示结果文件的内容
				ShowResultFile(resultFilePath);
			}

		}

		private void Main_Load(object sender, EventArgs e)
		{
			//设置OpenFileDialog的filter属性
			ofdPicture.Filter = "JPG|*.JPG;*.JPEG|TIFF|*.TIFF;*.TIF|所有文件|*.*";

			//设置图片列表框的DisplayMember和ValueMember属性
			lbxImages.DisplayMember = "Text";
			lbxImages.ValueMember = "Value";



			//设置backgroundWorker的属性
			backgroundWorkerInvokeCommand.WorkerReportsProgress = true;
			backgroundWorkerInvokeCommand.WorkerSupportsCancellation = true;

			//设置“取消”按钮为不可用
			btnCancel.Enabled = false;

			//设置PictureBox中的Label不可见
			lblPictureBoxError.Visible = false;
			lblPictureBoxError.Left = pbxForOcr.Left + 5;
			lblPictureBoxError.Top = pbxForOcr.Top + 5;
			//lblPictureBoxError.Text = "";

			//设置主窗体双缓冲，减少闪烁
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
				  ControlStyles.ResizeRedraw |
				  ControlStyles.AllPaintingInWmPaint, true);

			//显示语言文件
			ShowLanguageType();

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
				lblPictureBoxError.Visible = false;
				txtResult.Text = "";
			}
		}

		private void btnOcr_Click(object sender, EventArgs e)
		{
			string langType = cbxLanguageType.Text;


			if (lbxImages.Items.Count == 0)
			{
				MessageBox.Show("请先导入要识别的图片", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (cbxLanguageType.Text == NO_LANGUAGE_DATA_COMBOBOX_TEXT)
			{
				MessageBox.Show("无语言文件，无法识别。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
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


					//lbxImages.SelectedIndex = 0;
				}
				catch (Exception exception)
				{
					throw new Exception(exception.Message);
				}
			}
			
		}

		private void CheckoutputResultFileStatus()
		{
			for (int i = 0; i < imageFilePaths.Count; i++)
			{
				string resultFilePath = Settings.Default.OutputDir +
										imageFilePaths[i].Substring(imageFilePaths[i].LastIndexOf("\\") + 1) + ".txt";
				if (File.Exists(resultFilePath))
				{
					//避免再次显示“已识别”
					if (!lbxImages.Items[i].ToString().Contains("(已识别)"))
					{
						lbxImages.Items[i] += "    (已识别)";
					}
				}
			}
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

			backgroundWorkerInvokeCommand.ReportProgress(100 - afterSpan);
			//稍微暂停一下，以表现满格前最后一步动作
			Thread.Sleep(1000);

			//进度条满格
			backgroundWorkerInvokeCommand.ReportProgress(100);
			//稍微暂停一下，以表现满格
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
		private void backgroundWorkerInvokeCommand_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			//进度条进度清零
			progressBarOcr.Value = 0;

			MessageBox.Show("识别完毕！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

			//识别完毕后，选中第一张图片
			//lbxImages.SelectedIndex = -1;
			lbxImages.SelectedIndex = 0;

			//重新设置“取消”按钮为不可用
			btnCancel.Enabled = false;

			//显示图片识别状态
			CheckoutputResultFileStatus();
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
				txtResult.Text = "错误：该图片对应的识别结果不存在，请识别。";
				//MessageBox.Show("该图片对应的识别结果不存在，请重新识别。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 用于在文本框中显示指定路径的图片
		/// </summary>
		/// <param name="imageFilePath">要显示的识别结果路径</param>
		private void ShowImage(string imageFilePath)
		{
			if (File.Exists(imageFilePath))
			{
				lblPictureBoxError.Visible = false;
				pbxForOcr.ImageLocation = imageFilePath;
			}
			else
			{
				pbxForOcr.ImageLocation = null;
				lblPictureBoxError.Visible = true;
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


		/// <summary>
		/// 重写方法，改善闪烁
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;////用双缓冲绘制窗口的所有子控件
				return cp;
			}
		}

		/// <summary>
		/// 在下拉菜单中显示语言
		/// </summary>
		private void ShowLanguageType()
		{
			string[] trainedDataFilePaths = Directory.GetFiles(Settings.Default.TesseractOcrDir + "\\tessdata");

			foreach (string trainedDataFilePath in trainedDataFilePaths)
			{
				string extName = trainedDataFilePath.Substring(trainedDataFilePath.LastIndexOf(".") + 1);
				string trainedDataFileName = trainedDataFilePath.Substring(trainedDataFilePath.LastIndexOf("\\") + 1);

				//如果文件扩展名是 .traineddata
				if (extName == "traineddata")
				{
					cbxLanguageType.Items.Add(trainedDataFileName.Replace(".traineddata", ""));
				}
			}


			//若无语言文件，显示“无语言文件”
			if (cbxLanguageType.Items.Count == 0)
			{
				cbxLanguageType.Items.Add(NO_LANGUAGE_DATA_COMBOBOX_TEXT);
			}
			//选中第一项（可能是“无语言文件那项”）
			cbxLanguageType.SelectedIndex = 0;
		}

		private void 训练TToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//打开训练对话框
			var frmTraining = new TrainingTool();

			frmTraining.ShowDialog();
		}
	}
}