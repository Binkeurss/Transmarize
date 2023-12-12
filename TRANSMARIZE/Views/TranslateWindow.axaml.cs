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
using System.Threading.Tasks;

namespace TRANSMARIZE.Views
{
    public partial class TranslateWindow : Window
    {
        public string input = String.Empty;
        public TranslateWindow()
        {
            InitializeComponent();
            input = ConvertString(ShareData.transText);
            SourceText.Text = input;
            TransText.Text = ShareData.TranslateText(input, "auto", ShareData.langSecond);
        }

        public void TransButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            TransText.Text = ShareData.TranslateText(input, "auto", ShareData.langSecond);
        }

        //Bỏ đi những dấu xuống dòng tránh bị lỗi
        string ConvertString(string input)
        {
            string convertInput = input.Replace(System.Environment.NewLine, "");
            return convertInput;
        }

        public async void ExitButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.Close();
            await Task.Delay(1000);
            ShareData.currentText = string.Empty;
        }

        public void HideButton_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
