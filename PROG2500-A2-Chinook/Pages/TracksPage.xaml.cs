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
	/// Interaction logic for Tracks.xaml
	/// </summary>
	public partial class TracksPage : Page
	{
		ChinookContext context = new ChinookContext();
		CollectionViewSource tracksViewSource = new CollectionViewSource();
		public TracksPage()
		{
			InitializeComponent();

			tracksViewSource = (CollectionViewSource)FindResource(nameof(tracksViewSource));
			context.Tracks.Load();
			tracksViewSource.Source = context.Tracks.Local.ToObservableCollection();
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string search = SearchBox.Text.ToLower();

			if (!string.IsNullOrWhiteSpace(search))
			{
				// Use LINQ to filter the tracks
				var filteredTracks = context.Tracks.Local
					.Where(track =>
						(track.Name?.Contains(search, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
						(track.Composer?.Contains(search, StringComparison.CurrentCultureIgnoreCase) ?? false) ||
						track.UnitPrice.ToString().Contains(search))
					.ToList();

				
				tracksViewSource.Source = filteredTracks;
			}
			else
			{
				
				tracksViewSource.Source = context.Tracks.Local.ToObservableCollection();
			}
		}
	}
}
