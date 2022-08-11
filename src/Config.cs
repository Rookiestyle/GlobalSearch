using KeePass.App.Configuration;
using System;
using System.Drawing;

namespace GlobalSearch
{
	public static class Config
	{
		private static AceCustomConfig CustomConfig = KeePass.Program.Config.CustomConfig;
		private const string ConfigActive = "GlobalSearch.HookSearchForm";
		private const string ConfigPWDisplay = "GlobalSearch.PWDisplay";
		private const string ConfigAllowResize = "GlobalSearch.AllowResize";
		private const string ConfigSearchResultSize = "GlobalSearch.SearchResultSize";
		private const string ConfigSearchResultLocation = "GlobalSearch.SearchResultLocation";

		public static bool SearchForm
		{
			get { return CustomConfig.GetBool(ConfigActive, true); }
			set { CustomConfig.SetBool(ConfigActive, value); m_SearchFormGlobalSession = value; }

		}

		private static bool? m_SearchFormGlobalSession = null;
		public static bool SearchFormGlobalSession
		{
			get 
			{ 
				if (!m_SearchFormGlobalSession.HasValue) m_SearchFormGlobalSession = SearchForm;
				return m_SearchFormGlobalSession.Value; ;
			}
			set { m_SearchFormGlobalSession = value; }
		}

		private const string ConfigShowMultiDBInfoSearchForm = "GlobalSearch.ShowMultiDBInfoSearchForm";
		public static bool ShowMultiDBInfoSearchForm
		{
			get { return CustomConfig.GetBool(ConfigShowMultiDBInfoSearchForm, true); }
			set { CustomConfig.SetBool(ConfigShowMultiDBInfoSearchForm, value); }
		}

		private const string ConfigShowMultiDBInfoSingleSearch = "GlobalSearch.ShowMultiDBInfoSingleSearch";
		public static bool ShowMultiDBInfoSingleSearch
		{
			get { return CustomConfig.GetBool(ConfigShowMultiDBInfoSingleSearch, true); }
			set { CustomConfig.SetBool(ConfigShowMultiDBInfoSingleSearch, value); }
		}

		private const string ConfigHookSearchDupPw = "GlobalSearch.HookSearchDupPw";
		public static bool HookSearchDupPw
		{
			get { return CustomConfig.GetBool(ConfigHookSearchDupPw, true); }
			set { CustomConfig.SetBool(ConfigHookSearchDupPw, value); }
		}

		private const string ConfigHookSearchPwPairs = "GlobalSearch.HookSearchPwPairs";
		public static bool HookSearchPwPairs
		{
			get { return CustomConfig.GetBool(ConfigHookSearchPwPairs, true); }
			set { CustomConfig.SetBool(ConfigHookSearchPwPairs, value); }
		}

		private const string ConfigHookSearchPwCluster = "GlobalSearch.HookSearchPwCluster";
		public static bool HookSearchPwCluster
		{
			get { return CustomConfig.GetBool(ConfigHookSearchPwCluster, true); }
			set { CustomConfig.SetBool(ConfigHookSearchPwCluster, value); }
		}

		private const string ConfigHookPwQuality = "GlobalSearch.HookPwQuality";
		public static bool HookPwQuality
		{
			get { return CustomConfig.GetBool(ConfigHookPwQuality, true); }
			set { CustomConfig.SetBool(ConfigHookPwQuality, value); }
		}

		private const string ConfigHookLargeEntries = "GlobalSearch.HookLargeEntries";
		public static bool HookLargeEntries
		{
			get { return CustomConfig.GetBool(ConfigHookLargeEntries, true); }
			set { CustomConfig.SetBool(ConfigHookLargeEntries, value); }
		}

		private const string ConfigHookLastMod = "GlobalSearch.HookLastMod";
		public static bool HookLastMod
		{
			get { return CustomConfig.GetBool(ConfigHookLastMod, true); }
			set { CustomConfig.SetBool(ConfigHookLastMod, value); }
		}

		private const string ConfigHookAllExpired = "GlobalSearch.HookAllExpired";
		public static bool HookAllExpired
		{
			get { return CustomConfig.GetBool(ConfigHookAllExpired, true); }
			set { CustomConfig.SetBool(ConfigHookAllExpired, value); }
		}

		public enum PasswordDisplayMode
		{
			Always,
			Never,
			EntryviewBased,
		}
		public static PasswordDisplayMode PasswordDisplay
		{
			get 
			{ 
				string s = CustomConfig.GetString(ConfigPWDisplay, string.Empty);
				if (string.IsNullOrEmpty(s))
				{
					PasswordDisplay = PasswordDisplayMode.Always;
					return PasswordDisplayMode.Always;
				}
				try 
				{ 
					PasswordDisplayMode r = (PasswordDisplayMode)Enum.Parse(typeof(PasswordDisplayMode), s);
					return r;
				}
				catch 
				{
					PasswordDisplay = PasswordDisplayMode.Always;
					return PasswordDisplayMode.Always;
				}
			}
			set { CustomConfig.SetString(ConfigPWDisplay, value.ToString()); }
		}

        public static bool AllowResize 
		{
			get { return CustomConfig.GetBool(ConfigAllowResize, true); }
			set { CustomConfig.SetBool(ConfigAllowResize, value); }
		}

		public static Size SearchResultSize
        {
			get
            {
				string sSizeString = CustomConfig.GetString(ConfigSearchResultSize, "-9999/-9999");
				try
                {
					Size s = ResizableListViewForm.NoSize;
					string[] aSizeString = sSizeString.Split(new char[] { '/' });
					int i;
					if (!int.TryParse(aSizeString[0], out i)) i = -9999;
					s.Width = i;
					if (!int.TryParse(aSizeString[1], out i)) i = -9999;
					s.Height = i;
					return s;
				}
				catch { return ResizableListViewForm.NoSize; }
            }
			set { 
				CustomConfig.SetString(ConfigSearchResultSize, value.Width.ToString() + "/" + value.Height.ToString()); }
        }

		public static Point SearchResultLocation
		{
			get
			{
				string sPointString = CustomConfig.GetString(ConfigSearchResultLocation, "-9999/-9999");
				try
				{
					Point p = ResizableListViewForm.NoLocation;
					string[] aSizeString = sPointString.Split(new char[] { '/' });
					int i;
					if (!int.TryParse(aSizeString[0], out i)) i = -9999;
					p.X = i;
					if (!int.TryParse(aSizeString[1], out i)) i = -9999;
					p.Y = i;
					return p;
				}
				catch { return ResizableListViewForm.NoLocation; }
			}
			set { 
				CustomConfig.SetString(ConfigSearchResultLocation, value.X.ToString() + "/" + value.Y.ToString()); }
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
				case SearchHelp.SearchExpired: return HookAllExpired;
				case SearchHelp.SearchExpired_1D: return HookAllExpired;
				case SearchHelp.SearchExpired_2D: return HookAllExpired;
				case SearchHelp.SearchExpired_3D: return HookAllExpired;
				case SearchHelp.SearchExpired_7D: return HookAllExpired;
				case SearchHelp.SearchExpired_14D: return HookAllExpired;
				case SearchHelp.SearchExpired_1M: return HookAllExpired;
				case SearchHelp.SearchExpired_2M: return HookAllExpired;
				case SearchHelp.SearchExpired_F: return HookAllExpired;
			}
			return true;
			throw new Exception("Invalid parameter: " + menuName);
		}
	}
}