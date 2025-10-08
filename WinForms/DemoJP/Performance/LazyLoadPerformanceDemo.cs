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

using Microsoft.VisualBasic.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using unvell.ReoGrid.Data;
using unvell.ReoGrid.Graphics;

namespace unvell.ReoGrid.Demo.PerformanceDemo
{
	/// <summary>
	/// データの追加性能のデモ
	/// </summary>
	public partial class LazyLoadPerformanceDemo : UserControl
	{
		private Worksheet worksheet;

		public LazyLoadPerformanceDemo()
		{
			InitializeComponent();

			this.worksheet = grid.CurrentWorksheet;
			this.worksheet.SetColumnsWidth(0, 2, 110);
			this.worksheet.SetColumnsWidth(8, 2, 110);

			var exh = this.worksheet.ExtensionColumnHeader;
			exh.SetRowCount(2);

			exh.MergeCells(0, 2, 1, 3);
			exh.Cells[0, 2].Text = "基本データ";
			exh.MergeCells(0, 5, 1, 3);
			exh.Cells[0, 5].Text = "観測データ";
			exh.MergeCells(0, 8, 1, 3);
			exh.Cells[0, 8].Text = "環境データ";

			exh.Cells[0, 2].Style.HorizontalAlignment = ReoGridHorAlign.Center;
			exh.Cells[0, 5].Style.HorizontalAlignment = ReoGridHorAlign.Center;
			exh.Cells[0, 8].Style.HorizontalAlignment = ReoGridHorAlign.Center;

			exh.Cells[1, 0].Text = "No.";
			exh.Cells[1, 1].Text = "時間";
			exh.Cells[1, 2].Text = "速度 (km/h)";
			exh.Cells[1, 3].Text = "走行距離 (km)";
			exh.Cells[1, 4].Text = "アクセル踏み具合 (%)";
			exh.Cells[1, 5].Text = "エンブレ (%)";
			exh.Cells[1, 6].Text = "ガソリン量 (L)";
			exh.Cells[1, 7].Text = "扉開閉 (1:開, 0:閉)";
			exh.Cells[1, 8].Text = "GPS 緯度";
			exh.Cells[1, 9].Text = "GPS 経度";
			exh.Cells[1, 10].Text = "天気";
			exh.Cells[1, 11].Text = "比(%)";

		}

		public static int RECORD_COUNT = 100000;
		public static int COLUMN_COUNT = 230;

		public FlightLogDataSource dataSource;

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			// データソースを作成
			dataSource = new FlightLogDataSource(worksheet);

			// データの生成
			var logs = FlightLogGenerator.GenerateFlightLogs(RECORD_COUNT);

			// データをデータソースに追加
			dataSource.AppendLogs(logs);

			button1.Text = $"Load {RECORD_COUNT} x {COLUMN_COUNT} Data";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Stopwatch sw = Stopwatch.StartNew();

			// ワークシートの行数を設定
			worksheet.SetRows(dataSource.Logs.Count);

			// データの設定
			worksheet.AddDataSource(new RangePosition(0, 0, dataSource.Logs.Count, COLUMN_COUNT),
				dataSource,
				DataSourceLoadMode.LazyLoading // データの読み込みモード = 遅延読み込み
				);

			sw.Stop();
			label1.Text = $"Data loaded: {sw.ElapsedMilliseconds} ms.";

		}

