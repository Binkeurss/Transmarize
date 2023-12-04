using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia;
using System;
using SharpHook;
using SharpHook.Native;
using System.Threading.Tasks;
using Avalonia.Threading;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.Views;

public partial class MainWindow : Window
{
    public bool isRunning = false;

    // Khởi tạo các biến hook và giả lập sự kiện
    TaskPoolGlobalHook hook = new TaskPoolGlobalHook();
    EventSimulator simulator = new EventSimulator();

    string currentText = string.Empty;
    public MainWindow()
    {
        InitializeComponent();
        //Đăng kí sự kiện và chạy hook
        hook.MouseReleased += OnMouseRelease;
        hook.RunAsync();
    }

    public void OnMouseRelease(object sender, MouseHookEventArgs e)
    {
        Dispatcher.UIThread.Post(async () =>
        {
            //Có delay task để xử lý delay trong Word
            await Task.Delay(75);
            // Press Ctrl + C
            simulator.SimulateKeyPress(KeyCode.VcLeftControl);
            simulator.SimulateKeyPress(KeyCode.VcC);
            // Release 
            simulator.SimulateKeyRelease(KeyCode.VcC);
            simulator.SimulateKeyRelease(KeyCode.VcLeftControl);
            await Task.Delay(75);

            // Lấy giá trị text đang có trong Clipboard
            string text = await Clipboard.GetTextAsync();

            if (text is null || text == " ")
            {
                return;
            }

            ShareData.transText = text;
            // Mở popup window tại vị trí con chuột đang đứng
            PopWindow popup = new PopWindow();
            popup.Position = new Avalonia.PixelPoint(e.Data.X, e.Data.Y);
            popup.WindowStartupLocation = WindowStartupLocation.Manual;
            popup.Show();
        });
    }

    private void ToggleButtonClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Button button = (Button)sender;
        isRunning = !isRunning;
        button.Content = isRunning ? "STOP" : "START";
    }

    //Exit button and Close window
    public void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        //Environment.Exit(0);
        hook.Dispose();
        this.Close();
    }

    public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }


}
