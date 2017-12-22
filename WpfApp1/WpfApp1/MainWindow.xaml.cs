using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
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

namespace WpfApp1
{
	public partial class MainWindow : Window
    {
        Context context;
        GoogleService _service;
        List<SearchResult> results = new List<SearchResult>();
        public MainWindow()
        {
            InitializeComponent();
            context = new Context();
            _service = new GoogleService(context);

        }

        private async void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                results = await _service.GetResults(textBoxQuery.Text);
                queryResults.ItemsSource = results;

            }
            catch
            {
                MessageBox.Show("Error occured", "Google Search");
            }
        }

        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            context.Results.Load();
            var res = context.Results.Local;
            queryResults.ItemsSource = res;
        }
    }
}
