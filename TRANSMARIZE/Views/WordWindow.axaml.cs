using Avalonia.Controls;
using Avalonia.Threading;
using SharpHook;
using System.Diagnostics;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.Views
{
    public partial class WordWindow : Window
    {
        public WordWindow()
        {
            InitializeComponent();
            MainWindow.hook.MouseClicked += OnMouseClicked;
        }
        public void OnMouseClicked(object sender, MouseHookEventArgs e)
        {
            Dispatcher.UIThread.Post(() =>
            {
                var mousePosition = new Avalonia.PixelPoint(e.Data.X, e.Data.Y);
                var windowPosition = this.Position;
                if (!(mousePosition.X >= windowPosition.X && mousePosition.X <= windowPosition.X + 751 &&
                    mousePosition.Y >= windowPosition.Y && mousePosition.Y <= windowPosition.Y + 897))
                {
                    MainWindow.hook.MouseClicked -= OnMouseClicked;
                    this.Close();
                }
            });
        }

        public void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //Environment.Exit(0)
            this.Close();
        }
        public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
