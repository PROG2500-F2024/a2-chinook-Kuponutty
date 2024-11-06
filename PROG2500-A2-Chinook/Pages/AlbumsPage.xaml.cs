using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Data;
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
	/// Interaction logic for Album.xaml
	/// </summary>
	public partial class AlbumsPage : Page
	{
		ChinookContext context = new ChinookContext();
		CollectionViewSource albumsViewSource = new CollectionViewSource();
		public AlbumsPage()
		{
			InitializeComponent();

			albumsViewSource = (CollectionViewSource)FindResource(nameof(albumsViewSource));
			context.Albums.Load();
			albumsViewSource.Source = context.Albums.Local.ToObservableCollection();
		}
	}
}
