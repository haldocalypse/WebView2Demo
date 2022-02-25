using Microsoft.Web.WebView2.Core;
using System.Windows;

namespace WebView2Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CoreWebView2Environment WebViewEnvironment { get; set; }
        string PdfPath = @"PATH\TO\YOUR\print_test.pdf";

        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            webView2.NavigationCompleted += WebView_NavigationCompleted;
            await webView2.EnsureCoreWebView2Async(null);
            WebViewEnvironment = await CoreWebView2Environment.CreateAsync().ConfigureAwait(true);
        }

        private async void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            //await webView2.CoreWebView2.ExecuteScriptAsync("window.print();");        
            CoreWebView2PrintSettings lPrintSettings = WebViewEnvironment.CreatePrintSettings();
            lPrintSettings.MarginBottom = 0;
            lPrintSettings.MarginLeft = 0;
            lPrintSettings.MarginRight = 0;
            lPrintSettings.MarginTop = 0;
            lPrintSettings.ShouldPrintBackgrounds = true;
            lPrintSettings.ShouldPrintHeaderAndFooter = false;
            lPrintSettings.ShouldPrintSelectionOnly = false;
            await webView2.CoreWebView2.PrintToPdfAsync(PdfPath, lPrintSettings);
        }

        private void PopulateWebView2_Click(object sender, RoutedEventArgs e)
        {
            if (webView2 != null && webView2.CoreWebView2 != null)
            {
                webView2.NavigateToString(GetHtml());
            }
        }

        // create simple HTML with 2 images
        private string GetHtml()
        {
            return @"<html><body>
              <div>
                <p style=""color: red; font-weight: bold;"">Some text</p>
                <img src=""https://via.placeholder.com/350"" alt=""my image""/><br/>
                <img src=""https://via.placeholder.com/150"" alt=""my image2""/>
              </div>
            </html></body>";
        }
    }
}
