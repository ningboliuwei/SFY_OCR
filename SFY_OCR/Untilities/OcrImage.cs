#region

using System;
using System.Drawing;
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
			//临时文件（如： R:\a_temp.tiff）
			TempImageInfo =
				new FileNameInfo(Settings.Default.OutputDir + OriginalImageInfo.FullFileName + StringResourceManager.TempImageSuffix +
				                 "." +
				                 OriginalImageInfo.ExtFileName);
			//对应的Box文件（如 R:\a.tiff.box）
			BoxFileInfo =
				new FileNameInfo(TempImageInfo.Dir + OriginalImageInfo.MainFileName + "." +
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
		public void DrawBoxesOnBitmap(Bitmap bitmap)
		{
			foreach (Box box in ImageBoxList.Boxes)
			{
				//若当前矩形框被选中，使用红色，否则使用蓝色
				Color borderColor = box.Selected ? Color.Red : Color.Blue;
				int borderWidth = box.Selected
					? Convert.ToInt32(StringResourceManager.SelectedBoxBorderWidth)
					: Convert.ToInt32(StringResourceManager.BoxBorderWidth);
				//在传入的BitMap上画矩形
				Graphics.FromImage(bitmap)
					.DrawRectangle(new Pen(borderColor, borderWidth), box.X,
						box.Y, box.Width, box.Height);
			}
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

			try
			{
				streamReader = new StreamReader(BoxFileInfo.FilePath, Encoding.UTF8);
				while (!streamReader.EndOfStream)
				{
					//一次读取Box文件中的一行
					//注意，这里读取的Y坐标是BOX文件Y坐标
					string currentLine = streamReader.ReadLine();
					if (currentLine != null)
					{
						string[] items = currentLine.Split(' ');

						//从文件中载入：字符，BOX左边界离图像左边界距离，BOX下边界离图像下边界距离，BOX右边界离图像左边界距离，BOX上边界离图像下边界距离
						//文件中的最后一个 0 省略（因不知道何用）
						Bitmap bitmap = new Bitmap(TempImageInfo.FilePath);
						int imageHeight = bitmap.Height;
						bitmap.Dispose();

						ImageBoxList.Boxes.Add(new Box(items[0], Convert.ToInt32(items[1]),
							imageHeight - Convert.ToInt32(items[4]),
							Convert.ToInt32(items[3]) - Convert.ToInt32(items[1]),
							Convert.ToInt32(items[4]) - Convert.ToInt32(items[2])));
					}
				}
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
				Bitmap bitmap = new Bitmap(TempImageInfo.FilePath);
				int imageHeight = bitmap.Height;
				bitmap.Dispose();

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
				File.Create(BoxFileInfo.FilePath);
			}
		}
	}
}