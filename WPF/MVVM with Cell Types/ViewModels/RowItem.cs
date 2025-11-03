using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using unvell.ReoGrid.CellTypes;
using unvell.ReoGrid.WPFDemo.Behaviors;

namespace unvell.ReoGrid.WPFDemo.ViewModels
{
  public class RowItem : INotifyPropertyChanged, ICellBodyProvider
  {
    private static readonly string[] DefaultCategories = new[] { "Stationery", "Office Supplies", "Accessories", "Misc" };
    private static readonly IReadOnlyList<string> DefaultCategoryOptions = Array.AsReadOnly(DefaultCategories);

    public static IReadOnlyList<string> CategoryOptions => DefaultCategoryOptions;

    private readonly Dictionary<string, ICellBody> _cellBodies = new Dictionary<string, ICellBody>();

    private string _product;
    private int _qty;
    private double _price;
    private bool _inStock;
    private string _category;

    public RowItem()
      : this(DefaultCategories)
    {
    }

    public RowItem(IEnumerable<string> categoryOptions)
    {
      var categories = (categoryOptions ?? DefaultCategoryOptions)
        .Where(s => !string.IsNullOrWhiteSpace(s))
        .Distinct()
        .ToArray();
      if (categories.Length == 0)
        categories = DefaultCategoryOptions.ToArray();

      _cellBodies[nameof(InStock)] = new CheckBoxCell();
      _cellBodies[nameof(Category)] = new ComboListCell(categories);

      _inStock = true;
      _category = categories[0];
    }

    public string Product { get { return _product; } set { if (_product != value) { _product = value; OnPropertyChanged(nameof(Product)); } } }
    public int Qty { get { return _qty; } set { if (_qty != value) { _qty = value; OnPropertyChanged(nameof(Qty)); OnPropertyChanged(nameof(Total)); } } }
    public double Price { get { return _price; } set { if (_price != value) { _price = value; OnPropertyChanged(nameof(Price)); OnPropertyChanged(nameof(Total)); } } }
    public bool InStock { get { return _inStock; } set { if (_inStock != value) { _inStock = value; OnPropertyChanged(nameof(InStock)); } } }
    public string Category { get { return _category; } set { if (_category != value) { _category = value; OnPropertyChanged(nameof(Category)); } } }
    public double Total { get { return _qty * _price; } }

    public bool TryGetCellBody(string propertyName, out ICellBody cellBody)
    {
      return _cellBodies.TryGetValue(propertyName, out cellBody);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) { var h = PropertyChanged; if (h != null) h(this, new PropertyChangedEventArgs(name)); }
  }
}
