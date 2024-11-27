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
		private readonly ChinookContext context;
		private readonly CollectionViewSource catalogueViewSource;

		public CataloguePage()
		{
			InitializeComponent();

			context = new ChinookContext();
			catalogueViewSource = (CollectionViewSource)FindResource(nameof(catalogueViewSource));
			LoadData();
		}

		private void LoadData()
		{
			// Ensure related data is loaded and use LINQ for grouping
			var groupedData = context.Artists
				.Include(a => a.Albums)
				.ThenInclude(album => album.Tracks)
				.Where(artist => !string.IsNullOrEmpty(artist.Name))
				.AsEnumerable()
				.GroupBy(artist => artist.Name![0].ToString().ToUpper())
				.Select(group => new Group
				{
					Name = group.Key,
					Artists = new ObservableCollection<Artist>(group)
				})
				.ToList();

			catalogueViewSource.Source = groupedData;
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string? search = SearchBox.Text?.Trim().ToLower();

			if (!string.IsNullOrWhiteSpace(search))
			{
				// Use LINQ to filter and group the data
				var filteredData = context.Artists
					.Include(a => a.Albums)
					.ThenInclude(album => album.Tracks)
					.Where(artist =>
						(artist.Name != null && EF.Functions.Like(artist.Name, $"%{search}%")) ||
						artist.Albums.Any(album =>
							(album.Title != null && EF.Functions.Like(album.Title, $"%{search}%")) ||
							album.Tracks.Any(track =>
								track.Name != null && EF.Functions.Like(track.Name, $"%{search}%"))
						)
					)
					.AsEnumerable()
					.GroupBy(artist => artist.Name![0].ToString().ToUpper())
					.Select(group => new Group
					{
						Name = group.Key,
						Artists = new ObservableCollection<Artist>(group)
					})
					.ToList();

				// Update the CollectionViewSource
				catalogueViewSource.Source = filteredData;
			}
			else
			{
				// Reset to the original data if the search box is empty
				LoadData();
			}
		}
	}
}