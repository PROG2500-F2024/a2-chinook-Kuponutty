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
			tracksViewSource.Source = context.Tracks
				.AsNoTracking()
				.ToList();
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string? search = SearchBox.Text?.Trim().ToLower();

			var tracksQuery = context.Tracks
				.AsNoTracking()
				.Where(track =>
					string.IsNullOrWhiteSpace(search) ||
					(track.Name != null && EF.Functions.Like(track.Name, $"%{search}%")) ||
					(track.Composer != null && EF.Functions.Like(track.Composer, $"%{search}%")) ||
					track.UnitPrice.ToString().Contains(search))
				.ToList();

			tracksViewSource.Source = tracksQuery;
		}
	}
}