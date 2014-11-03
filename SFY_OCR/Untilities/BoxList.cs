#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace SFY_OCR.Untilities
{
	public class BoxList
	{
		/// <summary>
		///     构造函数，采用聚合方式拥有List<Box>对象
		/// </summary>
		public BoxList()
		{
			Boxes = new List<Box>();
		}

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
					if (box.Selected)
					{
						selectedBoxes.Add(box);
					}
				}

				return selectedBoxes;
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
		public void Remove(Box box)
		{
			Boxes.Remove(box);
		}

		/// <summary>
		///     删除指定下标的Box
		/// </summary>
		/// <param name="pos"></param>
		public void RemoveAt(int pos)
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
		/// 根据坐标系得到Box对象
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public Box GetBoxByCoordinate(int x, int y, int width, int height)
		{
			return Boxes.FirstOrDefault(box => box.X == x && box.Y == y && box.Width == width && box.Height == height);
		}
		/// <summary>
		/// 根据编号得到Box对象
		/// </summary>
		/// <param name="sn"></param>
		/// <returns></returns>
		public Box GetBoxBySn(int sn)
		{
			return Boxes.FirstOrDefault(box => box.Sn == sn);
		}
		/// <summary>
		/// 清除所有Box对象的选中状态
		/// </summary>
		public void UnSelectAll()
		{
			foreach (Box box in Boxes)
			{
				box.Selected = false;
			}
		}
	}
}