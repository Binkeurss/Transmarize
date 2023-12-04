using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia;
using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using TRANSMARIZE.Model;

namespace TRANSMARIZE.Views
{
    public partial class TranslateWindow : Window
    {
        private HttpClient httpClient;
        public TranslateWindow()
        {
            InitializeComponent();

        }



        public void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //Environment.Exit(0);
            this.Close();
        }

        public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
