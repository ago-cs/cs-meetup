using System;
using System.Linq;

namespace JpgToPdfConverter
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			if (args == null || args.Length < 2)
			{
				throw new InvalidOperationException(
					"At least two arguments expected:\n" +
					"\tsource file path and destination file path.");
			}

			var sourceFilePaths = args.SkipLast(1);
			var resultFilePath = args[^1];

			WriteLineColored(
				ConsoleColor.Yellow, 
				"JPG to PDF converter has started processing files...");

			var pdfGenerator = new Converter(
				sourceFilePaths,
				resultFilePath);

			pdfGenerator.ImageParsed += (sender, e) =>
			{
				Console.WriteLine(
					$"File \"{e.FilePath}\" parsed successfully:\n" +
					$"  Size in pixels: {e.WidthInPixels} × {e.HeightInPixels}\n" +
					$"  Size in millimeters: {e.WidthInMillimeters} × {e.HeightInMillimeters}\n" +
					$"  Resolution: {e.HorizontalResolution} × {e.VerticalResolution} dpi\n");
			};

			pdfGenerator.BuildPdf();

			WriteLineColored(ConsoleColor.Green, "Done!");
		}

		internal static void WriteLineColored(ConsoleColor color, string text)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(text);
			Console.ResetColor();
		}
	}
}
