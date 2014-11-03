#region

using System.Collections.Generic;

#endregion

namespace SFY_OCR.Untilities
{
	internal class RotateImageProcess : ImageProcess
	{
		public RotateImageProcess(Dictionary<string, string> args)
		{
			_arguments =
				string.Format(
					"-rotate {0} {1} {2} ",
					args["degree"], args["sourceImagePath"], args["destImagePath"]);
		}
	}
}