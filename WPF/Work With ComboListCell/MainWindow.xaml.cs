using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using unvell.ReoGrid.CellTypes;

namespace Work_With_ComboListCell
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();


			grid.CurrentWorksheet.SetColumnsWidth(1, 2, 200);

			Demo1();
			Demo2();
			Demo3();

		}

		// ------- Demo 1: Basic ComboListCell -------
		void Demo1()
		{
			var sheet = grid.CurrentWorksheet;
			var combo = new ComboListCell(new[] { "Apple", "Banana", "Orange", "Applause", "Appreciate" });
			sheet.Cells["C3"].Body = combo;
			sheet["B3"] = "Basic ComboListCell";
			// ----------------------
		}

		// ------- Demo 2: Customize auto complete behavior -------
		void Demo2()
		{
			var sheet = grid.CurrentWorksheet;

			var combo2 = new ComboListCell(new[] { "Apple", "Banana", "Orange", "Applause", "Appreciate" });
			sheet.Cells["C5"].Body = combo2;
			sheet["B5"] = "Customize auto complete behavior";


			// customize auto complete behavior
			combo2.AutoCompleteComparerator = (item, text) =>
			{
				if (string.IsNullOrWhiteSpace(text)) return true;

				return item.Contains(text, StringComparison.CurrentCultureIgnoreCase);
			};
			
		}

		// ------- Demo 3: Customize dropdown list appearance -------
		void Demo3()
		{
			var sheet = grid.CurrentWorksheet;
			sheet["B7"] = "Customized dropdown list appearance";

			var combo3 = new ComboListCell(new[] { "Apple", "Banana", "Orange", "Applause", "Appreciate" });

			combo3.DropdownOpened += (s, e) =>
			{
				if (((ComboListCell)s).DropdownControl is ListBox listBox)
				{
					listBox.Template = (ControlTemplate)FindResource("ReoGridComboListTemplate");
					listBox.ItemTemplate = (DataTemplate)FindResource("ReoGridComboItemTemplate");
				}
			};

			sheet.Cells["C7"].Body = combo3;
		}
	}
}