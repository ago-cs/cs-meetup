using System;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace JpgToPdfConverter
{
	class Program
	{
		private static float _mmInInch = 25.4F;

		private static string _outputPath = @"d:\test\result.pdf";

		private static int _inputResolutionDpi = 300;
		private static int _inputHeightPx = 3120;
		private static int _inputWidthPx = 2120;
		private static string[] _inputPaths =
		{
			@"d:\test\image1.jpg",
			@"d:\test\image2.jpg"
		};

		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var document = new PdfDocument();

			foreach (var imagePath in _inputPaths)
			{
				var page = document.AddPage();

				page.Width = XUnit.FromMillimeter(_inputWidthPx * _mmInInch / _inputResolutionDpi);
				page.Height = XUnit.FromMillimeter(_inputHeightPx * _mmInInch / _inputResolutionDpi);

				var gfx = XGraphics.FromPdfPage(page);
				var xImage = XImage.FromFile(imagePath);

				gfx.DrawImage(
					xImage,
					0,
					0,
					(float)_inputWidthPx * 72 / _inputResolutionDpi,
					(float)_inputHeightPx * 72 / _inputResolutionDpi);
			}

			document.Save(_outputPath);
		}
	}
}
