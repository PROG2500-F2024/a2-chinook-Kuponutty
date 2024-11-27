using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
using PROG2500_A2_Chinook.Models;
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
using System.Windows.Shapes;

namespace PROG2500_A2_Chinook.Pages
{
	/// <summary>
	/// Interaction logic for Artists.xaml
	/// </summary>
	public partial class ArtistsPage : Page
	{
		ChinookContext context = new ChinookContext();
		CollectionViewSource artistsViewSource = new CollectionViewSource();
		public ArtistsPage()
		{
			InitializeComponent();

			artistsViewSource = (CollectionViewSource)FindResource(nameof(artistsViewSource));
			context.Artists.Load();
			artistsViewSource.Source = context.Artists.Local.ToObservableCollection();
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string search = SearchBox.Text.ToLower();

			if (!string.IsNullOrWhiteSpace(search))
			{
				artistsViewSource.View.Filter = item =>
				{
					if (item is Artist artist)
					{
						return artist.Name?.Contains(search, StringComparison.CurrentCultureIgnoreCase) ?? false;
					}
					return false;
				};
			}
			else
			{
				artistsViewSource.View.Filter = null;
			}
		}
	}
}