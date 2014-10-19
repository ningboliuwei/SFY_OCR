#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

#endregion

namespace SFY_OCR.Untilities
{
	internal class BoxList
	{
		// 所有的Box对象
		/// <summary>
		///     构造函数，采用聚合方式拥有List<Box>对象
		/// </summary>
		public BoxList(string imageFilePath)
		{
			Boxes = new List<Box>();
			ImageFilePath = imageFilePath;
		}

		/// <summary>
		///     该Box列表对应的图片文件的路径
		/// </summary>
		public string ImageFilePath { get; set; }

		public List<Box> Boxes { get; set; }

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
		public void Display(Bitmap image)
		{
			foreach (Box box in Boxes)
			{
				Color borderColor;
				//若当前矩形框被选中
				if (box.IsSelected)
				{
					borderColor = Color.Blue;
				}
				else
				{
					borderColor = Color.Red;
				}
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
		///     删除某个Box对象
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
		public void LoadFromFile(string filePath)
		{
			//清空之前所有的Box
			Boxes.Clear();
			StreamReader streamReader = null;

			try
			{
				streamReader = new StreamReader(filePath, Encoding.UTF8);
				while (!streamReader.EndOfStream)
				{
					//一次读取Box文件中的一行
					//注意，这里读取的Y坐标是BOX文件Y坐标
					string[] items = streamReader.ReadLine().Split(' ');

					//字符、左上角点X坐标（与左边界距离），左上角点的Y坐标（与上边界距离）、左上角点的BOX文件Y坐标（高度减去Y）、宽度、高度
					Boxes.Add(new Box(items[0], Convert.ToInt32(items[1]), new Bitmap(ImageFilePath).Height - Convert.ToInt32(items[2]),
						Convert.ToInt32(items[2]),
						Convert.ToInt32(items[3]),
						Convert.ToInt32(items[4])));
				}
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message);
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
		public void SaveToFile(string filePath)
		{
			StreamWriter streamWriter = null;
			try
			{
				streamWriter = new StreamWriter(filePath, false, Encoding.UTF8);

				foreach (Box box in Boxes)
				{
					//保存到文件中：字符，左上角点X坐标，左上角点.BOX Y坐标（图片高度减去离上边界的距离），宽度，高度
					string[] items =
					{
						box.Character, box.X.ToString(), box.BoxFileY.ToString(), box.Width.ToString(),
						box.Height.ToString(), "0"
					};

					streamWriter.WriteLine(string.Join(" ", items));
				}
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message);
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
		/// 若图片对应的Box文件（.box）不存在，则根据路径创建
		/// </summary>
		/// <param name="filePath"></param>
		public void CreateFile(string filePath)
		{
			
			if (!File.Exists(filePath))
			{
				File.Create(filePath);
			}
		}
	}
}