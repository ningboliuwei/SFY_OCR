namespace SFY_OCR.Untilities
{
	public class OcrImage
	{
		public OcrImage(string filePath)
		{
			FileName = filePath;
			FileName = filePath.Substring(filePath.LastIndexOf("\\", System.StringComparison.Ordinal) + 1);
			MainFileName = FileName.Substring(0, FileName.LastIndexOf(".", System.StringComparison.Ordinal));
			ExtFileName = FileName.Substring(FileName.LastIndexOf(".", System.StringComparison.Ordinal) + 1);
		}

		public string FilePath { get; private set; }

		public string MainFileName { get; private set; }

		public string FileName { get; private set; }

		public string ExtFileName { get; private set; }
	}
}