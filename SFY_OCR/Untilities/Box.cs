﻿using System;
using System.Drawing;

namespace SFY_OCR.Untilities
{
	public class Box
	{
		private bool _selected = false;
		public Box(string character, int x, int y, int width, int height)
		//构造函数
		{
			Character = character;
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public string Character { get; set; }
		//Box对应的字符

		public int X { get; set; }
		//Box的左上角点的X坐标（离左边界的距离）

		public int Y { get; set; }
		//Box的左上角点的Y坐标（离上边界的距离）

		public int Width { get; set; }
		//Box宽度

		public int Height { get; set; }
		//Box高度

		/// <summary>
		///     该Box是否被选中
		/// </summary>
		public bool Selected
		{
			get { return _selected; }
			set { _selected = value; OnSelectedChanged(new EventArgs()); }
		}

		/// <summary>
		///     当前Box是否包含了指定坐标的点
		/// </summary>
		/// <param name="x">要判断的点的x坐标</param>
		/// <param name="y">要判断的点的y坐标</param>
		/// <returns>true表示某点在当前box范围之内</returns>
		public bool Contains(int x, int y)
		{
			if (new Rectangle(X, Y, Width, Height).Contains(x, y))
			{
				return true;
			}

			return false;
		}

		public event EventHandler SelectedChanged;

		protected virtual void OnSelectedChanged(EventArgs eventArgs)
		{
			EventHandler handler = SelectedChanged;
			if (handler != null) handler(this, EventArgs.Empty);
		}
	}
}