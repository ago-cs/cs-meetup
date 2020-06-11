using System;

namespace JpgToPdfConverter
{
	public class ImageParsedEventArgs : EventArgs
	{
		public string FilePath { get; set; }

		public int WidthInPixels { get; set; }

		public int HeightInPixels { get; set; }

		public float HorizontalResolution { get; set; }

		public float VerticalResolution { get; set; }

		public float WidthInMillimeters => 
			WidthInPixels * SharedData.MillimetersInInch / HorizontalResolution;

		public float HeightInMillimeters => 
			HeightInPixels * SharedData.MillimetersInInch / VerticalResolution;

		public ImageParsedEventArgs(
			string filePath,
			int widthInPixels,
			int heightInPixels,
			float horizontalResolution,
			float verticalResolution)
		{
			FilePath = filePath;
			WidthInPixels = widthInPixels;
			HeightInPixels = heightInPixels;
			HorizontalResolution = horizontalResolution;
			VerticalResolution = verticalResolution;
		}
	}
}
