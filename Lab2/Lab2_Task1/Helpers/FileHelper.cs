using System.Drawing;
using System.IO;

namespace Helpers
{
    public static class FileHelper
    {
        public static Bitmap ReadImage(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var image = new Bitmap(fileStream);
                return image.Clone() as Bitmap;
            }
        }

        public static void SaveImage(Bitmap image, string filePath)
        {
            if (File.Exists(filePath)) File.Delete(filePath);
            image.Save(filePath);
        }
    }
}
