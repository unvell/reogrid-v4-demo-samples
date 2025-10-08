using System;
using unvell.ReoGrid.CellTypes;
using unvell.ReoGrid.Core.Header;
using unvell.ReoGrid.Events;
using unvell.ReoGrid.Graphics;
using unvell.ReoGrid.Interaction;
using unvell.ReoGrid.Rendering;

namespace unvell.ReoGrid.WPFDemo
{
	/// <summary>
	/// A header checkbox that controls (select all / deselect all) the checkbox cells in the column.
	/// Shows three states: Unchecked, Checked, Indeterminate.
	/// </summary>
	internal class CheckboxHeaderCell : HeaderBody
	{
		public HeaderCell HeaderCell { get; private set; }
	
		private enum CheckState { Unchecked, Checked, Indeterminate }
		private CheckState state = CheckState.Unchecked;

		private Rectangle boxRect = Rectangle.Zero;

		public override void OnSetup(IHeader header)
		{
			base.OnSetup(header);

			HeaderCell = header as HeaderCell;
		}

		public override void OnSizeChanged(Size size)
		{
			base.OnSizeChanged(size);
			var side = Math.Min(size.Width, size.Height) - 4; // padding
			if (side < 10) side = 10;
			boxRect = new Rectangle((size.Width - side) / 2f, (size.Height - side) / 2f, side, side);
		}

		public override void OnPaint(CellDrawingContext dc, Size renderSize)
		{
			var g = dc.Graphics;

			// scaled box rectangle
			var renderBoxRect = boxRect * dc.Worksheet.ScaleFactor;

			// background rectangle
			g.FillRectangle(renderBoxRect, SolidColor.WhiteSmoke);
			g.DrawRectangle(renderBoxRect, SolidColor.Gray, 1, LineStyles.Solid);

			if (state == CheckState.Checked)
			{
				var x = renderBoxRect.X; var y = renderBoxRect.Y; var w = renderBoxRect.Width; var h = renderBoxRect.Height;
				var p1 = new Point(x + w * 0.18f, y + h * 0.55f);
				var p2 = new Point(x + w * 0.42f, y + h * 0.75f);
				var p3 = new Point(x + w * 0.82f, y + h * 0.22f);
				var pts = new[] { p1, p2, p3 };
				var lineWidth = Math.Max(1f, w * 0.12f);
				g.DrawLines(pts, 0, pts.Length, SolidColor.Black, lineWidth, LineStyles.Solid);
			}
			else if (state == CheckState.Indeterminate)
			{
				var bar = new Rectangle(renderBoxRect.X + renderBoxRect.Width * 0.2f, renderBoxRect.Y + renderBoxRect.Height * 0.45f,
					renderBoxRect.Width * 0.6f, renderBoxRect.Height * 0.15f);
				g.FillRectangle(bar, SolidColor.Black);
			}
		}

		public override void OnMouseDown(HeaderCellMouseEventArgs e)
		{
			if (boxRect.Contains(e.RelativePosition))
			{
				e.IsCancelled = true; // capture to OnMouseUp only
			}
		}

		public override void OnMouseUp(HeaderCellMouseEventArgs e)
		{
			if (boxRect.Contains(e.RelativePosition))
			{
				Toggle();
				e.IsCancelled = true;
			}
		}

		public override void OnMouseMove(HeaderCellMouseEventArgs e)
		{
			base.OnMouseMove(e);

			this.HeaderCell.Worksheet.ChangeCursor(CursorStyle.PlatformDefault);
			e.IsCancelled = true;
		}

		public override void OnDataChange(int startRow, int endRow)
		{
			var headerCell = OwnerHeader as HeaderCell; // need ColumnIndex
			if (headerCell == null) return;
			var ws = headerCell.Worksheet;
			int col = headerCell.ColumnIndex;
			if (col < 0 || col >= ws.ColumnCount) return;

			bool anyTrue = false, anyFalse = false;
			for (int r = 0; r < ws.RowCount; r++)
			{
				var data = ws.GetCellData(r, col) as bool?;
				if (data == true) anyTrue = true; else anyFalse = true;
				if (anyTrue && anyFalse) break; // early exit
			}

			var newState = anyTrue && anyFalse ? CheckState.Indeterminate : (anyTrue ? CheckState.Checked : CheckState.Unchecked);
			if (newState != state)
			{
				state = newState;
			}
		}

		private void Toggle()
		{
			var headerCell = OwnerHeader as HeaderCell;
			if (headerCell == null) return;
			bool target = state != CheckState.Checked; // toggle (indeterminate -> checked)
			ApplyStateToColumn(headerCell, target);
		}

		private void ApplyStateToColumn(HeaderCell headerCell, bool value)
		{
			var ws = headerCell.Worksheet;
			int col = headerCell.ColumnIndex;
			ws.SuspendDataChangedEvents();
			try
			{
				for (int r = 0; r < ws.RowCount; r++)
				{
					var cell = ws.GetCell(r, col);
					if (cell == null) continue;
					if (cell.Body is not CheckBoxCell)
					{
						cell.Body = new CheckBoxCell(value);
					}
					cell.Data = value;
				}
			}
			finally
			{
				ws.ResumeDataChangedEvents();
			}
			state = value ? CheckState.Checked : CheckState.Unchecked;
		}

		public override IHeaderBody Clone() => new CheckboxHeaderCell { state = this.state };
	}
}
