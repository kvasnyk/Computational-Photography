using System;
using System.Drawing;

namespace Lab1
{
    public class GammaCorrectionProcessor
    {
        private const float GAMMA = 2.2f;

        public void Process(Bitmap image)
        {
            var gammaCorrection = 1.0f / GAMMA;

            for(int i = 0; i < image.Width; i++)
            {
                for(int j = 0; j < image.Height; j++)
                {
                    var pixel = image.GetPixel(i, j);

                    var newR = 255 * Math.Pow(((float)pixel.R / 255.0f), gammaCorrection);
                    var newG = 255 * Math.Pow(((float)pixel.G / 255.0f), gammaCorrection);
                    var newB = 255 * Math.Pow(((float)pixel.B / 255.0f), gammaCorrection);

                    var newPixel = Color.FromArgb((int)newR, (int)newG, (int)newB);
                    image.SetPixel(i, j, newPixel);
                }
            }
        }
    }
}
