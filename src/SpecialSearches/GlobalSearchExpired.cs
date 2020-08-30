using KeePass.Resources;
using KeePass.UI;
using KeePassLib;
using KeePassLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GlobalSearch.SpecialSearches
{
	class SearchExpired
	{
		internal static List<object> FindExpired(PwDatabase db, IStatusLogger sl, out Action<ListView> fInit, bool bOnlyExpired, int iExpDays, int iExpMonths)
		{
			List<object> l = new List<object>();
			fInit = PrepareListView();

			PwGroup pg = new PwGroup(true, true, string.Empty, PwIcon.Expired);
			pg.IsVirtual = true;

			bool bExpInP = bOnlyExpired; // Past
			bool bExpInF = (iExpDays == int.MaxValue); // Future
			bool bExpInI = !bExpInP && !bExpInF; // Interval

			DateTime dtNow = DateTime.UtcNow;
			DateTime dtLimit = dtNow;
			if (bExpInI)
			{
				if (iExpDays > 0) dtLimit = dtNow.AddDays(iExpDays);
				else if (iExpMonths > 0) dtLimit = dtNow.AddMonths(iExpMonths);

				dtLimit = KeePassLib.Utility.TimeUtil.ToLocal(dtLimit, false);
				dtLimit = dtLimit.Date.Add(new TimeSpan(23, 59, 59));
				dtLimit = KeePassLib.Utility.TimeUtil.ToUtc(dtLimit, false);
			}

			KeePassLib.Delegates.EntryHandler eh = delegate (PwEntry pe)
			{
				if (!pe.Expires) return true;
				if (!pe.GetSearchingEnabled()) return true;
				if (PwDefs.IsTanEntry(pe)) return true; // Exclude TANs

				int iRelNow = pe.ExpiryTime.CompareTo(dtNow);

				if ((bExpInP && (iRelNow <= 0)) ||
					(bExpInI && (pe.ExpiryTime <= dtLimit) && (iRelNow > 0)) ||
					(bExpInF && (iRelNow > 0)))
					pg.AddEntry(pe, false, false);
				return true;
			};

			db.RootGroup.TraverseTree(TraversalMethod.PreOrder, null, eh);

			if (pg.Entries.UCount == 0) return l;

			l = MapResults(pg);
			return l;
		}

		private static List<object> MapResults(PwGroup pg)
		{
			List<object> l = new List<object>();
			Dictionary<PwDatabase, ListViewGroup> dGroups = new Dictionary<PwDatabase, ListViewGroup>();
			foreach (PwEntry pe in pg.Entries)
			{
				string strGroup = string.Empty;
				if (pe.ParentGroup != null)	strGroup = pe.ParentGroup.GetFullPath(" - ", false);

				ListViewGroup lvg = null;
				PwDatabase dbReal = KeePass.Program.MainForm.DocumentManager.SafeFindContainerOf(pe);
				if (!dGroups.ContainsKey(dbReal))
				{
					lvg = new ListViewGroup(SearchHelp.GetDBName(pe));
					lvg.Tag = new PwGroup(true, true, lvg.Header, PwIcon.Expired) { IsVirtual = true, };
					l.Add(lvg);
				}
				else lvg = dGroups[dbReal];
				dGroups[dbReal] = lvg;


				ListViewItem lvi = new ListViewItem(pe.Strings.ReadSafe(PwDefs.TitleField));
				lvi.SubItems.Add(pe.Strings.ReadSafe(PwDefs.UserNameField));
				lvi.SubItems.Add(pe.ExpiryTime.ToLocalTime().ToString());
				lvi.SubItems.Add(strGroup);

				lvi.Tag = pe;
				(lvg.Tag as PwGroup).AddEntry(pe, false, false);
				l.Add(lvi);
			}
			return l;
		}

		private static Action<ListView> PrepareListView()
		{
			return delegate (ListView lv)
			{
				int w = lv.ClientSize.Width - UIUtil.GetVScrollBarWidth();
				int wf = w / 4;
				int di = Math.Min(UIUtil.GetSmallIconSize().Width, wf);

				lv.Columns.Add(KPRes.Title, wf + di);
				lv.Columns.Add(KPRes.UserName, wf);
				lv.Columns.Add(KPRes.ExpiryTime, wf);
				lv.Columns.Add(KPRes.Group, wf - di);

				UIUtil.SetDisplayIndices(lv, new int[] { 1, 2, 3, 0 });
			};
		}
	}
}
