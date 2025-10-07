using System.Collections.ObjectModel;

namespace unvell.ReoGrid.WPFDemo.ViewModels
{
  public class SpreadsheetViewModel
  {
    public ObservableCollection<RowItem> Rows { get; } = new ObservableCollection<RowItem>();

    public SpreadsheetViewModel()
    {
      Rows.Add(new RowItem { Product = "Pen", Qty = 10, Price = 1.2 });
      Rows.Add(new RowItem { Product = "Notebook", Qty = 5, Price = 3.5 });
      Rows.Add(new RowItem { Product = "Binder", Qty = 2, Price = 6.0 });
    }
  }
}