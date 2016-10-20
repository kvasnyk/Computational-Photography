using System;
using System.Collections.Generic;
using System.Drawing;

namespace Lab1
{
    public class FilterTheChromacityProcessor
    {
        private readonly int WINDOW = 10;

        private GammaCorrectionProcessor GammaCorrectionProcessor { get; set; }

        private class YCrCbImage
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public int[,] Y { get; set; }
            public int[,] Cr { get; set; }
            public int[,] Cb { get; set; }
        }

        public FilterTheChromacityProcessor()
        {
            GammaCorrectionProcessor = new GammaCorrectionProcessor();
        }

        public void Process(Bitmap image)
        {
            var yCrCbImage = ConvertToYCrCbImage(image);
            FilterChromacity(yCrCbImage);
            ConvertToBitmap(yCrCbImage, image);
            GammaCorrectionProcessor.Process(image);
        }

        private YCrCbImage ConvertToYCrCbImage(Bitmap image)
        {
            var result = new YCrCbImage();
            result.Width = image.Width;
            result.Height = image.Height;
            result.Y = new int[image.Width, image.Height];
            result.Cr = new int[image.Width, image.Height];
            result.Cb = new int[image.Width, image.Height];

            for(int i = 0; i < image.Width; i++)
            {
                for(int j = 0; j < image.Height; j++)
                {
                    var pixel = image.GetPixel(i, j);
                    result.Y[i, j] = (int)(0.299f * pixel.R + 0.587f * pixel.G + 0.114f * pixel.B);
                    result.Cr[i, j] = (int)(128.0f + 0.5f * pixel.R - 0.418688f * pixel.G - 0.081312f * pixel.B);
                    result.Cb[i, j] = (int)(128.0f - 0.168736 * pixel.R - 0.331264f * pixel.G + 0.5f * pixel.B);
                }
            }

            return result;
        }

        private void ConvertToBitmap(YCrCbImage yCrCbImage, Bitmap result)
        {
            for(int i = 0; i < result.Width; i++)
            {
                for(int j = 0; j < result.Height; j++)
                {
                    var newR = (int)(yCrCbImage.Y[i, j] + 1.402f * (yCrCbImage.Cr[i, j] - 128.0f));
                    var newG = (int)(yCrCbImage.Y[i, j] - 0.34414f * (yCrCbImage.Cb[i, j] - 128.0f) - 0.71414f * (yCrCbImage.Cr[i, j] - 128.0f));
                    var newB = (int)(yCrCbImage.Y[i, j] + 1.772f * (yCrCbImage.Cb[i, j] - 128.0f));

                    newR = Math.Max(0, newR);
                    newG = Math.Max(0, newG);
                    newB = Math.Max(0, newB);

                    newR = Math.Min(255, newR);
                    newG = Math.Min(255, newG);
                    newB = Math.Min(255, newB);

                    var newPixel = Color.FromArgb(newR, newG, newB);
                    result.SetPixel(i, j, newPixel);
                }
            }
        }

        private void FilterChromacity(YCrCbImage yCrCbImage)
        {
            for(int i = 0; i < yCrCbImage.Width; i++)
            {
                for(int j = 0; j < yCrCbImage.Height; j++)
                {
                    var crs = new List<int>();
                    var cbs = new List<int>();

                    for (int x = i - WINDOW; x <= i + WINDOW; x++)
                    {
                        for (int y = j - WINDOW; y <= j + WINDOW; y++)
                        {
                            if (x >= 0 && x < yCrCbImage.Width && y >= 0 && y < yCrCbImage.Height)
                            {
                                crs.Add(yCrCbImage.Cr[x, y]);
                                cbs.Add(yCrCbImage.Cb[x, y]);
                            }
                        }
                    }                    

                    yCrCbImage.Cr[i, j] = Median(crs);
                    yCrCbImage.Cb[i, j] = Median(cbs);
                }
            }
        }

        private int Median(List<int> list)
        {
            list.Sort();

            if(list.Count % 2 == 1)
            {
                var center = list.Count / 2;
                var result = list[center];
                return result;
            }
            else
            {
                var center1 = list.Count / 2 - 1;
                var center2 = list.Count / 2;
                var result = (int)(((float)list[center1] + (float)list[center2]) / 2.0f);
                return result;
            }
        }
    }
}
