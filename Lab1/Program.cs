using System;
using System.Drawing;
using System.IO;

namespace Lab1
{
    public class Program
    {
        private static ImageProcessor ImageProcessor { get; set; }
        private static Bitmap Image { get; set; }
        private static string ImagePath { get; set; }

        private static void Main(string[] args)
        {
            ImageProcessor = new ImageProcessor();

            ShowWelcomeMessage();

            while(true)
            {
                var choice = ShowMenu();

                switch(choice)
                {
                    case 0:
                        ShowGoodbyeMessage();
                        Environment.Exit(0);
                        break;
                    case 1:
                        ShowChooseImageMenu();
                        ImageProcessor.BasicDemosaicing(Image);
                        SaveImage();
                        break;
                    case 2:
                        ShowChooseImageMenu();
                        ImageProcessor.EdgeBasedDemosaicing(Image);
                        SaveImage();
                        break;
                    case 3:
                        ShowChooseImageMenu();
                        ImageProcessor.GammaCorrection(Image);
                        SaveImage();
                        break;
                    case 4:
                        ShowChooseImageMenu();
                        ImageProcessor.FilterTheChromacity(Image);
                        SaveImage();
                        break;
                    default:
                        ShowErrorMessage();
                        break;
                }
            }
        }

        private static void ShowWelcomeMessage()
        {
            Console.WriteLine("Hello! This is a solution for Lab1.");
            Console.WriteLine();
        }

        private static int ShowMenu()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("0 - Exit");
            Console.WriteLine("1 - Basic Demosaicing");
            Console.WriteLine("2 - Edge-based Demosaicing");
            Console.WriteLine("3 - Gamma Correction");
            Console.WriteLine("4 - Filter the chromacity");

            var result = -1;
            int.TryParse(Console.ReadLine(), out result);

            Console.WriteLine();

            return result;
        }

        private static void ShowGoodbyeMessage()
        {
            Console.WriteLine("Thank you and see you soon!");
            Console.WriteLine("Press any key to close the program...");
            Console.ReadKey();
        }

        private static void ShowErrorMessage()
        {
            Console.WriteLine("Something has gone wrong. Please try again...");
            Console.WriteLine();
        }

        private static void ShowChooseImageMenu()
        {
            var result = string.Empty;

            Console.WriteLine("Please enter a path to the image you want to process...");
            var path = Console.ReadLine();

            try
            {
                Image = new Bitmap(path, false);
                ImagePath = path;
                Console.WriteLine();
            }
            catch(Exception)
            {
                ShowErrorMessage();
                ShowChooseImageMenu();
            }
        }

        private static void SaveImage()
        {
            var fileExtension = Path.GetExtension(ImagePath);
            var newFileName = Path.GetFileNameWithoutExtension(ImagePath) + " - output" + fileExtension;
            var newPath = Path.Combine(Path.GetDirectoryName(ImagePath), newFileName);

            Image.Save(newPath);
            Console.WriteLine("The image has been processed. You can now see the result...");
            Console.WriteLine();
        }
    }
}
