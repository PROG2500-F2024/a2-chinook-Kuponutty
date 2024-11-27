using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PROG2500_A2_Chinook.Models
{
	public class Group
	{
		public string Name { get; set; } // The group name, e.g., "A", "B"
		public int ItemCount => Artists.Count; 
		
		//public List<Artist> Artists { get; set; } = new List<Artist>(); // The artists in this group
		public ObservableCollection<Artist> Artists { get; set; } = new ObservableCollection<Artist>();

		public event PropertyChangedEventHandler PropertyChanged;


		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Notify UI when the Artists collection changes
		public void UpdateItemCount()
		{
			OnPropertyChanged(nameof(ItemCount));
		}
	}
}
