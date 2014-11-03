#region

using System.Collections.Generic;

#endregion

namespace SFY_OCR.Untilities
{
	public class TextCleanerImageProcess : ImageProcess
	{
		public TextCleanerImageProcess(Dictionary<string, string> args)
		{
			_arguments = string.Format(
					"( {0} -colorspace gray -type grayscale -contrast-stretch 0 ) ( -clone 0 -colorspace gray -negate -lat {1}x{1}+{2}% -contrast-stretch 0 ) -compose copy_opacity -composite -fill \"{3}\" -opaque none +matte -deskew {4}% -sharpen 0x1  {5}",
					args["sourceImagePath"], args["filter_size"], args["off_set"], args["bgcolor"],
					args["deskew"], args["destImagePath"]);
		}
	}
}