using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Text;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace JpgToPdfConverter
{
	internal sealed class Converter
	{
		public string ResultFilePath { get; }

		public IReadOnlyCollection<string> ImagePaths { get; }

		public event EventHandler<ImageParsedEventArgs> ImageParsed;

		public Converter(
			IEnumerable<string> sourceFilePaths,
			string resultFilePath)
		{
			ResultFilePath = resultFilePath;
			ImagePaths = sourceFilePaths.ToImmutableList();
		}

		public void BuildPdf()
		{
			var document = new PdfDocument();

			foreach (var imagePath in ImagePaths)
				AddPageWithImage(document, imagePath);

			document.Save(ResultFilePath);
		}

		private void AddPageWithImage(PdfDocument document, string imagePath)
		{
			Size imageSize;
			float imageVerticalDpi;
			float imageHorizontalDpi;

			using (var image = Image.FromFile(imagePath))
			{
				imageSize = image.Size;
				imageVerticalDpi = image.VerticalResolution;
				imageHorizontalDpi = image.HorizontalResolution;
			}

			OnImageAdded(new ImageParsedEventArgs(
				imagePath,
				imageSize.Width,
				imageSize.Height,
				imageHorizontalDpi,
				imageVerticalDpi));

			var page = document.AddPage();

			page.Width = XUnit.FromMillimeter(imageSize.Width * _mmInInch / imageHorizontalDpi);
			page.Height = XUnit.FromMillimeter(imageSize.Height * _mmInInch / imageVerticalDpi);

			var gfx = XGraphics.FromPdfPage(page);
			var xImage = XImage.FromFile(imagePath);

			gfx.DrawImage(
				xImage,
				0,
				0,
				imageSize.Width * 72 / imageHorizontalDpi,
				imageSize.Height * 72 / imageHorizontalDpi);
		}

		private void OnImageAdded(ImageParsedEventArgs e)
		{
			ImageParsed?.Invoke(this, e);
		}
	}
}
