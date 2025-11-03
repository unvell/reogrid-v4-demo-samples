using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using unvell.ReoGrid;
using unvell.ReoGrid.CellTypes;

namespace unvell.ReoGrid.WPFDemo.Behaviors
{
  public static class ReoGridBindingBehavior
  {
    public static readonly DependencyProperty ItemsSourceProperty =
      DependencyProperty.RegisterAttached("ItemsSource", typeof(IEnumerable),
        typeof(ReoGridBindingBehavior),
        new PropertyMetadata(null, OnItemsSourceChanged));

    public static readonly DependencyProperty EnableTwoWayProperty =
      DependencyProperty.RegisterAttached("EnableTwoWay", typeof(bool),
        typeof(ReoGridBindingBehavior),
        new PropertyMetadata(false, OnEnableTwoWayChanged));

    public static void SetItemsSource(DependencyObject d, IEnumerable value) { d.SetValue(ItemsSourceProperty, value); }
    public static IEnumerable GetItemsSource(DependencyObject d) { return (IEnumerable)d.GetValue(ItemsSourceProperty); }

    public static void SetEnableTwoWay(DependencyObject d, bool value) { d.SetValue(EnableTwoWayProperty, value); }
    public static bool GetEnableTwoWay(DependencyObject d) { return (bool)d.GetValue(EnableTwoWayProperty); }

    private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var grid = d as ReoGridControl;
      if (grid == null) return;
      Bind(grid, e.NewValue as IEnumerable);
    }

    private static void OnEnableTwoWayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var grid = d as ReoGridControl;
      if (grid == null) return;
      var items = GetItemsSource(grid);
      Bind(grid, items);
    }

    private static void Bind(ReoGridControl grid, IEnumerable items)
    {
      if (grid == null) return;
      var sheet = grid.CurrentWorksheet;
      sheet.Reset();
      if (items == null) return;

      var list = items.Cast<object>().ToList();
      if (list.Count == 0) return;

      var sample = list[0];
      var props = TypeDescriptor.GetProperties(sample)
        .Cast<PropertyDescriptor>()
        .Where(p => p.IsBrowsable && p.ComponentType == sample.GetType())
        .ToList();

      // Header
      for (int c = 0; c < props.Count; c++)
        sheet[0, c] = props[c].Name;

      // Data
      for (int r = 0; r < list.Count; r++)
      {
        var rowObj = list[r];
        for (int c = 0; c < props.Count; c++)
        {
          ApplyValueToCell(sheet, r + 1, c, props[c], rowObj);
        }
      }

      var notify = items as INotifyCollectionChanged;
      if (notify != null)
      {
        notify.CollectionChanged += (s, ev) =>
        {
          // Simple rebuild; optimize as needed
          Bind(grid, items);
        };
      }

      if (GetEnableTwoWay(grid))
      {
        sheet.CellDataChanged -= Sheet_CellDataChanged;
        sheet.CellDataChanged += Sheet_CellDataChanged;
        sheet.Tag = new SheetBindingContext { Items = items, Properties = props };
        foreach (var obj in list)
        {
          var inpc = obj as INotifyPropertyChanged;
          if (inpc != null)
          {
            inpc.PropertyChanged -= (o, e) => SyncFromObject(sheet, list, props, o, e.PropertyName);
            inpc.PropertyChanged += (o, e) => SyncFromObject(sheet, list, props, o, e.PropertyName);
          }
        }
      }
    }

    private class SheetBindingContext
    {
      public IEnumerable Items;
      public System.Collections.Generic.List<PropertyDescriptor> Properties;
    }

    private static void Sheet_CellDataChanged(object sender, unvell.ReoGrid.Events.CellEventArgs e)
    {
      var sheet = sender as Worksheet;
      if (sheet == null) return;
      var ctx = sheet.Tag as SheetBindingContext;
      if (ctx == null) return;

      // Ignore header row
      if (e.Cell.Position.Row == 0) return;
      int dataRow = e.Cell.Position.Row - 1;
      int col = e.Cell.Position.Col;

      var list = ctx.Items.Cast<object>().ToList();
      if (dataRow < 0 || dataRow >= list.Count) return;
      if (col < 0 || col >= ctx.Properties.Count) return;

      var prop = ctx.Properties[col];
      if (typeof(ICellBody).IsAssignableFrom(prop.PropertyType))
      {
        return;
      }
      object converted = e.Cell.Data;
      try
      {
        if (converted != null && prop.PropertyType != typeof(string) && converted is string)
        {
          converted = Convert.ChangeType(converted, prop.PropertyType);
        }
        prop.SetValue(list[dataRow], converted);
      }
      catch
      {
        // swallow or log
      }
    }

    private static void SyncFromObject(Worksheet sheet,
      System.Collections.Generic.List<object> list,
      System.Collections.Generic.List<PropertyDescriptor> props,
      object obj, string propertyName)
    {
      int rowIndex = list.IndexOf(obj);
      if (rowIndex < 0) return;
      for (int c = 0; c < props.Count; c++)
      {
        if (props[c].Name == propertyName)
        {
          ApplyValueToCell(sheet, rowIndex + 1, c, props[c], obj);
          break;
        }
      }
    }

    private static void ApplyValueToCell(Worksheet sheet, int sheetRow, int col,
      PropertyDescriptor prop, object rowObj)
    {
      var value = prop.GetValue(rowObj);

      if (value is ICellBody directBody)
      {
        sheet[sheetRow, col] = directBody;
        return;
      }

      if (rowObj is ICellBodyProvider provider
        && provider.TryGetCellBody(prop.Name, out var providedBody)
        && providedBody != null)
      {
        sheet[sheetRow, col] = providedBody;
      }

      sheet[sheetRow, col] = value;
    }
  }
}
