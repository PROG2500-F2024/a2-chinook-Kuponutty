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
		private readonly ChinookContext context;
		private readonly CollectionViewSource artistsViewSource;

		public ArtistsPage()
		{
			InitializeComponent();

			context = new ChinookContext();
			artistsViewSource = (CollectionViewSource)FindResource(nameof(artistsViewSource));

			LoadArtists(null);
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string? search = SearchBox.Text?.Trim().ToLower();

			LoadArtists(search);
		}

		private void LoadArtists(string? search)
		{
			// LINQ query to get all or filtered artists
			var artistsQuery = context.Artists
				.Where(artist => string.IsNullOrWhiteSpace(search) ||
								 EF.Functions.Like(artist.Name, $"%{search}%"))
				.AsNoTracking()
				.ToList();

			// Update the CollectionViewSource with the query result
			artistsViewSource.Source = artistsQuery;
		}
	}
}