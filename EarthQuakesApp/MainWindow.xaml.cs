using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json;

namespace EarthQuakesApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string url = "https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson&starttime=2019-01-01&endtime=2019-01-02";
        private const double KILOMETERS_IN_ONE_DEGREE = 111.2;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void ShowButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(amountTextBox.Text))
            {
                MessageBox.Show("Введите количество записей");
                return;
            }
            if (int.TryParse(amountTextBox.Text, out int result) && result > 0)
            {
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        RootObject rootObjects = JsonConvert.DeserializeObject<RootObject>(client.DownloadString(url));
                        List<Info> source = new List<Info>();
                        for (int i = 0; i < int.Parse(amountTextBox.Text); i++)
                        {
                            source.Add(rootObjects.Features[i].Infos);
                        }
                        dataGrid.ItemsSource = source;
                        for (int i = 0; i < int.Parse(amountTextBox.Text); i++)
                        {
                            ((Info)dataGrid.Items[i]).Depth *= KILOMETERS_IN_ONE_DEGREE;
                            ((Info)dataGrid.Items[i]).Time = new DateTime(long.Parse(((Info)dataGrid.Items[i]).Time)).ToString();
                        }
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Проверьте подключение к интернету");
                    }
                }
            }
            else
            {
                MessageBox.Show("Некорректные данные");
            }
        }
    }
}
