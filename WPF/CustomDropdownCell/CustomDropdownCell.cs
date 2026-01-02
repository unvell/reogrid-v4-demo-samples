using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using unvell.ReoGrid;
using unvell.ReoGrid.CellTypes;

namespace ReoGridWPFSample
{
	public class CustomDropdownCell : DropdownCell
	{
		private readonly FrameworkElement dropdownContent;
		private readonly TreeView tree;

		public CustomDropdownCell(IEnumerable<GroupNode> groups)
		{
			dropdownContent = CreateContentFromTemplate()
				?? throw new InvalidOperationException("GroupedDropdownTreeTemplate からコンテンツを生成できませんでした。");

			tree = FindTreeView(dropdownContent)
				?? throw new InvalidOperationException("テンプレート内に TreeView が見つかりません。");

			tree.ItemsSource = groups;
			tree.SelectedItemChanged += OnSelectedItemChanged;

			DropdownControl = dropdownContent;
			MinimumDropdownWidth = 280;
		}

		public bool UseActionForDataUpdate { get; set; } = true;

		private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (e.NewValue is ItemNode item)
			{
				if (UseActionForDataUpdate)
				{
					base.Worksheet?.DoAction(new unvell.ReoGrid.Actions.SetCellDataAction(Cell.Position, item.ItemName));
				}
				else
				{
					Cell.Data = item.ItemName;
				}

				PullUp();
			}
		}

		private static FrameworkElement? CreateContentFromTemplate()
		{
			var template = (DataTemplate?)Application.Current.TryFindResource("GroupedDropdownTreeTemplate");
			return template?.LoadContent() as FrameworkElement;
		}

		private static TreeView? FindTreeView(object? root)
		{
			if (root is TreeView tv) return tv;
			if (root is DependencyObject dep)
			{
				foreach (var child in LogicalTreeHelper.GetChildren(dep))
				{
					var found = FindTreeView(child);
					if (found != null) return found;
				}
			}
			return null;
		}
	}

	public class CustomDropdownCellWithPanel : DropdownCell
	{
		private readonly MyDropdownPanel panel;

		public CustomDropdownCellWithPanel(IEnumerable<GroupNode> groups)
		{
			panel = new MyDropdownPanel
			{
				Items = groups.ToList(),

				// Set width and height to Auto, so that the panel resizes based on its content, make it scrollable when too tall.
				Width = Double.NaN,
				Height = Double.NaN,
			};

			panel.Tree.SelectedItemChanged += OnSelectedItemChanged;

			DropdownControl = panel;
			MinimumDropdownWidth = 280;
		}

		public bool UseActionForDataUpdate { get; set; } = true;

		private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (e.NewValue is ItemNode item)
			{
				if (UseActionForDataUpdate)
				{
					base.Worksheet?.DoAction(new unvell.ReoGrid.Actions.SetCellDataAction(Cell.Position, item.ItemName));
				}
				else
				{
					Cell.Data = item.ItemName;
				}

				PullUp();
			}
		}
	}

	public sealed class GroupOption
	{
		public string GroupName { get; init; } = string.Empty;
		public IReadOnlyList<string> Items { get; init; } = Array.Empty<string>();
	}

	public sealed class GroupNode
	{
		public string GroupName { get; }
		public IReadOnlyList<ItemNode> Items { get; }

		public GroupNode(GroupOption option)
		{
			GroupName = option.GroupName;
			Items = option.Items
					.Where(x => !string.IsNullOrWhiteSpace(x))
					.Select(x => new ItemNode(option.GroupName, x))
					.ToList();
		}
	}

	public sealed class ItemNode
	{
		public string GroupName { get; }
		public string ItemName { get; }

		public ItemNode(string groupName, string itemName)
		{
			GroupName = groupName;
			ItemName = itemName;
		}
	}
}
