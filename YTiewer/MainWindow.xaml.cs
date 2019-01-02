using CefSharp;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YTiewer.Properties;
using YTiewer.Unity;

namespace YTiewer
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var clipboardMng = new ClipboardManager(this);
            clipboardMng.ClipboardChanged += (s, ee) =>
            {
                bool getTxt = false;
                while (!getTxt)
                {
                    try
                    {
                        if (Clipboard.ContainsText()) YTInfo.Detail.YTUrl = Clipboard.GetText();
                        getTxt = true;
                    }
                    catch
                    {
                        Thread.Sleep(50);
                    }
                }
            };

            LocationSetting.Initial(this);
        }



        private void SearchVideo_Click(object sender, RoutedEventArgs e)
            => SearchFlyout.IsOpen = !SearchFlyout.IsOpen;

        private void TopmostBtn_Click(object sender, RoutedEventArgs e)
            => ((Button)sender).Content = (Settings.Default.KeepTopmost = !Settings.Default.KeepTopmost) ? "Release" : "Topmost";

        private void Position_Click(object sender, RoutedEventArgs e)
            => LocationFlyout.IsOpen = !LocationFlyout.IsOpen;

        //private void MetroWindow_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    this.UseNoneWindowStyle = false;
        //}

        //private void MetroWindow_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    this.UseNoneWindowStyle = true;
        //}
    }
}
