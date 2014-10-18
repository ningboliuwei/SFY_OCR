#region

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

#endregion

namespace SFY_OCR.Untilities
{
	internal class BoxList
	{
		// 所有的Box对象

		/// <summary>
		/// 构造函数，采用聚合方式拥有List<Box>对象
		/// </summary>
		public BoxList()
		{
			Boxes = new List<Box>();
		}

		public List<Box> Boxes { get; set; }

		/// <summary>
		/// 获得BoxList中所有被选中的Box对象
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
		/// 在一个BitMap对象中画上矩形
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
					.DrawRectangle(new Pen(borderColor, Convert.ToInt32(StringResourceManager.BoxBorderWidth)), box.TopLeftPoint.X,
						box.TopLeftPoint.Y, box.Width, box.Height);
			}
		}

		/// <summary>
		/// 添加一个新的Box
		/// </summary>
		/// <param name="box"></param>
		public void Add(Box box)
		{
			//Boxes.Sort();
			Boxes.Add(box);
			Boxes = Boxes.OrderBy(b => b.TopLeftPoint.X).ToList();
		}

		/// <summary>
		/// 删除某个Box对象
		/// </summary>
		/// <param name="box"></param>
		public void Delete(Box box)
		{
			Boxes.Remove(box);
		}

		/// <summary>
		/// 删除指定下标的Box
		/// </summary>
		/// <param name="pos"></param>
		public void DeleteAt(int pos)
		{
			Boxes.RemoveAt(pos);
		}

		/// <summary>
		/// 删除所有Box
		/// </summary>
		public void Clear()
		{
			Boxes.Clear();
		}

		/// <summary>
		/// 将两个Box合并
		/// </summary>
		/// <param name="box"></param>
		/// <param name="anotherBox"></param>
		public void Merge(Box box, Box anotherBox)
		{
		}

		/// <summary>
		/// 将一个Box拆分为两个Box
		/// </summary>
		/// <param name="box"></param>
		public void Split(Box box)
		{
		}

		public void LoadFromFile(string filePath)
		{
			//清空之前所有的Box
			Boxes.Clear();

			try
			{
				StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8);
				while (!streamReader.EndOfStream)
				{
					//一次读取Box文件中的一行
					string[] items = streamReader.ReadLine().Split(' ');

					//字符、左上角点X坐标（与左边界距离），左上角点的Y坐标（与上边界距离）、宽度、高度
					Boxes.Add(new Box(items[0], new Point(Convert.ToInt32(items[1]), Convert.ToInt32(items[2])),
						Convert.ToInt32(items[3]),
						Convert.ToInt32(items[4])));
				}
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message);
			}
		}
	}
}