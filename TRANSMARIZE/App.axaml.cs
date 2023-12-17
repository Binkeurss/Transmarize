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
using Microsoft.Win32;

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
                DataContext = new FeaturesViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainWindow
            {
                DataContext = new FeaturesViewModel()
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

    static SavedWordWindow savedWordWindow;
    public static SavedWordWindow SavedWordWindow
    {
        get
        {
            if (savedWordWindow == null)
            {
                savedWordWindow = new SavedWordWindow();
            }
            return savedWordWindow;
        }
    }

    static NaviFeaturesWindow naviFeaturesWindow;
    public static NaviFeaturesWindow NaviFeaturesWindow
    {
        get
        {
            if (naviFeaturesWindow == null)
            {
                naviFeaturesWindow = new NaviFeaturesWindow();
            }
            return naviFeaturesWindow;
        }
    }
    private void WordBookOpenClick(object? sender, System.EventArgs e)
    {
        SavedWordWindow.Show();
        if (SavedWordWindow.IsActive == false || SavedWordWindow.WindowState == Avalonia.Controls.WindowState.Minimized)
        {
            SavedWordWindow.Topmost = true;
            SavedWordWindow.WindowState = Avalonia.Controls.WindowState.Normal;
            SavedWordWindow.Activate();
            SavedWordWindow.Topmost = false;
        }
    }
    private void MainWindowOpenClick(object? sender, System.EventArgs e)
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow.Show();
        }
    }
    private void ExitButtonClick(object? sender, System.EventArgs e)
    {
        Environment.Exit(0);
    }
}
