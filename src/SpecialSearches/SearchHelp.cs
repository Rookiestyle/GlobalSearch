using KeePass.Resources;
using KeePassLib.Interfaces;
using PluginTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace GlobalSearch
{
	public enum SearchType
	{
		BuiltIn,
		Expiring,
		QuickFind, //To-Do
	}

	public class FindInfo
	{
		public string Name = string.Empty;
		public string Func = string.Empty;
		public Image img = null;
		public string Title = string.Empty;
		public string SubTitle = string.Empty;
		public string Note = string.Empty;
		public string NothingFound = string.Empty;
		public string OptionsText = string.Empty;
		public ToolStripItem tsiMenuItem = null;
		public MethodInfo StandardMethod = null;
		public List<Delegate> StandardEventHandlers = null;
		public SearchType SearchType = SearchType.BuiltIn;

		public override string ToString()
		{
			int i = StandardEventHandlers == null ? 0 : StandardEventHandlers.Count;
			return Name + ": " + Func + ": " + i.ToString() + " handlers";
		}
	}

	public static class SearchHelp
	{
		public const string SearchForm = "m_menuFindInDatabase";
		public const string SearchDupPw = "m_menuFindDupPasswords";
		public const string SearchPasswordPairs = "m_menuFindSimPasswordsP";
		public const string SearchPasswordClusters = "m_menuFindSimPasswordsC";
		public const string SearchPasswordQuality = "m_menuFindPwQuality";
		public const string SearchLargeEntries = "m_menuFindLarge";
		public const string SearchLastMod = "m_menuFindLastMod";

		public const string SearchExpired = "m_menuFindExp";
		public const string SearchExpired_1D = "m_menuFindExp1";
		public const string SearchExpired_2D = "m_menuFindExp2";
		public const string SearchExpired_3D = "m_menuFindExp3";
		public const string SearchExpired_7D = "m_menuFindExp7";
		public const string SearchExpired_14D = "m_menuFindExp14";
		public const string SearchExpired_1M = "m_menuFindExp30";
		public const string SearchExpired_2M = "m_menuFindExp60";
		public const string SearchExpired_F = "m_menuFindExpInF";


		public static List<FindInfo> FindList = new List<FindInfo>();

		static SearchHelp()
		{
			AddStandardSearches();

			AddExpiredSearches();

			foreach (FindInfo fi in FindList)
				PrepareFindInfoItem(fi);
		}

		private static void AddExpiredSearches()
		{
			FindList.Add(new FindInfo()
			{
				Name = SearchExpired,
				Func = "OnFindExp",
				Title = KPRes.ExpiredEntries,
				SubTitle = string.Empty,
				Note = null,
				SearchType = SearchType.Expiring,
			});

			FindList.Add(new FindInfo()
			{
				Name = SearchExpired_1D,
				Func = "OnFindExp1",
				Title = KPRes.ExpiredEntries,
				SubTitle = string.Empty,
				Note = null,
				SearchType = SearchType.Expiring,
			});


			FindList.Add(new FindInfo()
			{
				Name = SearchExpired_2D,
				Func = "OnFindExp2",
				Title = KPRes.ExpiredEntries,
				SubTitle = string.Empty,
				Note = null,
				SearchType = SearchType.Expiring,
			});


			FindList.Add(new FindInfo()
			{
				Name = SearchExpired_3D,
				Func = "OnFindExp3",
				Title = KPRes.ExpiredEntries,
				SubTitle = string.Empty,
				Note = null,
				SearchType = SearchType.Expiring,
			});

			FindList.Add(new FindInfo()
			{
				Name = SearchExpired_7D,
				Func = "OnFindExp7",
				Title = KPRes.ExpiredEntries,
				SubTitle = string.Empty,
				Note = null,
				SearchType = SearchType.Expiring,
			});


			FindList.Add(new FindInfo()
			{
				Name = SearchExpired_14D,
				Func = "OnFindExp14",
				Title = KPRes.ExpiredEntries,
				SubTitle = string.Empty,
				Note = null,
				SearchType = SearchType.Expiring,
			});


			FindList.Add(new FindInfo()
			{
				Name = SearchExpired_1M,
				Func = "OnFindExp30",
				Title = KPRes.ExpiredEntries,
				SubTitle = string.Empty,
				Note = null,
				SearchType = SearchType.Expiring,
			});


			FindList.Add(new FindInfo()
			{
				Name = SearchExpired_2M,
				Func = "OnFindExp60",
				Title = KPRes.ExpiredEntries,
				SubTitle = string.Empty,
				Note = null,
				SearchType = SearchType.Expiring,
			});


			FindList.Add(new FindInfo()
			{
				Name = SearchExpired_F,
				Func = "OnFindExpInF",
				Title = KPRes.ExpiredEntries,
				SubTitle = string.Empty,
				Note = null,
				SearchType = SearchType.Expiring,
			});
		}

		private static void AddStandardSearches()
		{
			FindList.Add(new FindInfo
			{
				Name = SearchDupPw,
				Func = "FindDuplicatePasswords",
				Title = KPRes.DuplicatePasswords,
				SubTitle = KPRes.DuplicatePasswordsList,
				Note = null,
				NothingFound = KPRes.DuplicatePasswordsNone
			});

			FindList.Add(new FindInfo
			{
				Name = SearchPasswordPairs,
				Func = "FindSimilarPasswordsP",
				Title = KPRes.SimilarPasswords,
				SubTitle = KPRes.SimilarPasswordsList2,
				Note = KPRes.SimilarPasswordsNoDup
			});

			FindList.Add(new FindInfo
			{
				Name = SearchPasswordClusters,
				Func = "FindSimilarPasswordsC",
				Title = KPRes.SimilarPasswords,
				SubTitle = KPRes.ClusterCenters2,
				Note = KPRes.ClusterCentersDesc
			});

			FindList.Add(new FindInfo
			{
				Name = SearchPasswordQuality,
				Func = "CreatePwQualityList",
				Title = KPRes.PasswordQuality,
				SubTitle = KPRes.PasswordQualityReport2,
				Note = null
			});

			FindList.Add(new FindInfo
			{
				Name = SearchLargeEntries,
				Func = "FindLargeEntries",
				Title = KPRes.LargeEntries,
				SubTitle = KPRes.LargeEntriesList,
				Note = null
			});

			FindList.Add(new FindInfo
			{
				Name = SearchLastMod,
				Func = "FindLastModEntries",
				Title = KPRes.LastModified,
				SubTitle = KPRes.LastModified,
				Note = null
			});

			FindList.Add(new FindInfo
			{
				Name = SearchForm,
				OptionsText = KPRes.Search
			});
		}

		private static void PrepareFindInfoItem(FindInfo fi)
		{
			if (string.IsNullOrEmpty(fi.OptionsText))
			{
				if (string.IsNullOrEmpty(fi.Title))
					fi.OptionsText = fi.Name;
				else
					fi.OptionsText = fi.Title;
			}

			if (fi.SearchType != SearchType.QuickFind)
			{
				try { fi.tsiMenuItem = Tools.FindToolStripMenuItem(KeePass.Program.MainForm.MainMenu.Items, fi.Name, true); }
				catch { }
				if (fi.tsiMenuItem == null) return;

				fi.OptionsText = fi.tsiMenuItem.Text;
				fi.img = fi.tsiMenuItem.Image;
			}

			if (fi.SearchType == SearchType.BuiltIn)
			{
				Assembly a = typeof(KeePass.Program).Assembly;
				Type t = a.GetType("KeePass.Util.EntryUtil");
				fi.StandardMethod = t.GetMethod(fi.Func, BindingFlags.Static | BindingFlags.NonPublic);
			}
			else if (fi.SearchType == SearchType.Expiring)
			{
				if (!string.IsNullOrEmpty(fi.OptionsText))
				{
					fi.Title = fi.OptionsText;
					try { fi.Title = fi.tsiMenuItem.OwnerItem.Text + " - " + fi.OptionsText; }
					catch { }
					fi.Title = KeePassLib.Utility.StrUtil.RemoveAccelerator(fi.Title);
				}
				fi.StandardMethod = typeof(SearchHelp).GetMethod("FindWrapper", BindingFlags.Static | BindingFlags.NonPublic);
			}
		}

		internal static List<object> FindWrapper(KeePassLib.PwDatabase db,
				IStatusLogger sl, out Action<ListView> fInit, FindInfo fi)
		{
			if (fi.Name == SearchExpired) return SpecialSearches.SearchExpired.FindExpired(db, sl, out fInit, true, 0, 0);
			else if (fi.Name == SearchExpired_1D) return SpecialSearches.SearchExpired.FindExpired(db, sl, out fInit, false, 1, 0);
			else if (fi.Name == SearchExpired_2D) return SpecialSearches.SearchExpired.FindExpired(db, sl, out fInit, false, 2, 0);
			else if (fi.Name == SearchExpired_3D) return SpecialSearches.SearchExpired.FindExpired(db, sl, out fInit, false, 3, 0);
			else if (fi.Name == SearchExpired_7D) return SpecialSearches.SearchExpired.FindExpired(db, sl, out fInit, false, 7, 0);
			else if (fi.Name == SearchExpired_14D) return SpecialSearches.SearchExpired.FindExpired(db, sl, out fInit, false, 14, 0);
			else if (fi.Name == SearchExpired_1M) return SpecialSearches.SearchExpired.FindExpired(db, sl, out fInit, false, 0, 1);
			else if (fi.Name == SearchExpired_2M) return SpecialSearches.SearchExpired.FindExpired(db, sl, out fInit, false, 0, 2);
			else if (fi.Name == SearchExpired_F) return SpecialSearches.SearchExpired.FindExpired(db, sl, out fInit, false, int.MaxValue, 0);

			List<object> l = null;
			fInit = null;
			return l;
		}

		public static string GetDBName(KeePassLib.PwEntry pe)
		{
			if (pe == null) return string.Empty;
			KeePassLib.PwDatabase db = KeePass.Program.MainForm.DocumentManager.FindContainerOf(pe);
			if (db == null) return string.Empty;
			if (!string.IsNullOrEmpty(db.Name)) return db.Name;
			return KeePassLib.Utility.UrlUtil.GetFileName(db.IOConnectionInfo.Path);
		}
	}
}
