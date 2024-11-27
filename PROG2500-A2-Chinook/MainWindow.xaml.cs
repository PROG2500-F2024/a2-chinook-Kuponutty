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

namespace PROG2500_A2_Chinook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
			mainFrame.Navigate(new Pages.HomePage());
		}

		private void HomeButton_Click(object sender, RoutedEventArgs e)
		{
			mainFrame.NavigationService.Navigate(new Pages.HomePage());
		}

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
		}

        private void ArtistsButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new Pages.ArtistsPage());
		}

		private void AlbumsButton_Click(object sender, RoutedEventArgs e)
		{
			mainFrame.NavigationService.Navigate(new Pages.AlbumsPage());
		}

		private void TracksButton_Click(object sender, RoutedEventArgs e)
		{
			mainFrame.NavigationService.Navigate(new Pages.TracksPage());
		}

		private void CatalogueButton_Click(object sender, RoutedEventArgs e)
		{
			mainFrame.NavigationService.Navigate(new Pages.CataloguePage());
		}

		private void OrdersButton_Click(object sender, RoutedEventArgs e)
		{
			mainFrame.NavigationService.Navigate(new Pages.OrdersPage());
		}
	}
}
