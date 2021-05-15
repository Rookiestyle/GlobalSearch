using KeePass.UI;
using System;
using System.Windows.Forms;

namespace PluginTools
{
	public static partial class Tools_ListView_Sortable
	{
		public static void RS_Sortable(this ListView lv, bool bSortable)
		{
			if (lv == null) return;
			if (!bSortable)
			{
				lv.ListViewItemSorter = null;
				return;
			}
			if (lv.ListViewItemSorter != null) return;
			lv.ListViewItemSorter = new ListSorter() { CompareNaturally = true };
			lv.ColumnClick += DoSort;
			lv.HeaderStyle = ColumnHeaderStyle.Clickable;
		}

		private static void DoSort(object sender, ColumnClickEventArgs e)
		{
			var lv = sender as ListView;
			if (lv == null || lv.ListViewItemSorter == null) return;
			var ls = lv.ListViewItemSorter as ListSorter;
			DateTime dt;
			ls.CompareTimes = DateTime.TryParse(lv.Items[0].SubItems[e.Column].Text, out dt);
			if (e.Column == ls.Column)
			{
				if (ls.Order == SortOrder.Ascending) ls.Order = SortOrder.Descending;
				else ls.Order = SortOrder.Ascending;
			}
			else
			{
				ls.Column = e.Column;
				ls.Order = SortOrder.Ascending;
			}
			lv.Sort();
			lv.UpdateColumnSortingIcons(ls);
		}

		private static void UpdateColumnSortingIcons(this ListView lv, ListSorter ls)
		{
			if (lv == null) return;
			if (UIUtil.SetSortIcon(lv, ls.Column, ls.Order)) return;

			// if(m_lvEntries.SmallImageList == null) return;

			if (ls.Column < 0) return;

			string strAsc = "  \u2191"; // Must have same length
			string strDsc = "  \u2193"; // Must have same length
			if (KeePass.Util.WinUtil.IsWindows9x || KeePass.Util.WinUtil.IsWindows2000 || KeePass.Util.WinUtil.IsWindowsXP ||
				KeePassLib.Native.NativeLib.IsUnix())
			{
				strAsc = @"  ^";
				strDsc = @"  v";
			}
			else if (KeePass.Util.WinUtil.IsAtLeastWindowsVista)
			{
				strAsc = "  \u25B3";
				strDsc = "  \u25BD";
			}

			foreach (ColumnHeader ch in lv.Columns)
			{
				string strCur = ch.Text, strNew = null;

				if (strCur.EndsWith(strAsc) || strCur.EndsWith(strDsc))
				{
					strNew = strCur.Substring(0, strCur.Length - strAsc.Length);
					strCur = strNew;
				}

				if ((ch.Index == ls.Column) &&
					(ls.Order != SortOrder.None))
				{
					if (ls.Order == SortOrder.Ascending)
						strNew = strCur + strAsc;
					else if (ls.Order == SortOrder.Descending)
						strNew = strCur + strDsc;
				}

				if (strNew != null) ch.Text = strNew;
			}
		}
	}
}