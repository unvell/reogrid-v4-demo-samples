using System.Collections.Generic;
using System.Linq;
using System.Windows;
using unvell.ReoGrid;

namespace ReoGridWPFSample
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			SetupSheet();
		}

		private void SetupSheet()
		{
			var sheet = grid.CurrentWorksheet;

			// 見やすいようにカラム幅を確保
			sheet.SetColumnsWidth(1, 2, 200);

			var groupOptions = new[]
			{
				new GroupOption { GroupName = "果物", Items = new[] { "りんご", "バナナ", "みかん" } },
				new GroupOption { GroupName = "野菜", Items = new[] { "にんじん", "ブロッコリー", "ピーマン" } },
				new GroupOption { GroupName = "飲み物", Items = new[] { "コーヒー", "紅茶", "水" } },
			};

			var groups = groupOptions.Select(opt => new GroupNode(opt)).ToList();

			// カスタムドロップダウンセルを使用
			var dropdown = new CustomDropdownCell(groups);
			sheet["B3"] = "グループ付きドロップダウン";
			sheet.Cells["C3"].Body = dropdown;
			sheet.Ranges["C3"].BorderOutside = RangeBorderStyle.GraySolid;

			// カスタムドロップダウンセル（パネル版）を使用
			var dropdownFromXaml = new CustomDropdownCellWithPanel(groups);
			sheet["B5"] = "XAML UserControl 版";
			sheet.Cells["C5"].Body = dropdownFromXaml;
			sheet.Ranges["C5"].BorderOutside = RangeBorderStyle.GraySolid;
		}
	}
}
