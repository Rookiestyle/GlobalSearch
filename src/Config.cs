using KeePass.Resources;
using PluginTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GlobalSearch
{
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

		public static List<FindInfo> FindList = new List<FindInfo>();

		static SearchHelp()
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

			foreach (FindInfo fi in FindList)
				PrepareFindInfoItem(fi);
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
			try { fi.tsiMenuItem = Tools.FindToolStripMenuItem(KeePass.Program.MainForm.MainMenu.Items, fi.Name, true); }
			catch { }
			if (fi.tsiMenuItem == null) return;

			fi.OptionsText = fi.tsiMenuItem.Text;
			fi.img = fi.tsiMenuItem.Image;
			Assembly a = typeof(KeePass.Program).Assembly;
			Type t = a.GetType("KeePass.Util.EntryUtil");
			fi.StandardMethod = t.GetMethod(fi.Func, BindingFlags.Static | BindingFlags.NonPublic);
		}
	}

	public static class Config
	{
		private const string ConfigActive = "GlobalSearch.HookSearchForm";
		public static bool SearchForm
		{
			get { return KeePass.Program.Config.CustomConfig.GetBool(ConfigActive, true); }
			set { KeePass.Program.Config.CustomConfig.SetBool(ConfigActive, value); }
		}

		private const string ConfigShowMultiDBInfoSearchForm = "GlobalSearch.ShowMultiDBInfoSearchForm";
		public static bool ShowMultiDBInfoSearchForm
		{
			get { return KeePass.Program.Config.CustomConfig.GetBool(ConfigShowMultiDBInfoSearchForm, true); }
			set { KeePass.Program.Config.CustomConfig.SetBool(ConfigShowMultiDBInfoSearchForm, value); }
		}

		private const string ConfigShowMultiDBInfoSingleSearch = "GlobalSearch.ShowMultiDBInfoSingleSearch";
		public static bool ShowMultiDBInfoSingleSearch
		{
			get { return KeePass.Program.Config.CustomConfig.GetBool(ConfigShowMultiDBInfoSingleSearch, true); }
			set { KeePass.Program.Config.CustomConfig.SetBool(ConfigShowMultiDBInfoSingleSearch, value); }
		}

		private const string ConfigHookSearchDupPw = "GlobalSearch.HookSearchDupPw";
		public static bool HookSearchDupPw
		{
			get { return KeePass.Program.Config.CustomConfig.GetBool(ConfigHookSearchDupPw, true); }
			set { KeePass.Program.Config.CustomConfig.SetBool(ConfigHookSearchDupPw, value); }
		}

		private const string ConfigHookSearchPwPairs = "GlobalSearch.HookSearchPwPairs";
		public static bool HookSearchPwPairs
		{
			get { return KeePass.Program.Config.CustomConfig.GetBool(ConfigHookSearchPwPairs, true); }
			set { KeePass.Program.Config.CustomConfig.SetBool(ConfigHookSearchPwPairs, value); }
		}

		private const string ConfigHookSearchPwCluster = "GlobalSearch.HookSearchPwCluster";
		public static bool HookSearchPwCluster
		{
			get { return KeePass.Program.Config.CustomConfig.GetBool(ConfigHookSearchPwCluster, true); }
			set { KeePass.Program.Config.CustomConfig.SetBool(ConfigHookSearchPwCluster, value); }
		}

		private const string ConfigHookPwQuality = "GlobalSearch.HookPwQuality";
		public static bool HookPwQuality
		{
			get { return KeePass.Program.Config.CustomConfig.GetBool(ConfigHookPwQuality, true); }
			set { KeePass.Program.Config.CustomConfig.SetBool(ConfigHookPwQuality, value); }
		}

		private const string ConfigHookLargeEntries = "GlobalSearch.HookLargeEntries";
		public static bool HookLargeEntries
		{
			get { return KeePass.Program.Config.CustomConfig.GetBool(ConfigHookLargeEntries, true); }
			set { KeePass.Program.Config.CustomConfig.SetBool(ConfigHookLargeEntries, value); }
		}

		private const string ConfigHookLastMod = "GlobalSearch.HookLastMod";
		public static bool HookLastMod
		{
			get { return KeePass.Program.Config.CustomConfig.GetBool(ConfigHookLastMod, true); }
			set { KeePass.Program.Config.CustomConfig.SetBool(ConfigHookLastMod, value); }
		}

		public static bool HookActive(string menuName)
		{
			switch (menuName)
			{
				case SearchHelp.SearchForm: return SearchForm;
				case SearchHelp.SearchDupPw: return HookSearchDupPw;
				case SearchHelp.SearchPasswordPairs: return HookSearchPwPairs;
				case SearchHelp.SearchPasswordClusters: return HookSearchPwCluster;
				case SearchHelp.SearchPasswordQuality: return HookPwQuality;
				case SearchHelp.SearchLargeEntries: return HookLargeEntries;
				case SearchHelp.SearchLastMod: return HookLastMod;
			}
			return true;
			throw new Exception("Invalid parameter: " + menuName);
		}
	}
}
