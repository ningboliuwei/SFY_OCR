#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using SFY_OCR.Properties;

#endregion

namespace SFY_OCR.Untilities
{
	public class BoxList
	{
		/// <summary>
		///     构造函数，采用聚合方式拥有List<Box>对象
		/// </summary>
		public BoxList(string imageFilePath)
		{
			Boxes = new List<Box>();
			ImageFilePath = imageFilePath;

			string imageFileName = imageFilePath.Substring(imageFilePath.LastIndexOf("\\", StringComparison.Ordinal) + 1);
			//得到图片文件的文件名

			BoxFilePath = Settings.Default.OutputDir +
			              imageFileName.Substring(0, imageFileName.LastIndexOf(".", StringComparison.Ordinal)) + "." +
			              StringResourceManager.BoxFileExtName;
			//生成图片文件对应的.box文件的文件路径

			if (!File.Exists(BoxFilePath))
			{
				CreateBoxFile();
			}
		}

		/// <summary>
		///     .box 文件的路径
		/// </summary>
		public string BoxFilePath { get; private set; }

		/// <summary>
		///     该Box列表对应的图片文件的路径
		/// </summary>
		public string ImageFilePath { get; private set; }

		/// <summary>
		///     存放所有的 Box 对象
		/// </summary>
		public List<Box> Boxes { get; private set; }

		/// <summary>
		///     获得BoxList中所有被选中的Box对象
		/// </summary>
		public List<Box> SelectedBoxes
		{
			get
			{
				List<Box> selectedBoxes = new List<Box>();

				foreach (Box box in Boxes)
				{
					if (box.IsSelected)
					{
						selectedBoxes.Add(box);
					}
				}

				return selectedBoxes;
			}
		}


		/// <summary>
		///     在一个BitMap对象中画上矩形
		/// </summary>
		/// <param name="image">需要画矩形的BitMap对象（如PictureBox.Image）</param>
		/// <returns>画毕的BitMap对象</returns>
		public void DisplayBoxes(Bitmap image)
		{
			foreach (Box box in Boxes)
			{
				//若当前矩形框被选中，使用红色，否则使用蓝色
				Color borderColor = box.IsSelected ? Color.Red : Color.Blue;
				//在传入的BitMap上画矩形
				Graphics.FromImage(image)
					.DrawRectangle(new Pen(borderColor, Convert.ToInt32(StringResourceManager.BoxBorderWidth)), box.X,
						box.Y, box.Width, box.Height);
			}
		}

		/// <summary>
		///     添加一个新的Box
		/// </summary>
		/// <param name="box"></param>
		public void Add(Box box)
		{
			//Boxes.Sort();
			Boxes.Add(box);
			//Boxes = Boxes.OrderBy(b => b.X.X).ToList();
		}

		/// <summary>
		///     删除某个Box
		/// </summary>
		/// <param name="box"></param>
		public void Delete(Box box)
		{
			Boxes.Remove(box);
		}

		/// <summary>
		///     删除指定下标的Box
		/// </summary>
		/// <param name="pos"></param>
		public void DeleteAt(int pos)
		{
			Boxes.RemoveAt(pos);
		}

		/// <summary>
		///     删除所有Box
		/// </summary>
		public void Clear()
		{
			Boxes.Clear();
		}

		/// <summary>
		///     将两个Box合并
		/// </summary>
		/// <param name="box"></param>
		/// <param name="anotherBox"></param>
		public void Merge(Box box, Box anotherBox)
		{
		}

		/// <summary>
		///     将一个Box拆分为两个Box
		/// </summary>
		/// <param name="box"></param>
		public void Split(Box box)
		{
		}

		/// <summary>
		///     从Box文件加载所有的Box数据
		/// </summary>
		/// <param name="filePath"></param>
		public void LoadFromBoxFile()
		{
			//清空之前所有的Box
			Boxes.Clear();
			StreamReader streamReader = null;

			try
			{
				streamReader = new StreamReader(BoxFilePath, Encoding.UTF8);
				while (!streamReader.EndOfStream)
				{
					//一次读取Box文件中的一行
					//注意，这里读取的Y坐标是BOX文件Y坐标
					string currentLine = streamReader.ReadLine();
					if (currentLine != null)
					{
						string[] items = currentLine.Split(' ');

						//字符、左上角点X坐标（与左边界距离），左上角点的Y坐标（与上边界距离）、左上角点的BOX文件Y坐标（高度减去Y）、宽度、高度
						//文件中的最后一个 0 省略（因不知道何用）
						Boxes.Add(new Box(items[0], Convert.ToInt32(items[1]),
							new Bitmap(ImageFilePath).Height - Convert.ToInt32(items[2]),
							Convert.ToInt32(items[3]),
							Convert.ToInt32(items[4])));
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
				streamWriter = new StreamWriter(BoxFilePath, false, Encoding.UTF8);

				foreach (Box box in Boxes)
				{
					//保存到文件中：字符，左上角点X坐标，左上角点.BOX Y坐标（图片高度减去离上边界的距离），宽度，高度
					string[] items =
					{
						box.Character, box.X.ToString(), (new Bitmap(ImageFilePath).Height - Convert.ToInt32(box.Y)).ToString(),
						box.Width.ToString(),
						box.Height.ToString(), fifthColumnValue
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
			if (!File.Exists(BoxFilePath))
			{
				File.Create(BoxFilePath);
			}
		}
	}
}