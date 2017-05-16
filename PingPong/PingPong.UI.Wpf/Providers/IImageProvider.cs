using System.Windows.Controls;

namespace PingPong.UI.Wpf.Helpers
{
    public interface IImageProvider
    {
        Image GetImageFromPath(string path, double imageWidth, double imageHeight);
    }
}
