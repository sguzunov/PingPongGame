using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace PingPongGame.UI.WindowsUniversal.Providers
{
    public interface IImageProvider
    {
        Image GetImageFromPath(string path, double imageWidth, double imageHeight);
    }
}
