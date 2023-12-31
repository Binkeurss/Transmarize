using Avalonia.Controls;
using SharpHook;
using SharpHook.Native;
using System.Threading.Tasks;
using Avalonia.Threading;
using TRANSMARIZE.Model;
using TRANSMARIZE.ViewModels;
using System.Collections.Generic;
using System;
using NetSparkleUpdater.SignatureVerifiers;
using NetSparkleUpdater.UI.Avalonia;
using NetSparkleUpdater;
using NetSparkleUpdater.Enums;
using Avalonia.Media;

namespace TRANSMARIZE.Views;

public partial class MainWindow : Window
{
    // Khởi tạo các biến hook và giả lập sự kiện
    public static TaskPoolGlobalHook hook = new TaskPoolGlobalHook();
    EventSimulator simulator = new EventSimulator();
    private SparkleUpdater _sparkle;

    // currentText dùng để tránh popup window mở sai thời điểm
    // string currentText = string.Empty; // note: đã được chuyển sang ShareData
    public MainWindow()
    {
        InitializeComponent();
        Clipboard.ClearAsync();

        string manifestModuleName = System.Reflection.Assembly.GetEntryAssembly().ManifestModule.FullyQualifiedName;

        _sparkle = new SparkleUpdater("https://ngyyen.github.io/AppCast.xml", // link to your app cast file
                                       new Ed25519Checker(SecurityMode.Unsafe) )
        {
            UIFactory = new NetSparkleUpdater.UI.Avalonia.UIFactory(Icon)
            {
                UpdateWindowGridBackgroundBrush = new SolidColorBrush(Colors.Purple)
            },
            RelaunchAfterUpdate = true, // default is false; set to true if you want your app to restart after updating (keep as false if your installer will start your app for you)
            ShowsUIOnMainThread = true, // required on Avalonia, preferred on WPF/WinForms
        };
        //_sparkle.SecurityProtocolType = System.Net.SecurityProtocolType.Tls12;
        _sparkle.StartLoop(true, true); // `true` to run an initial check online -- only call StartLoop once for a given SparkleUpdater instance!

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
    private void OpenBookButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        App.SavedWordWindow.Show();
        if (App.SavedWordWindow.IsActive == false || App.SavedWordWindow.WindowState == WindowState.Minimized)
        {
            App.SavedWordWindow.Topmost = true;
            App.SavedWordWindow.WindowState = WindowState.Normal;
            App.SavedWordWindow.Activate();
            App.SavedWordWindow.Topmost = false;
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

    private async void CheckUpdateButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        await _sparkle.CheckForUpdatesAtUserRequest();
    }
}
