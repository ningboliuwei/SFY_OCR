#region

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using SFY_OCR.Properties;

#endregion

namespace SFY_OCR.Untilities
{
	public class OcrImage
	{
		public OcrImage(string imageFilePath)
		{
			//原始文件（如：D:\images\a.tiff）
			OriginalImageInfo = new FileNameInfo(imageFilePath);
			//临时文件（如： R:\a.temp.tiff）
			TempImageInfo =
				new FileNameInfo(Settings.Default.OutputDir + OriginalImageInfo.MainFileName + StringResourceManager.TempImageSuffix +
								 "." +
								 OriginalImageInfo.ExtFileName);
			//对应的Box文件（如 R:\a.tiff.box）
			BoxFileInfo =
				new FileNameInfo(TempImageInfo.Dir + TempImageInfo.MainFileName + "." +
								 StringResourceManager.BoxFileExtName);
			//创建BoxList对象
			ImageBoxList = new BoxList();
		}

		/// <summary>
		///     Box列表
		/// </summary>
		public BoxList ImageBoxList { get; private set; }

		/// <summary>
		///     原始图片文件路径
		/// </summary>
		public FileNameInfo OriginalImageInfo { get; private set; }

		/// <summary>
		///     临时图片路径（供降噪处理等）
		/// </summary>
		public FileNameInfo TempImageInfo { get; private set; }

		/// <summary>
		///     Box文件路径
		/// </summary>
		public FileNameInfo BoxFileInfo { get; private set; }

		/// <summary>
		///     在一个BitMap对象中画上矩形
		/// </summary>
		/// <param name="image">需要画矩形的BitMap对象（如PictureBox.Image）</param>
		/// <param name="bitmap"></param>
		/// <returns>画毕的BitMap对象</returns>
		public Bitmap GetBoxesImage(Size imageSize)
		{
			Bitmap bitmap = new Bitmap(imageSize.Width, imageSize.Height);
			Graphics g = Graphics.FromImage(bitmap);
			foreach (Box box in ImageBoxList.Boxes)
			{
				//若当前矩形框被选中，使用红色，否则使用蓝色
				Color borderColor = box.Selected ? Color.Red : Color.Blue;
				int borderWidth = box.Selected
					? Convert.ToInt32(StringResourceManager.SelectedBoxBorderWidth)
					: Convert.ToInt32(StringResourceManager.BoxBorderWidth);
				//在传入的BitMap上画矩形
				
				int offset = borderWidth;
				g.DrawRectangle(new Pen(borderColor, offset), box.X,
					box.Y, box.Width, box.Height);

				#region 画被选中 Box 的锚点

				//if (box.Selected)
				//{
				//	int sideLength = borderWidth * 3;
				//	SolidBrush solidBrush = new SolidBrush(Color.DarkGreen);

				//	//上左
				//	g.FillRectangle(solidBrush, box.X, box.Y - offset, sideLength, sideLength);
				//	//上中
				//	g.FillRectangle(solidBrush, box.X + (box.Width/2), box.Y, sideLength, sideLength);
				//	//上右
				//	g.FillRectangle(solidBrush, box.X + box.Width - offset, box.Y - offset, sideLength, sideLength);
				//	//中左
				//	g.FillRectangle(solidBrush, box.X - offset, box.Y + (box.Height / 2) - offset, sideLength, sideLength);
				//	//中右
				//	g.FillRectangle(solidBrush, box.X + box.Width - offset, box.Y + (box.Height / 2) - offset, sideLength, sideLength);
				//	//下左
				//	g.FillRectangle(solidBrush, box.X - offset, box.Y + box.Height - offset, sideLength, sideLength);
				//	//下中
				//	g.FillRectangle(solidBrush, box.X + (box.Width / 2) - offset, box.Y + box.Height - offset, sideLength, sideLength);
				//	//下右
				//	g.FillRectangle(solidBrush, box.X + box.Width - offset, box.Y + box.Height - offset, sideLength, sideLength);

				//}

				#endregion
			}
			return bitmap;
		}

		/// <summary>
		///     从Box文件加载所有的Box数据
		/// </summary>
		/// <param name="filePath"></param>
		public void LoadFromBoxFile()
		{
			//清空之前所有的Box
			ImageBoxList.Boxes.Clear();
			StreamReader streamReader = null;

			string fileContent;

			try
			{
				streamReader = new StreamReader(BoxFileInfo.FilePath, Encoding.UTF8);
				fileContent = streamReader.ReadToEnd().Trim();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				if (streamReader != null)
				{
					streamReader.Close();
				}
			}

			

			if (fileContent.Length != 0)
			{
				string[] lines = fileContent.Split('\n');
				//一次读取Box文件中的一行
				//注意，这里读取的Y坐标是BOX文件Y坐标
				int imageHeight = GetOriginalImage().Height;
				int index = 1;
				foreach (string currentLine in lines)
				{
					string[] items = currentLine.Split(' ');

					//从文件中载入：字符，BOX左边界离图像左边界距离，BOX下边界离图像下边界距离，BOX右边界离图像左边界距离，BOX上边界离图像下边界距离
					//文件中的最后一个 0 省略（因不知道何用）
					ImageBoxList.Boxes.Add(new Box(index, items[0], Convert.ToInt32(items[1]),
						imageHeight - Convert.ToInt32(items[4]),
						Convert.ToInt32(items[3]) - Convert.ToInt32(items[1]),
						Convert.ToInt32(items[4]) - Convert.ToInt32(items[2])));
					index++;
				}
			}
			else
			{
				//TODO
				//throw new Exception("Box文件损坏，请重新识别。");
			}
		}

