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

			// Load Customers into the context
			var customers = context.Customers
								   .Include(c => c.Invoices)
								   .ThenInclude(i => i.InvoiceLines)
								   .ToList();

			// Ensure CollectionViewSource is correctly bound
			ordersViewSource = (CollectionViewSource)FindResource("CustomerOrdersViewSource");
			ordersViewSource.Source = customers;
		}

		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			string search = SearchBox.Text?.ToLower();

			if (!string.IsNullOrWhiteSpace(search))
			{
				// Use LINQ to filter the customers based on the search text
				var filteredCustomers = context.Customers
					.Include(c => c.Invoices)
					.ThenInclude(i => i.InvoiceLines)
					.Where(c =>
						(c.FirstName != null && c.FirstName.ToLower().Contains(search)) ||
						(c.LastName != null && c.LastName.ToLower().Contains(search)) ||
						(c.Email != null && c.Email.ToLower().Contains(search)) ||
						(c.Country != null && c.Country.ToLower().Contains(search)))
					.ToList();

				// Update the CollectionViewSource with the filtered list
				ordersViewSource.Source = filteredCustomers;
			}
			else
			{
				// Reset to show all customers if search is empty
				var allCustomers = context.Customers
										  .Include(c => c.Invoices)
										  .ThenInclude(i => i.InvoiceLines)
										  .ToList();

				ordersViewSource.Source = allCustomers;
			}
		}
	}
}