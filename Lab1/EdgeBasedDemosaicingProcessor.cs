using System;
using System.Drawing;

namespace Lab1
{
    public class EdgeBasedDemosaicingProcessor
    {
        public void Process(Bitmap image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if (IsGreen(i, j))
                    {
                        ComputeRed(image, i, j);
                        ComputeBlue(image, i, j);
                    }
                    else if (IsRed(i, j))
                    {
                        ComputeGreen(image, i, j);
                        ComputeBlue(image, i, j);
                    }
                    else if (IsBlue(i, j))
                    {
                        ComputeGreen(image, i, j);
                        ComputeRed(image, i, j);
                    }
                }
            }
        }

        private bool IsGreen(int i, int j)
        {
            return !IsRed(i, j) && !IsBlue(i, j);
        }

        private bool IsRed(int i, int j)
        {
            return (i % 2 == 0 && j % 2 == 0);
        }

        private bool IsBlue(int i, int j)
        {
            return (i % 2 == 1 && j % 2 == 1);
        }

        private void ComputeGreen(Bitmap image, int i, int j)
        {
            var oldPixel = GetPixel(image, i, j);
            var newG = (int)oldPixel.G;

            var hg = Math.Abs(GetPixel(image, i - 1, j).G - GetPixel(image, i + 1, j).G);
            var vg = Math.Abs(GetPixel(image, i, j - 1).G - GetPixel(image, i, j + 1).G);

            if (hg > vg) newG = (GetPixel(image, i, j - 1).G + GetPixel(image, i, j + 1).G) / 2;
            else if (hg < vg) newG = (GetPixel(image, i, j - 1).G + GetPixel(image, i + 1, j).G) / 2;
            else newG = (GetPixel(image, i - 1, j).G + GetPixel(image, i + 1, j).G + GetPixel(image, i, j - 1).G + GetPixel(image, i, j + 1).G) / 4;

            var newPixel = Color.FromArgb(oldPixel.R, newG, oldPixel.B);
            image.SetPixel(i, j, newPixel);
        }

        private void ComputeRed(Bitmap image, int i, int j)
        {
            var oldPixel = GetPixel(image, i, j);
            var newR = (int)oldPixel.R;

            if (IsRed(i - 1, j)) newR = (GetPixel(image, i - 1, j).R + GetPixel(image, i + 1, j).R) / 2;
            else if (IsRed(i - 1, j - 1)) newR = (GetPixel(image, i - 1, j - 1).R + GetPixel(image, i + 1, j - 1).R + GetPixel(image, i - 1, j + 1).R + GetPixel(image, i + 1, j + 1).R) / 4;
            else if (IsRed(i, j - 1)) newR = (GetPixel(image, i, j - 1).R + GetPixel(image, i, j + 1).R) / 2;

            var newPixel = Color.FromArgb(newR, oldPixel.G, oldPixel.B);
            image.SetPixel(i, j, newPixel);
        }

        private void ComputeBlue(Bitmap image, int i, int j)
        {
            var oldPixel = GetPixel(image, i, j);
            var newB = (int)oldPixel.B;

            if (IsBlue(i - 1, j)) newB = (GetPixel(image, i - 1, j).B + GetPixel(image, i + 1, j).B) / 2;
            else if (IsBlue(i - 1, j - 1)) newB = (GetPixel(image, i - 1, j - 1).B + GetPixel(image, i + 1, j - 1).B + GetPixel(image, i - 1, j + 1).B + GetPixel(image, i + 1, j + 1).B) / 4;
            else if (IsBlue(i, j - 1)) newB = (GetPixel(image, i, j - 1).B + GetPixel(image, i, j + 1).B) / 2;

            var newPixel = Color.FromArgb(oldPixel.R, oldPixel.G, newB);
            image.SetPixel(i, j, newPixel);
        }

        private Color GetPixel(Bitmap image, int i, int j)
        {
            if (i > 0 && i < image.Width && j > 0 && j < image.Height)
                return image.GetPixel(i, j);
            return Color.FromArgb(0, 0, 0);
        }
    }
}
