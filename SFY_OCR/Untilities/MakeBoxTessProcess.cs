using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFY_OCR.Untilities
{
	class MakeBoxTessProcess : TessProcess
	{
		public MakeBoxTessProcess(Dictionary<string, string> args)
		{
			_arguments =
				string.Format(
					"{0} {1} batch.nochop makebox",
					args["sourceImagePath"], args["boxFilePath"]);
		}
	}
}
