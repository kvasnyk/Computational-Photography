using System.Drawing;

namespace Helpers
{
    public static class ImageHelper
    {
        public static Color GetPixel(Bitmap image, int i, int j)
        {
            if (i > 0 && i < image.Width && j > 0 && j < image.Height)
                return image.GetPixel(i, j);

            return Color.Black;
        }
    }
}
