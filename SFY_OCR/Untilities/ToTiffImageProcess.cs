using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFY_OCR.Untilities
{
	class ToTiffImageProcess : ImageProcess
	{
		public ToTiffImageProcess(Dictionary<string, string> args)
		{
			base._arguments = string.Format("-compress none {0} {1}", args["sourceImagePath"], args["destImagePath"]);
		}
	}
}
