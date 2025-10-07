/*****************************************************************************
 * 
 * ReoGrid - .NET 表計算スプレッドシートコンポーネント
 * https://reogrid.net/jp
 *
 * ReoGrid 日本語版デモプロジェクトは MIT ライセンスでリリースされています。
 * 
 * このソフトウェアは無保証であり、このソフトウェアの使用により生じた直接・間接の損害に対し、
 * 著作権者は補償を含むあらゆる責任を負いません。 
 * 
 * Copyright (c) 2012-2025 UNVELL Inc., All Rights Reserved.
 * https://unvell.com/
 * 
 ****************************************************************************/

namespace unvell.ReoGrid.Demo.PerformanceDemo
{
	partial class LazyLoadPerformanceDemo
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			grid = new ReoGridControl();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			button2 = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// grid
			// 
			grid.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
			grid.ColumnHeaderContextMenuStrip = null;
			grid.Dock = System.Windows.Forms.DockStyle.Fill;
			grid.LeadHeaderContextMenuStrip = null;
			grid.Location = new System.Drawing.Point(0, 0);
			grid.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			grid.Name = "grid";
			grid.RowHeaderContextMenuStrip = null;
			grid.Script = null;
			grid.SheetTabContextMenuStrip = null;
			grid.SheetTabNewButtonVisible = true;
			grid.SheetTabVisible = true;
			grid.SheetTabWidth = 667;
			grid.ShowScrollEndSpacing = true;
			grid.Size = new System.Drawing.Size(1084, 1283);
			grid.TabIndex = 8;
			grid.Text = "reoGridControl1";
			// 
			// panel1
			// 
			panel1.Controls.Add(label2);
			panel1.Controls.Add(button2);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(button1);
			panel1.Dock = System.Windows.Forms.DockStyle.Right;
			panel1.Location = new System.Drawing.Point(1084, 0);
			panel1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			panel1.Name = "panel1";
			panel1.Padding = new System.Windows.Forms.Padding(8);
			panel1.Size = new System.Drawing.Size(313, 1283);
			panel1.TabIndex = 9;
			// 
			// label2
			// 
			label2.Dock = System.Windows.Forms.DockStyle.Top;
			label2.Location = new System.Drawing.Point(8, 158);
			label2.Name = "label2";
			label2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			label2.Size = new System.Drawing.Size(297, 46);
			label2.TabIndex = 3;
			// 
			// button2
			// 
			button2.Dock = System.Windows.Forms.DockStyle.Top;
			button2.Location = new System.Drawing.Point(8, 106);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(297, 52);
			button2.TabIndex = 2;
			button2.Text = "Append 100000 x 10 Cells";
			button2.Click += button2_Click;
			// 
			// label1
			// 
			label1.Dock = System.Windows.Forms.DockStyle.Top;
			label1.Location = new System.Drawing.Point(8, 60);
			label1.Name = "label1";
			label1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			label1.Size = new System.Drawing.Size(297, 46);
			label1.TabIndex = 1;
			// 
			// button1
			// 
			button1.Dock = System.Windows.Forms.DockStyle.Top;
			button1.Location = new System.Drawing.Point(8, 8);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(297, 52);
			button1.TabIndex = 0;
			button1.Text = "Load 100000 x 10 Cells";
			button1.Click += button1_Click;
			// 
			// LazyLoadPerformanceDemo
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(grid);
			Controls.Add(panel1);
			Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			Name = "LazyLoadPerformanceDemo";
			Size = new System.Drawing.Size(1397, 1283);
			panel1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private ReoGridControl grid;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
	}
}