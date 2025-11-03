using System.Collections.ObjectModel;
using System.Linq;

namespace unvell.ReoGrid.WPFDemo.ViewModels
{
  public class SpreadsheetViewModel
  {
    public ObservableCollection<RowItem> Rows { get; } = new ObservableCollection<RowItem>();

    private static readonly string[] Categories = RowItem.CategoryOptions.ToArray();

    public SpreadsheetViewModel()
    {
      Rows.Add(new RowItem(Categories) { Product = "Pen", Qty = 20, Price = 1.2, InStock = true, Category = Categories[0] });
      Rows.Add(new RowItem(Categories) { Product = "Notebook", Qty = 5, Price = 3.5, InStock = false, Category = Categories[1] });
      Rows.Add(new RowItem(Categories) { Product = "Binder", Qty = 2, Price = 6.0, InStock = true, Category = Categories[2] });
    }
  }
}
