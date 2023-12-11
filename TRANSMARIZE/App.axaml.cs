using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using TRANSMARIZE.Model;
using TRANSMARIZE.ViewModels;
using TRANSMARIZE.Views;
using TRANSMARIZE.Services;
using System.IO;
using System;

namespace TRANSMARIZE;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new ShareData()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }
   
        base.OnFrameworkInitializationCompleted();
    }
    // Khai báo biến database, biến này được sử dụng xuyên suốt App
    static WordBookService wordBookDatabase;
    public static WordBookService WordBookDatabase
    {
        get
        {
            // Khi gọi hàm get, nếu database chưa được tạo thì tiến hành tạo
            if (wordBookDatabase == null)
            {
                wordBookDatabase = new WordBookService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WordBookDatabase3.db3"));
            }
            return wordBookDatabase;
        }
    }
    private void MainWindowOpenClick(object? sender, System.EventArgs e)
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow.Show();
        }
    }

    static SavedWordWindow savedWordWindow;
    public static SavedWordWindow SavedWordWindow
    {
        get
        {
            if (savedWordWindow == null)
            {
                savedWordWindow = new SavedWordWindow();
                return savedWordWindow;
            }
            if (savedWordWindow.IsVisible == false)
            {
                savedWordWindow = new SavedWordWindow();
                return savedWordWindow;
            }
            return savedWordWindow;
        }
    }
    private void WordBookOpenClick(object? sender, System.EventArgs e)
    {
        SavedWordWindow.Show();
        SavedWordWindow.Activate();
    }
}
