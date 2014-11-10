using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFY_OCR.Untilities
{
	class CustomCommandLineProcess : CommandLineProcess
	{
		public CustomCommandLineProcess(Dictionary<string, string> args)
		{
			_commandPath = args["commandPath"];
			_arguments = args["arguments"];
			if (args.ContainsKey("workingDirectory"))
			{
				_workingDirectory = args["workingDirectory"];
			}
		}
	}
}
