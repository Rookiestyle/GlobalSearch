﻿using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

using KeePass.Plugins;
using KeePass.UI;
using KeePassLib;

using PluginTranslation;
using PluginTools;

namespace GlobalSearch
{
	/// <summary>
	/// Description of Options.
	/// </summary>
	/// 
	public partial class Options : UserControl
	{
		public Options()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			Text = PluginTranslate.PluginName;
			tpOptions.Text = PluginTranslate.OptionsCaption;
			tpHelp.Text = KeePass.Resources.KPRes.Description;
			AdjustCheckBox(cbSearchForm, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchForm));
			cbUseEntryListColumnWidths.Text = PluginTranslate.UseEntryListColumnWidths;

            AdjustCheckBox(cbSearchLastMod, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchLastMod));
			AdjustCheckBox(cbSearchLarge, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchLargeEntries));
			AdjustCheckBox(cbSearchDupPw, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchDupPw));
			AdjustCheckBox(cbSearchPwPairs, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchPasswordPairs));
			AdjustCheckBox(cbSearchPwCluster, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchPasswordClusters));
			AdjustCheckBox(cbSearchPwQuality, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchPasswordQuality));
			AdjustCheckBox(cbSearchPwQuality, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchPasswordQuality));

			FindInfo fiExpired = SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchExpired);
			cbSearchAllExpired.Enabled = fiExpired.StandardMethod != null;
			cbSearchAllExpired.Text = KeePass.Resources.KPRes.ExpiredEntries;

			cbMultiDBSearchInfoSearchFormActive.Text = string.Format(PluginTranslate.MultiDBSearchInfoSearchFormActive, cbSearchForm.Text);
			cbMultiDBSearchInfoSingleSearchActive.Text = PluginTranslate.MultiDBSearchInfoSingleSearchActive;
			string sDesc = string.Format(PluginTranslate.Description, PluginTranslate.PluginName, cbSearchForm.Text);
			tbDesc.Lines = sDesc.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

			gSearches.Text = KeePass.Resources.KPRes.SearchingOp;
			gPWDisplay.Text = PluginTranslate.PWDisplayMode;	
			rbPWDisplayAlways.Text = PluginTranslate.PWDisplayModeAlways;
			rbPWDisplayNever.Text = PluginTranslate.PWDisplayModeNever;
			rbPWDisplayEntryList.Text = PluginTranslate.PWDisplayModeEntryView;
		}

		private void AdjustCheckBox(CheckBox cbBox, FindInfo fiInfo)
		{
			cbBox.Text = fiInfo.OptionsText.Replace("&", string.Empty);
			cbBox.Enabled = fiInfo.StandardMethod != null || (fiInfo.Name == SearchHelp.SearchForm);
		}

		public void SetPwDisplayMode(Config.PasswordDisplayMode m)
		{
			rbPWDisplayAlways.Checked = true;
			if (m == Config.PasswordDisplayMode.Never) rbPWDisplayNever.Checked = true;
			if (m == Config.PasswordDisplayMode.EntryviewBased) rbPWDisplayEntryList.Checked = true;
		}

		public Config.PasswordDisplayMode GetPwDisplayMode()
		{
			if (rbPWDisplayNever.Checked) return Config.PasswordDisplayMode.Never;
			if (rbPWDisplayEntryList.Checked) return Config.PasswordDisplayMode.EntryviewBased;
			return Config.PasswordDisplayMode.Always;
		}
	}
}





















