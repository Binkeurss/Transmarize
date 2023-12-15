using Avalonia.Controls;
using SharpHook;
using SharpHook.Native;
using System.Threading.Tasks;
using Avalonia.Threading;
using TRANSMARIZE.Model;
using TRANSMARIZE.ViewModels;
using System.Collections.Generic;
using System;

namespace TRANSMARIZE.Views;

public partial class MainWindow : Window
{
    // Khởi tạo các biến hook và giả lập sự kiện
    public static TaskPoolGlobalHook hook = new TaskPoolGlobalHook();
    EventSimulator simulator = new EventSimulator();

    // currentText dùng để tránh popup window mở sai thời điểm
    // string currentText = string.Empty; // note: đã được chuyển sang ShareData
    public MainWindow()
    {
        InitializeComponent();
        Clipboard.ClearAsync();
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

            if (text == "" || text == String.Empty || text is null || text == " " || text == ShareData.currentText)
            {
                return;
            }

            ShareData.currentText = text;
            // gán text cho transText để đưa đi dịch
            ShareData.transText = text;
            // Mở popup window tại vị trí con chuột đang đứng
            PopWindow popWindow = new PopWindow();
            // Gán DataContext của MainWindow cho PopWindow
            popWindow.DataContext = this.DataContext;
            popWindow.Position = new Avalonia.PixelPoint(e.Data.X, e.Data.Y);
            popWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            popWindow.Show();
        });
    }

    private void RunHookButton(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Button button = (Button)sender;
        if (hook.IsRunning == false)
        {
            // Đăng kí sự kiện và chạy hook
            hook.MouseReleased += OnMouseRelease;
            hook.RunAsync();
            button.Content = "STOP";
            this.Hide();
        }
        else
        {
            hook.Dispose();
            hook = new TaskPoolGlobalHook();
            button.Content = "START";
        }
    }
    private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        ComboBox comboBox = sender as ComboBox;
        string langSelected = comboBox.SelectedItem.ToString();
        if (langSelected != null)
        {
            ShareData.langSecond = ShareData.languageDictionary[langSelected];
        }
    }

    //Exit button and Close window
    public void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        hook.Dispose();
        Environment.Exit(0);
    }

    public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        this.Hide();
    }

    private void OpenBookButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        App.SavedWordWindow.Show();
        if (App.SavedWordWindow.IsActive == false)
        {
            App.SavedWordWindow.Topmost = true;
            App.SavedWordWindow.Activate();
            App.SavedWordWindow.Topmost = false;
        }
    }
}
