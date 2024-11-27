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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG2500_A2_Chinook.Pages
{
	/// <summary>
	/// Interaction logic for OrdersPage.xaml
	/// </summary>
	public partial class OrdersPage : Page
	{
		private readonly ChinookContext context;
		private readonly CollectionViewSource ordersViewSource;

		public OrdersPage()
		{
			InitializeComponent();

			context = new ChinookContext();

			// Load initial customer data using LINQ
			var customers = context.Customers
				.Include(c => c.Invoices)
				.ThenInclude(i => i.InvoiceLines)
				.ToList();

			ordersViewSource = (CollectionViewSource)FindResource("CustomerOrdersViewSource");
			ordersViewSource.Source = customers;
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string? search = SearchBox.Text?.Trim().ToLower();

			var customersQuery = context.Customers
				.Include(c => c.Invoices)
				.ThenInclude(i => i.InvoiceLines)
				.Where(c =>
					string.IsNullOrWhiteSpace(search) ||
					(c.FirstName != null && EF.Functions.Like(c.FirstName, $"%{search}%")) ||
					(c.LastName != null && EF.Functions.Like(c.LastName, $"%{search}%")) ||
					(c.Email != null && EF.Functions.Like(c.Email, $"%{search}%")) ||
					(c.Country != null && EF.Functions.Like(c.Country, $"%{search}%")))
				.ToList();

			ordersViewSource.Source = customersQuery;
		}
	}
}