using unvell.ReoGrid.CellTypes;

namespace unvell.ReoGrid.WPFDemo.Behaviors
{
  public interface ICellBodyProvider
  {
    bool TryGetCellBody(string propertyName, out ICellBody cellBody);
  }
}
