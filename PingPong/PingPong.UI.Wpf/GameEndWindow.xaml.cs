using System.Windows;

namespace PingPong.UI.Wpf
{
    /// <summary>
    /// Interaction logic for GameEndWindow.xaml
    /// </summary>
    public partial class GameEndWindow : Window
    {
        public GameEndWindow()
        {
            InitializeComponent();
        }

        public GameEndWindow(string message)
            : this()
        {
            this.MessageTB.Text = message;
        }
    }
}
