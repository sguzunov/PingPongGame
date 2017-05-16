using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PingPong.UI.Wpf.Helpers
{
    public class ImageProvider : IImageProvider
    {
        public Image GetImageFromPath(string path, double imageWidth, double imageHeight)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();

            Image image = new Image();
            image.Source = bitmap;
            image.Width = imageWidth;
            image.Height = imageHeight;
            
            return image;
        }
    }
}
