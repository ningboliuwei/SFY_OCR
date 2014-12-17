using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFY_OCR.Untilities
{
	class RecognitionTessProcess : TessProcess
	{
		public RecognitionTessProcess(Dictionary<string, string> args)
		{
			_arguments =
				string.Format(
					"{0} {1} -l {2} ",
					args["sourceImagePath"], args["resultFilePath"], args["langType"]);
		}
	}
}
