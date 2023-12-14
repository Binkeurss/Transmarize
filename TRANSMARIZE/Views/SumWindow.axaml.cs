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
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Threading.Tasks;

namespace TRANSMARIZE.Views
{
    public partial class SumWindow : Window
    {
        private string input = String.Empty;
        public SumWindow()
        {
            InitializeComponent();
            input = ConvertString(ShareData.transText);
            SourceText.Text = input;
            SumText.Text = SummarizeText(input);
        }
        public string SummarizeText(string input)
        {
            var YOUR_API_KEY = "gAAAAABlZErp8klOmJLj0tYZwK9XD3waVoUGOcpfeD94pVkNx1dmpoNTySCyrBfhlN-jaXmWcejKbAzG_pBhTObpujyqUBmMhv-CnA1IDRGCnw-pUwspGwXftUVT66bMDnOx27ctgC0K";
            var client = new RestClient("https://api.textcortex.com/v1");
            var request = new RestRequest("texts/summarizations", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {YOUR_API_KEY}");
            //request.AddParameter("application/json", $"{{\n  \"target_lang\": \"en\", \n  \"text\": \"{input}\"\n}}", ParameterType.RequestBody);
            request.AddJsonBody(new { model = "gpt-3.5-turbo-16k", target_lang = "en", text = input });
            //request.AddParameter("application/json", new { target_lang = "en", text = input }, ParameterType.RequestBody);
            var response = client.Execute(request);
            var responseData = JObject.Parse(response.Content);
            string output = responseData["data"]["outputs"][0]["text"].ToString();
            return output;
        }
        private void SummarizeButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            SumText.Text = ShareData.TranslateText(SumText.Text, "auto", ShareData.langSecond);
        }
        string ConvertString(string input)
        {
            string convertInput = input.Replace(System.Environment.NewLine, " ");
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
        private async void CopySource2Button(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await Task.Delay(1000);
            Clipboard.SetTextAsync(SourceText.Text);
        }

        private async void CopySumedButton(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            await Task.Delay(1000);
            Clipboard.SetTextAsync(SumText.Text);
            ShareData.currentText = SumText.Text;
        }
        private async void Flyout_Opened_1(object? sender, System.EventArgs e)
        {
            var flyOut = sender as Flyout;
            await Task.Delay(2000);
            flyOut.Hide();
        }
    }
}
