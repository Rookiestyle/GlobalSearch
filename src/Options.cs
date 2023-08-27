using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePass.UI;
using KeePassLib;
using PluginTools;
using PluginTranslation;

namespace GlobalSearch
{
  /// <summary>
  /// Description of Options.
  /// </summary>
  /// 
  public partial class Options : UserControl
  {
    private TreeNode tvnSearchForm { get { return tvHookedSearches.Nodes.Find("tvnSearchForm", true)[0]; } }
    private TreeNode tvnUseEntryListColummnWidth { get { return tvHookedSearches.Nodes.Find("tvnUseEntryListColummnWidth", true)[0]; } }
    private TreeNode tvnLastMod { get { return tvHookedSearches.Nodes.Find("tvnLastMod", true)[0]; } }
    private TreeNode tvnLargeEntries { get { return tvHookedSearches.Nodes.Find("tvnLargeEntries", true)[0]; } }
    private TreeNode tvnDupPw { get { return tvHookedSearches.Nodes.Find("tvnDupPw", true)[0]; } }
    private TreeNode tvnSimPwPairs { get { return tvHookedSearches.Nodes.Find("tvnSimPwPairs", true)[0]; } }
    private TreeNode tvnSimPwCluster { get { return tvHookedSearches.Nodes.Find("tvnSimPwCluster", true)[0]; } }
    private TreeNode tvnPwQuality { get { return tvHookedSearches.Nodes.Find("tvnPwQuality", true)[0]; } }
    private TreeNode tvnExpired { get { return tvHookedSearches.Nodes.Find("tvnExpired", true)[0]; } }

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
      AdjustNode(tvnSearchForm, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchForm));

      tvnUseEntryListColummnWidth.Text = PluginTranslate.UseEntryListColumnWidths;

      AdjustNode(tvnLastMod, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchLastMod));
      AdjustNode(tvnLargeEntries, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchLargeEntries));
      AdjustNode(tvnDupPw, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchDupPw));
      AdjustNode(tvnSimPwPairs, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchPasswordPairs));
      AdjustNode(tvnSimPwCluster, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchPasswordClusters));
      AdjustNode(tvnPwQuality, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchPasswordQuality));
      AdjustNode(tvnExpired, SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchExpired));
      tvnExpired.Text = KeePass.Resources.KPRes.ExpiredEntries;

      cbMultiDBSearchInfoSearchFormActive.Text = string.Format(PluginTranslate.MultiDBSearchInfoSearchFormActive, tvnSearchForm.Text);
      cbMultiDBSearchInfoSingleSearchActive.Text = PluginTranslate.MultiDBSearchInfoSingleSearchActive;
      string sDesc = string.Format(PluginTranslate.Description, PluginTranslate.PluginName, tvnSearchForm.Text);
      tbDesc.Lines = sDesc.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

      gSearches.Text = KeePass.Resources.KPRes.SearchingOp;
      gPWDisplay.Text = PluginTranslate.PWDisplayMode;
      rbPWDisplayAlways.Text = PluginTranslate.PWDisplayModeAlways;
      rbPWDisplayNever.Text = PluginTranslate.PWDisplayModeNever;
      rbPWDisplayEntryList.Text = PluginTranslate.PWDisplayModeEntryView;
    }

    private void SetDisabled(TreeNode t)
    {
      t.NodeFont = new Font(t.NodeFont == null ? t.TreeView.Font : t.NodeFont, FontStyle.Strikeout);
    }

    private bool IsEnabled(TreeNode t)
    {
      return t.NodeFont == null || !t.NodeFont.Strikeout;
    }

    public void InitEx()
    {
      tvnSearchForm.Checked = Config.SearchForm;
      tvnUseEntryListColummnWidth.Checked = Config.UseEntryListColumnWidths;
      tvnDupPw.Checked = IsEnabled(tvnDupPw) && Config.HookSearchDupPw;
      tvnSimPwPairs.Checked = IsEnabled(tvnSimPwPairs) && Config.HookSearchPwPairs;
      tvnSimPwCluster.Checked = IsEnabled(tvnSimPwCluster) && Config.HookSearchPwCluster;
      tvnPwQuality.Checked = IsEnabled(tvnPwQuality) && Config.HookPwQuality;
      tvnLargeEntries.Checked = IsEnabled(tvnLargeEntries) && Config.HookLargeEntries;
      tvnLastMod.Checked = IsEnabled(tvnLastMod) && Config.HookLastMod;
      tvnExpired.Checked = IsEnabled(tvnExpired) && Config.HookAllExpired;
      cbMultiDBSearchInfoSearchFormActive.Checked = Config.ShowMultiDBInfoSearchForm;
      cbMultiDBSearchInfoSingleSearchActive.Checked = Config.ShowMultiDBInfoSingleSearch;
      SetPwDisplayMode(Config.PasswordDisplay);
    }

    public void UpdateConfig()
    {
      Config.SearchForm = tvnSearchForm.Checked;
      Config.UseEntryListColumnWidths = tvnUseEntryListColummnWidth.Checked;
      if (IsEnabled(tvnDupPw)) Config.HookSearchDupPw = tvnDupPw.Checked;
      if (IsEnabled(tvnSimPwPairs)) Config.HookSearchPwPairs = tvnSimPwPairs.Checked;
      if (IsEnabled(tvnSimPwCluster)) Config.HookSearchPwCluster = tvnSimPwCluster.Checked;
      if (IsEnabled(tvnPwQuality)) Config.HookPwQuality = tvnPwQuality.Checked;
      if (IsEnabled(tvnLargeEntries)) Config.HookLargeEntries = tvnLargeEntries.Checked;
      if (IsEnabled(tvnLastMod)) Config.HookLastMod = tvnLastMod.Checked;
      if (IsEnabled(tvnExpired)) Config.HookAllExpired = tvnExpired.Checked;
      Config.ShowMultiDBInfoSearchForm = cbMultiDBSearchInfoSearchFormActive.Checked;
      Config.ShowMultiDBInfoSingleSearch = cbMultiDBSearchInfoSingleSearchActive.Checked;
      Config.PasswordDisplay = GetPwDisplayMode();
    }

    private void AdjustNode(TreeNode t, FindInfo fiInfo)
    {
      t.Text = fiInfo.OptionsText.Replace("&", string.Empty);
      if (fiInfo.StandardMethod != null || (fiInfo.Name == SearchHelp.SearchForm)) return;
      SetDisabled(t);
    }

    private void SetPwDisplayMode(Config.PasswordDisplayMode m)
    {
      gPWDisplay.Visible = gPWDisplay.Enabled = Tools.KeePassVersion < Util.KeePassVersion_2_54;
      rbPWDisplayAlways.Checked = true;
      if (m == Config.PasswordDisplayMode.Never) rbPWDisplayNever.Checked = true;
      if (m == Config.PasswordDisplayMode.EntryviewBased) rbPWDisplayEntryList.Checked = true;
    }

    private Config.PasswordDisplayMode GetPwDisplayMode()
    {
      if (rbPWDisplayNever.Checked) return Config.PasswordDisplayMode.Never;
      if (rbPWDisplayEntryList.Checked) return Config.PasswordDisplayMode.EntryviewBased;
      return Config.PasswordDisplayMode.Always;
    }

    private void tvHookedSearches_BeforeChecked(object sender, TreeViewCancelEventArgs e)
    {
      if (IsEnabled(e.Node)) return;
      e.Cancel = true;
    }
  }
}





















