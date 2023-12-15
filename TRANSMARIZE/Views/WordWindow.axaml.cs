using Avalonia.Controls;
using Avalonia.Threading;
using SharpHook;
using System.Diagnostics;
using TRANSMARIZE.Model;
using System.Threading.Tasks;
using Avalonia;


namespace TRANSMARIZE.Views
{
    public partial class WordWindow : Window
    {
        public WordWindow()
        {
            InitializeComponent();
            MainWindow.hook.MouseClicked += OnMouseClicked;
        }

        // Khi click ra ngoai window thi auto close window
        public void OnMouseClicked(object sender, MouseHookEventArgs e)
        {
            Dispatcher.UIThread.Post(async () =>
            {
                var mousePosition = new Avalonia.PixelPoint(e.Data.X, e.Data.Y);
                var windowPosition = this.Position;
                var x = this.Bounds.BottomRight;
                PixelPoint bottomRight = Avalonia.VisualExtensions.PointToScreen(this, x);

                if (!(mousePosition.X >= windowPosition.X && mousePosition.X <= bottomRight.X &&
                    mousePosition.Y >= windowPosition.Y && mousePosition.Y <= bottomRight.Y))
                {
  /*                if (Clipboard != null)
                    {
                        await Clipboard.ClearAsync();
                    }*/
                    this.Close();
                    // chờ 1 giây sau đó chuyển currentText = empty để có thể bôi 2 từ giống nhau liên tiếp
/*                  await Task.Delay(1000);
                    ShareData.currentText = string.Empty;*/
                }
            });
        }

        public async void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // delay 1 tí để chương trình Ctrl C rồi mới tắt, tránh bug lặp PopWindow
            await Task.Delay(100);
            this.Close();
        }

        public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_Closed(object? sender, System.EventArgs e)
        {
            // khi close Window thì Unsubcribe hook
            MainWindow.hook.MouseClicked -= OnMouseClicked;
        }
    }
}
