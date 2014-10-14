using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFY_OCR.Untilities
{
	using System.Drawing;

	class Box
	{
		public Point TopLeftPoint { get; set; }
		public int Width { get; set; }

		public int Height { get; set; }

		public Box(Point topLeftPoint, int width, int height)
		{
			TopLeftPoint = new Point(topLeftPoint.X, topLeftPoint.Y);
			Width = width;
			Height = height;
		}
	}
}
