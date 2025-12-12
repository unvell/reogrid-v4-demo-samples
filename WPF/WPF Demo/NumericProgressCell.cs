/*****************************************************************************
 * 
 * ReoGrid - .NET Spreadsheet Control
 * 
 * https://reogrid.net/
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
 * PURPOSE.
 *
 * Copyright (c) 2012-2025 UNVELL Inc., All rights reserved.
 * 
 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using unvell.ReoGrid.CellTypes;
using unvell.ReoGrid.Graphics;
using unvell.ReoGrid.Rendering;

namespace unvell.ReoGrid.WPFDemo
{
	internal class NumericProgressCell : CellBody
	{
		public override void OnPaint(CellDrawingContext dc)
		{
			double value = Cell.Worksheet.GetCellData<double>(this.Cell.Position);

			var bounds = GetBodyBounds();

			int width = (int)(Math.Round(value * bounds.Width));

			if (width > 0)
			{
				Rectangle rect = new Rectangle(bounds.Left, bounds.Top + 1, width, bounds.Height - 1);
				dc.Graphics.FillRectangleLinear(SolidColor.Coral, SolidColor.IndianRed, 90f, rect);
			}
		}
	}
}
