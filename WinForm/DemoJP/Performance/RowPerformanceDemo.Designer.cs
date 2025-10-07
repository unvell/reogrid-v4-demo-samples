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
	partial class RowPerformanceDemo
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
			button6 = new System.Windows.Forms.Button();
			button5 = new System.Windows.Forms.Button();
			button4 = new System.Windows.Forms.Button();
			button3 = new System.Windows.Forms.Button();
			button2 = new System.Windows.Forms.Button();
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
			grid.Size = new System.Drawing.Size(1169, 1305);
			grid.TabIndex = 8;
			grid.Text = "reoGridControl1";
			// 
			// panel1
			// 
			panel1.Controls.Add(button6);
			panel1.Controls.Add(button5);
			panel1.Controls.Add(button4);
			panel1.Controls.Add(button3);
			panel1.Controls.Add(button2);
			panel1.Controls.Add(button1);
			panel1.Dock = System.Windows.Forms.DockStyle.Right;
			panel1.Location = new System.Drawing.Point(1169, 0);
			panel1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(313, 1305);
			panel1.TabIndex = 9;
			// 
			// button6
			// 
			button6.Location = new System.Drawing.Point(10, 468);
			button6.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			button6.Name = "button6";
			button6.Size = new System.Drawing.Size(292, 67);
			button6.TabIndex = 8;
			button6.Text = "100000 x 100 Borders";
			button6.UseVisualStyleBackColor = true;
			button6.Click += button6_Click;
			// 
			// button5
			// 
			button5.Enabled = false;
			button5.Location = new System.Drawing.Point(10, 338);
			button5.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			button5.Name = "button5";
			button5.Size = new System.Drawing.Size(292, 88);
			button5.TabIndex = 7;
			button5.Text = "10 万行のデータを追加\r\n (イベントの発生を禁止)";
			button5.UseVisualStyleBackColor = true;
			button5.Click += button5_Click;
			// 
			// button4
			// 
			button4.Enabled = false;
			button4.Location = new System.Drawing.Point(10, 256);
			button4.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(292, 71);
			button4.TabIndex = 6;
			button4.Text = "10 万行のデータを追加";
			button4.UseVisualStyleBackColor = true;
			button4.Click += button4_Click;
			// 
			// button3
			// 
			button3.Enabled = false;
			button3.Location = new System.Drawing.Point(10, 173);
			button3.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(292, 71);
			button3.TabIndex = 5;
			button3.Text = "行数を 10 万行に変更";
			button3.UseVisualStyleBackColor = true;
			button3.Click += button3_Click;
			// 
			// button2
			// 
			button2.Enabled = false;
			button2.Location = new System.Drawing.Point(10, 94);
			button2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			button2.Name = "button2";
			button2.Size = new System.Drawing.Size(292, 67);
			button2.TabIndex = 4;
			button2.Text = "1万行のデータを追加";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// button1
			// 
			button1.Location = new System.Drawing.Point(10, 15);
			button1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(292, 67);
			button1.TabIndex = 3;
			button1.Text = "行数を 1 万行に追加";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// RowPerformanceDemo
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(grid);
			Controls.Add(panel1);
			Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			Name = "RowPerformanceDemo";
			Size = new System.Drawing.Size(1482, 1305);
			panel1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private ReoGridControl grid;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
	}
}