using System.Drawing;

namespace Lab1
{
    public class ImageProcessor
    {
        private BasicDemosaicingProcessor BasicDemosaicingProcessor { get; set; }
        private EdgeBasedDemosaicingProcessor EdgeBasedDemosaicingProcessor { get; set; }
        private GammaCorrectionProcessor GammaCorrectionProcessor { get; set; }
        private FilterTheChromacityProcessor FilterTheChromacityProcessor { get; set; }

        public ImageProcessor()
        {
            BasicDemosaicingProcessor = new BasicDemosaicingProcessor();
            EdgeBasedDemosaicingProcessor = new EdgeBasedDemosaicingProcessor();
            GammaCorrectionProcessor = new GammaCorrectionProcessor();
            FilterTheChromacityProcessor = new FilterTheChromacityProcessor();
        }

        public void BasicDemosaicing(Bitmap image)
        {
            BasicDemosaicingProcessor.Process(image);
        }

        public void EdgeBasedDemosaicing(Bitmap image)
        {
            EdgeBasedDemosaicingProcessor.Process(image);
        }

        public void GammaCorrection(Bitmap image)
        {
            GammaCorrectionProcessor.Process(image);
        }

        public void FilterTheChromacity(Bitmap image)
        {
            FilterTheChromacityProcessor.Process(image);
        }
    }
}