		/// <summary>
		///     获取原图像
		/// </summary>
		/// <returns></returns>
		public Bitmap GetOriginalImage()
		{
			return GetImage(OriginalImageInfo.FilePath);
		}

		/// <summary>
		///     获取临时图像
		/// </summary>
		/// <returns></returns>
		public Bitmap GetTempImage()
		{
			return GetImage(TempImageInfo.FilePath);
		}

		/// <summary>
		///     获取指定路径的 Bitmap 对象
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		private Bitmap GetImage(string filePath)
		{
			Bitmap bitmap = null;

			try
			{
				bitmap = Image.FromFile(filePath) as Bitmap;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return bitmap;
		}

		/// <summary>
		///     将所有的Box数据保存到文件中
		/// </summary>
		/// <param name="filePath"></param>
		public void SaveToBoxFile()
		{
			//第五列的默认值，不知道什么用
			const string fifthColumnValue = "0";
			StreamWriter streamWriter = null;
			try
			{
				streamWriter = new StreamWriter(BoxFileInfo.FilePath, false, Encoding.UTF8);
				int imageHeight = GetOriginalImage().Height;
				foreach (Box box in ImageBoxList.Boxes)
				{
					//保存到文件中：字符，BOX左边界离图像左边界距离，BOX下边界离图像下边界距离，BOX右边界离图像左边界距离，BOX上边界离图像下边界距离
					string[] items =
					{
						box.Character, box.X.ToString(), (imageHeight - box.Y - box.Height).ToString(),
						(box.X + box.Width).ToString(),
						(imageHeight - box.Y).ToString(), fifthColumnValue
					};

					streamWriter.WriteLine(string.Join(" ", items));
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				if (streamWriter != null)
				{
					streamWriter.Close();
				}
			}
		}


		/// <summary>
		///     若图片对应的Box文件（.box）不存在，则根据路径创建
		/// </summary>
		/// <param name="filePath"></param>
		public void CreateBoxFile()
		{
			if (!File.Exists(BoxFileInfo.FilePath))
			{
				//Common.InvokeMakeBoxCommandLine(new Dictionary<string, string>
				//{
				//	{"imageFilePath", TempImageInfo.FilePath},
				//	{"boxFilePath", BoxFileInfo.Dir + BoxFileInfo.MainFileName}
				//});
			}
		}

		/// <summary>
		/// 旋转时改变所有 Box 的坐标
		/// </summary>
		/// <param name="rotateFlipType"></param>
		public void ChangeBoxCoordinates(RotateFlipType rotateFlipType)
		{
			//此时文件已经是旋转后的文件，所以宽度和高度需要对调才符合旋转之前的状态
			Size rotatedSize = new Size(GetOriginalImage().Height, GetOriginalImage().Width);

			switch (rotateFlipType)
			{
				case RotateFlipType.Rotate270FlipNone:
					foreach (Box box in ImageBoxList.Boxes)
					{
						//顺序不能搞错
						//左转90度以后，原先的 Y 变成旋转后的 X
						//原先图片宽度 - Box 宽度 - X 变成旋转后的 Y
						int originalY = box.Y;
						box.Y = rotatedSize.Width - box.Width - box.X;
						box.X = originalY;

						//左转90度以后，交换 Box 的宽与高
						int originalHeight = box.Height;
						box.Height = box.Width;
						box.Width = originalHeight;
					}
					break;
				case RotateFlipType.Rotate90FlipNone:
					//右转90度以后，原先的 X 变成新的 Y
					foreach (Box box in ImageBoxList.Boxes)
					{
						//顺序不能搞错
						//左转90度以后，原先的 Y 变成旋转后的 X
						//原先图片宽度 - Box 宽度 - X 变成旋转后的 Y
						int originalX = box.X;
						box.X = rotatedSize.Height - box.Height - box.Y;
						box.Y = originalX;

						//转90度以后，交换 Box 的宽与高
						int originalHeight = box.Height;
						box.Height = box.Width;
						box.Width = originalHeight;
					}
					break;
				default:
					break;
			}

			SaveToBoxFile();
		}

	}
}