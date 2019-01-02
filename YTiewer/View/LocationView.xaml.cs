using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YTiewer.View
{
    /// <summary>
    /// LocationView.xaml 的互動邏輯
    /// </summary>
    public partial class LocationView : UserControl
    {
        public LocationView()
        {
            InitializeComponent();
            CreateBtn();
        }

        Dictionary<int, string> Horizontal = new Dictionary<int, string>
        {
            { 1,"top"},
            { 2,"center"},
            { 3,"button"},
        };

        Dictionary<int, string> Vertical = new Dictionary<int, string>
        {
            { 1,"left"},
            { 2,"center"},
            { 3,"right"},
        };

        private void CreateBtn()
        {
            int h = 1, v = 1;
            Button btn;
            while (h <= 3)
            {
                btn = new Button
                {
                    Name = $"{Horizontal[h]}_{Vertical[v]}",
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment =  HorizontalAlignment.Stretch,
                };
                btn.Click += LocationBtn_Click;
                btn.SetValue(Grid.RowProperty, h);
                btn.SetValue(Grid.ColumnProperty, v++);

                Location.Children.Add(btn);

                if (v > 3)
                {
                    h++;
                    v = 1;
                }
            }
        }

        MainWindow MainView { get; set; }
        public void Initial(MainWindow mainView)
        {
            MainView = mainView;
            xAxis = new Dictionary<string, Func<double>>
            {
                { "left", () => 0},
                { "center", () => (SystemParameters.WorkArea.Width/2) - (MainView.Width/2) },
                { "right", () => SystemParameters.WorkArea.Width - MainView.Width }
            };

            yAxis = new Dictionary<string, Func<double>>
            {
                { "top", () => 0},
                { "center", () => (SystemParameters.WorkArea.Height/2) - (MainView.Height/2) },
                { "button", () => SystemParameters.WorkArea.Height - MainView.Height }
            };
        }

        Dictionary<string, Func<double>> xAxis;
        Dictionary<string, Func<double>> yAxis;

        private void LocationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainView == null) return;

            string
                btn = ((Button)sender).Name,
                h = btn.Split('_')[0],
                v = btn.Split('_')[1];

            MainView.Left = xAxis[v]();
            MainView.Top = yAxis[h]();
        }

    }
}
