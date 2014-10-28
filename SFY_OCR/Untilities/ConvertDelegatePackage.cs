#region

using System.Collections;

#endregion

namespace SFY_OCR.Untilities
{
	/// <summary>
	///     用于保存在backgroundworker中异步调用的转换函数的包（包括一个指向效果函数的delegate和一个hashtable形式的参数列表
	/// </summary>
	internal class ConvertDelegatePackage
	{
		public ConvertDelegatePackage()
		{
			ConvertImageArgs = new Hashtable();
		}

		public ConvertImageDelegate ConvertImageDelegate { get; set; }

		public Hashtable ConvertImageArgs { get; set; }
	}
}