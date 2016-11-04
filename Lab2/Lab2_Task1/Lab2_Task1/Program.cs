using Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Lab2_Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            var directoryPath = Console.ReadLine();
            var fileNames = Directory.GetFiles(directoryPath);

            var images = new List<Bitmap>();
            foreach (var fileName in fileNames) images.Add(FileHelper.ReadImage(fileName));

            var width = images.First().Width;
            var height = images.First().Height;

            var onlyVerticalResult = new Bitmap(width, height);
            var onlyHorizontalResult = new Bitmap(width, height);
            var bothResult = new Bitmap(width, height);

            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    var maxVerticalGradient = -1;
                    var maxVerticalGradientImage = images.First();

                    var maxHorizontalGradient = -1;
                    var maxHorizontalGradientImage = images.First();

                    var maxBothGradient = -1;
                    var maxBothGradientImage = images.First();

                    foreach(var image in images)
                    {
                        var verticalGradient = Math.Abs(ImageHelper.GetPixel(image, i, j - 1).R - ImageHelper.GetPixel(image, i, j + 1).R);
                        if(verticalGradient >= maxVerticalGradient)
                        {
                            maxVerticalGradient = verticalGradient;
                            maxVerticalGradientImage = image;
                        }

                        var horizontalGradient = Math.Abs(ImageHelper.GetPixel(image, i - 1, j).R - ImageHelper.GetPixel(image, i + 1, j).R);
                        if(horizontalGradient >= maxHorizontalGradient)
                        {
                            maxHorizontalGradient = horizontalGradient;
                            maxHorizontalGradientImage = image;
                        }

                        var bothGradient = verticalGradient + horizontalGradient;
                        if(bothGradient > maxBothGradient)
                        {
                            maxBothGradient = bothGradient;
                            maxBothGradientImage = image;
                        }
                    }

                    var onlyVerticalResultPixel = ImageHelper.GetPixel(maxVerticalGradientImage, i, j);
                    onlyVerticalResult.SetPixel(i, j, onlyVerticalResultPixel);

                    var onlyHorizontalResultPixel = ImageHelper.GetPixel(maxHorizontalGradientImage, i, j);
                    onlyHorizontalResult.SetPixel(i, j, onlyHorizontalResultPixel);

                    var bothResultPixel = ImageHelper.GetPixel(maxBothGradientImage, i, j);
                    bothResult.SetPixel(i, j, bothResultPixel);
                }
            }

            var onlyVerticalPath = Path.Combine(directoryPath, "-result-only-vertical.jpg");
            FileHelper.SaveImage(onlyVerticalResult, onlyVerticalPath);

            var onlyHorizontalPath = Path.Combine(directoryPath, "-result-only-horizontal.jpg");
            FileHelper.SaveImage(onlyHorizontalResult, onlyHorizontalPath);

            var bothPath = Path.Combine(directoryPath, "-result-both.jpg");
            FileHelper.SaveImage(bothResult, bothPath);

            Console.WriteLine();
            Console.WriteLine("Completed!");
            Console.ReadKey();
        }
    }
}
