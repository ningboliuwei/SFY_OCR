#region

using System.Collections.Generic;

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
					if (box.IsSelected)
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
	}
}