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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PROG2500_A2_Chinook.Pages
{
	/// <summary>
	/// Interaction logic for Album.xaml
	/// </summary>
	public partial class AlbumsPage : Page
	{
		private readonly ChinookContext context;
		private readonly CollectionViewSource albumsViewSource;

		public AlbumsPage()
		{
			InitializeComponent();

			context = new ChinookContext();
			albumsViewSource = (CollectionViewSource)FindResource(nameof(albumsViewSource));

			LoadAlbums(null);
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string? search = SearchBox.Text?.Trim().ToLower();

			LoadAlbums(search);
		}

		private void LoadAlbums(string? search)
		{
			// LINQ query to get all or filtered albums
			var albumsQuery = context.Albums
				.Where(album => string.IsNullOrWhiteSpace(search) ||
								EF.Functions.Like(album.Title, $"%{search}%"))
				.AsNoTracking()
				.ToList();

			// Update the CollectionViewSource with the query result
			albumsViewSource.Source = albumsQuery;
		}
	}
}
