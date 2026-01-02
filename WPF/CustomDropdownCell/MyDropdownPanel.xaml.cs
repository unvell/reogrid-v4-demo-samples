using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ReoGridWPFSample
{
	/// <summary>
	/// XAMLエディタで編集可能なドロップダウン用 TreeView パネル
	/// </summary>
	public partial class MyDropdownPanel : UserControl
	{
		public MyDropdownPanel()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty ItemsProperty =
			DependencyProperty.Register(nameof(Items), typeof(IEnumerable<GroupNode>), typeof(MyDropdownPanel), new PropertyMetadata(null));

		public IEnumerable<GroupNode> Items
		{
			get => (IEnumerable<GroupNode>)GetValue(ItemsProperty);
			set => SetValue(ItemsProperty, value);
		}

		/// <summary>
		/// CustomDropdownCell から TreeView を直接触れるように公開
		/// </summary>
		public TreeView Tree => GroupedTree;
	}
}
