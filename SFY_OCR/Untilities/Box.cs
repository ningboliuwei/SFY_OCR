#region

using System.Collections.Generic;
using System.Drawing;

#endregion

namespace SFY_OCR.Untilities
{
	internal class Box
	{
		//Box对应的字符
		public Box(string character, Point topLeftPoint, int width, int height)
		{
			Character = character;
			TopLeftPoint = new Point(topLeftPoint.X, topLeftPoint.Y);
			Width = width;
			Height = height;
		}

		public string Character { get; set; }
		//Box对象左上角的Point
		public Point TopLeftPoint { get; set; }
		//Box宽度
		public int Width { get; set; }
		//Box高度
		public int Height { get; set; }

		//构造函数

		/// <summary>
		///     该Box是否被选中
		/// </summary>
		public bool IsSelected { get; set; }

		/// <summary>
		///     当前Box是否包含了指定坐标的点
		/// </summary>
		/// <param name="x">要判断的点的x坐标</param>
		/// <param name="y">要判断的点的y坐标</param>
		/// <returns>true表示某点在当前box范围之内</returns>
		public bool Contains(int x, int y)
		{
			if (x >= TopLeftPoint.X && x < TopLeftPoint.X + Width && y >= TopLeftPoint.Y && y <= TopLeftPoint.Y + Height)
			{
				return true;
			}
			return false;
		}
	}
}