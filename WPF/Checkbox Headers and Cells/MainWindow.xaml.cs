using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using unvell.ReoGrid.CellTypes;

namespace unvell.ReoGrid.WPFDemo
{
	/// <summary>
	/// Interaction logic for CheckboxWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private static readonly Random rand = new Random(1);

		public MainWindow()
		{
			InitializeComponent();

			var sheet = grid.CurrentWorksheet;

			sheet.Rows = 10;

			// 10列使用 (0~9) を前提にヘッダーを設定
			sheet.ColumnHeaders[0].Body = new CheckboxHeaderCell(grid); // 集約チェック
			sheet.ColumnHeaders[1].Text = "ID";
			sheet.ColumnHeaders[2].Text = "Name";
			sheet.ColumnHeaders[3].Text = "Math";
			sheet.ColumnHeaders[4].Text = "Science";
			sheet.ColumnHeaders[5].Text = "English";
			sheet.ColumnHeaders[6].Text = "History";
			sheet.ColumnHeaders[7].Text = "PE";
			sheet.ColumnHeaders[8].Text = "Art";
			sheet.ColumnHeaders[9].Text = "Average";

			// 10行のダミーデータ (学生成績例)
			for (int r = 0; r < 10; r++)
			{
				// チェック列 (偶数行のみ true)
				var chkCell = sheet.Cells[r, 0];
				if (chkCell.Body is not CheckBoxCell)
				{
					chkCell.Body = new CheckBoxCell(false);
				}
				chkCell.Data = (r % 2 == 0);

				// ID & 名前
				sheet[r, 1] = $"S{r + 1:000}";
				sheet[r, 2] = $"Student {r + 1}";

				// 科目スコア (3~8列)
				for (int c = 3; c <= 8; c++)
				{
					// 50〜100 の乱数
					sheet[r, c] = 50 + rand.Next(51);
				}

				// 平均 (J列) AVERAGE(D..I)
				int excelRow = r + 1; // 1-based
				sheet[r, 9] = $"=ROUND(AVERAGE(D{excelRow}:I{excelRow}), 1)";
			}

			// 列幅調整
			sheet.SetColumnsWidth(0, 1, 28); // チェック列
			sheet.AutoFitColumnsWidth(1, 2); // ID & Name
			sheet.AutoFitColumnsWidth(3, 7); // 科目列
			sheet.AutoFitColumnsWidth(9, 1); // Average
		}
	}
}
