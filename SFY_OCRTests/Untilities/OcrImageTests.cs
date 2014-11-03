using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFY_OCR.Untilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SFY_OCR.Untilities.Tests
{
	[TestClass()]
	public class OcrImageTests
	{
		[TestMethod()]
		public void LoadFromBoxFileTest()
		{
			OcrImage ocrImage = new OcrImage("R:\\PIC_20141018_085857_4C8.JPG");
			ocrImage.LoadFromBoxFile();
			Assert.AreNotEqual(ocrImage, null);
		}
	}
}
