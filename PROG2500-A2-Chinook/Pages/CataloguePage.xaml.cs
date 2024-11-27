using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PROG2500_A2_Chinook.Data;
using PROG2500_A2_Chinook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PROG2500_A2_Chinook.Pages
{
    /// <summary>
    /// Interaction logic for CataloguePage.xaml
    /// </summary>
	/// 

    public partial class CataloguePage : Page
    {
		ChinookContext context = new ChinookContext();
		CollectionViewSource catalogueViewSource = new CollectionViewSource();
		public CataloguePage()
        {
            InitializeComponent();

			catalogueViewSource = (CollectionViewSource)FindResource(nameof(catalogueViewSource));
			LoadData();
		}
		private void LoadData()
		{
			// Ensure related data is loaded
			context.Artists
				.Include(a => a.Albums)
				.ThenInclude(album => album.Tracks)
				.Load();

			var groupedData = context.Artists.Local
				.Where(artist => !string.IsNullOrEmpty(artist.Name))
				.GroupBy(artist => artist.Name![0].ToString().ToUpper())
				.Select(group => new Group
				{
					Name = group.Key,
					Artists = new ObservableCollection<Artist>(group.ToList())
				})
				.ToList();

			catalogueViewSource.Source = groupedData;
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string? search = SearchBox.Text?.Trim().ToLower();

			if (!string.IsNullOrWhiteSpace(search))
			{
				catalogueViewSource.View.Filter = item =>
				{
					if (item is Group group)
					{
						// Filter the artists dynamically
						var filteredArtists = group.Artists
							.Where(artist =>
								artist.Name?.ToLower().Contains(search) == true || // Match artist name
								artist.Albums.Any(album =>
									album.Title?.ToLower().Contains(search) == true || // Match album title
									album.Tracks.Any(track => track.Name?.ToLower().Contains(search) == true) // Match track name
								)
							)
							.ToList();

						// Update the Artists collection
						group.Artists.Clear();
						foreach (var artist in filteredArtists)
						{
							group.Artists.Add(artist);
						}

						// Notify UI that the ItemCount has changed
						group.UpdateItemCount();

						// Keep the group if it has matching artists
						return group.Artists.Any();
					}

					return false;
				};
			}
			else
			{
				// Reset to the original data if the search box is empty
				LoadData();
			}

			// Refresh the CollectionViewSource to reflect changes
			catalogueViewSource.View.Refresh();
		}
	}
}
