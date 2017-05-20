using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace PingPongGame.UI.WindowsUniversal.Providers
{
    public class ImageProvider : IImageProvider
    {
        public Image GetImageFromPath(string path, double imageWidth, double imageHeight)
        {
            BitmapImage bitmap = new BitmapImage()
            {
                UriSource = new Uri("ms-appx:///" + path, UriKind.RelativeOrAbsolute)
            };

            Image image = new Image()
            {
                Source = bitmap,
                Width = imageWidth,
                Height = imageHeight,
            };
            return image;
        }
    }
}
