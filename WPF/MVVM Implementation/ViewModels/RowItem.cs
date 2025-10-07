using System.ComponentModel;

namespace unvell.ReoGrid.WPFDemo.ViewModels
{
  public class RowItem : INotifyPropertyChanged
  {
    private string _product;
    private int _qty;
    private double _price;

    public string Product { get { return _product; } set { if (_product != value) { _product = value; OnPropertyChanged(nameof(Product)); } } }
    public int Qty { get { return _qty; } set { if (_qty != value) { _qty = value; OnPropertyChanged(nameof(Qty)); OnPropertyChanged(nameof(Total)); } } }
    public double Price { get { return _price; } set { if (_price != value) { _price = value; OnPropertyChanged(nameof(Price)); OnPropertyChanged(nameof(Total)); } } }
    public double Total { get { return _qty * _price; } }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) { var h = PropertyChanged; if (h != null) h(this, new PropertyChangedEventArgs(name)); }
  }
}