		private void button2_Click(object sender, EventArgs e)
		{
			Stopwatch sw = Stopwatch.StartNew();

			// 追加データの生成
			dataSource.AppendLogs(FlightLogGenerator.GenerateFlightLogs(RECORD_COUNT));

			// ワークシートの行数を設定
			worksheet.SetRows(dataSource.Logs.Count);

			// データの設定
			worksheet.ResizeDataSource(new RangePosition(0, 0, dataSource.Logs.Count, COLUMN_COUNT), dataSource);

			sw.Stop();
			label2.Text = $"Data appended: {sw.ElapsedMilliseconds} ms.";
		}
	}

	/// <summary>
	/// ダミーフライトログデータソース
	/// </summary>
	public class FlightLogDataSource : IDataSource<FlightlogDataRecord>
	{
		public Worksheet Worksheet { get; private set; }

		public List<FlightLog> Logs { get; private set; } = new List<FlightLog>();

		private FlightlogDataRecord[] initedRecords = new FlightlogDataRecord[10];

		public FlightLogDataSource(Worksheet worksheet)
		{
			this.Worksheet = worksheet;
		}

		public int RecordCount => Logs.Count;
		public int ColumnCount => LazyLoadPerformanceDemo.COLUMN_COUNT;

		public bool SuspendDataChangeEvent { get; set; }
		public event EventHandler<DataSourceChangedEventArgs> OnInputDataChanged;
		public event EventHandler<DataSourceChangedEventArgs> OnDataSizeChanged;

		public void AppendLogs(List<FlightLog> logs)
		{
			this.Logs.AddRange(logs);
			Array.Resize(ref this.initedRecords, this.Logs.Count);
		}

		/// <summary>
		/// 指定された行のデータを取得します
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public FlightlogDataRecord GetRecord(int row)
		{
			FlightlogDataRecord record = initedRecords[row];

			if (record == null)
			{
				record = new FlightlogDataRecord(this, row, Logs[row]);

				// 行が初めてアクセスされたときに、行の罫線を設定
				Worksheet.SetRangeBorders(row, 0, 1, ColumnCount, BorderPositions.Outside, RangeBorderStyle.BlackSolid);
				Worksheet.SetRangeBorders(row, 0, 1, ColumnCount, BorderPositions.InsideVertical, RangeBorderStyle.GrayDotted);
			}

			// 行データを返却
			return record;
		}

		public IEnumerator<FlightlogDataRecord> GetEnumerator()
		{
			for (int i = 0; i < Logs.Count; i++)
			{
				yield return new FlightlogDataRecord(this, i, Logs[i]);
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public class FlightlogDataRecord : IDataRecord
	{
		private FlightLogDataSource source;
		private int row;
		public FlightLog LogData { get; private set; }
		public FlightlogDataRecord(FlightLogDataSource source, int row, FlightLog logData)
		{
			this.source = source;
			this.row = row;
			this.LogData = logData;
		}

		public object GetData(int columnIndex) {
			switch (columnIndex)
			{
				case 0: return $"No. {row + 1}";
				case 1: return LogData.Timestamp;
				case 2: return LogData.Speed;
				case 3: return LogData.Distance;
				case 4: return LogData.Throttle;
				case 5: return LogData.EngineBrake;
				case 6: return LogData.Fuel;
				case 7: return LogData.DoorStatus;
				case 8: return LogData.GPSLatitude;
				case 9: return LogData.GPSLongitude;
				case 10: return LogData.Weather;
				case 11: return $"=ROUND({new CellPosition(row, 4).ToAbsoluteAddress()} / {new CellPosition(row, 5).ToAbsoluteAddress()}, 1)";
			}
			return null;
		}
	}

	// フライトログデータ構造
	public class FlightLog
	{
		public DateTime Timestamp { get; set; }  // 時間
		public double Speed { get; set; }  // 速度 (km/h)
		public double Distance { get; set; }  // 走行距離 (km)
		public int Throttle { get; set; }  // アクセル踏み具合 (%)
		public int EngineBrake { get; set; }  // エンブレ (%)
		public double Fuel { get; set; }  // ガソリン量 (L)
		public bool DoorStatus { get; set; }  // 扉開閉 (1:開, 0:閉)
		public double GPSLatitude { get; set; }  // GPS 緯度
		public double GPSLongitude { get; set; }  // GPS 経度
		public string Weather { get; set; }  // 天気
	}

	public class FlightLogGenerator
	{
		/// <summary>
		/// ランダムなフライトログデータを生成します
		/// </summary>
		/// <param name="numRows"></param>
		/// <returns></returns>
		public static List<FlightLog> GenerateFlightLogs(int numRows = 10000)
		{
			Random rand = new Random();
			DateTime startTime = DateTime.Now;
			List<FlightLog> logs = new List<FlightLog>();

			string[] weatherOptions = { "晴れ", "曇り", "雨", "雪" };

			for (int i = 0; i < numRows; i++)
			{
				logs.Add(new FlightLog
				{
					Timestamp = startTime.AddMilliseconds(200 * i),
					Speed = Math.Round(rand.NextDouble() * 180, 2), // 速度 (0-180 km/h)
					Distance = Math.Round(rand.NextDouble() * 1000, 2), // 走行距離 (0-1000 km)
					Throttle = rand.Next(0, 101), // アクセル踏み具合 (0-100%)
					EngineBrake = rand.Next(0, 101), // エンブレ (0-100%)
					Fuel = Math.Round(rand.NextDouble() * 100, 2), // ガソリン量 (0-100 L)
					DoorStatus = rand.Next(0, 2) == 1, // 扉開閉 (0=閉, 1=開)
					GPSLatitude = Math.Round(35.0 + rand.NextDouble() * 10, 6), // 緯度 (35.0 - 45.0)
					GPSLongitude = Math.Round(135.0 + rand.NextDouble() * 10, 6), // 経度 (135.0 - 145.0)
					Weather = weatherOptions[rand.Next(weatherOptions.Length)] // 天気 (ランダム)
				});
			}

			return logs;
		}
	}

}
