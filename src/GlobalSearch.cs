using KeePass.App.Configuration;
using KeePass.Forms;
using KeePass.Plugins;
using KeePass.Resources;
using KeePass.UI;
using KeePassLib;
using KeePassLib.Interfaces;
using PluginTools;
using PluginTranslation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GlobalSearch
{
	public sealed class GlobalSearchExt : Plugin
	{
		private IPluginHost m_host = null;
		private ToolStripMenuItem m_menu = null;
		private Image m_img = null;

		private SearchForm m_sf = null;
		private Button m_btnOK = null;
		private CheckBox m_cbSearchAllDatabases = null;
		private List<AceColumn> m_lEntryListColumns = null;
		private MethodInfo m_miUpdateColumnsEx = null;
		private MethodInfo m_miGetEntryFieldEx = null;
		private MethodInfo m_miSprCompileFn = null;
		private Dictionary<PwDatabase, PwGroup> m_dDBGroups = new Dictionary<PwDatabase, PwGroup>();

		private ListView m_lvEntries = null;

		private Action<ListView> m_aStandardLvInit = null;

		public override bool Initialize(IPluginHost host)
		{
			m_host = host;
			PluginTranslate.Init(this, KeePass.Program.Translation.Properties.Iso6391Code);
			Tools.DefaultCaption = PluginTranslate.PluginName;
			Tools.PluginURL = "https://github.com/rookiestyle/globalsearch/";

			GetStandardMethods();

			m_lvEntries = (ListView)Tools.GetControl("m_lvEntries");
			if (m_lvEntries == null) PluginDebug.AddError("Could not get 'm_lvEntries'", 0);

			Tools.OptionsFormShown += Tools_OptionsFormShown;
			Tools.OptionsFormClosed += Tools_OptionsFormClosed;

			Activate();
			FindInfo fi = SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchForm);
			if (fi != null) m_img = fi.img;
			if (m_img == null)
			{
				try { m_img = (Image)KeePass.Program.Resources.GetObject("B16x16_XMag"); }
				catch { }
			}
			if (m_img == null) m_img = m_host.MainWindow.ClientIcons.Images[(int)PwIcon.EMailSearch];

			m_menu = new ToolStripMenuItem();
			m_menu.Click += OnShowOptions;
			m_menu.Text = PluginTranslate.PluginName + "...";
			m_menu.Image = m_img;
			m_host.MainWindow.ToolsMenu.DropDownItems.Add(m_menu);

			return true;
		}

		#region Search form
		private void OnSearchFormAdded(object sender, GwmWindowEventArgs e)
		{
			if (e.Form is SearchForm)
			{
				OnSearchFormClosed(null, null);
				int iOpenDB = m_host.MainWindow.DocumentManager.GetOpenDatabases().Count;
				StackFrame[] sf = new StackTrace().GetFrames();
				bool bSFRelevant = false;

				List<string> lSF = new List<string>();
				foreach (StackFrame f in sf)
				{
					bSFRelevant = f.GetMethod().Name == "OnFindInDatabase";
					bSFRelevant |= f.GetMethod().Name == "OnPwListFind"; //KeePass 2.41
					bSFRelevant |= f.GetMethod().Name == "PerformSearchDialog"; //KeePass 2.41
					lSF.Add(f.GetMethod().Name);
					if (bSFRelevant) break;
				}
				bool bAddCheckbox = (iOpenDB > 1) && bSFRelevant;
				PluginDebug.AddInfo("Search form added", 0,
					"Open databases: " + iOpenDB.ToString(),
					"Callstack relevant: " + bSFRelevant.ToString(),
					"Add 'Search db' checkbox: " + bAddCheckbox.ToString());
				if (!bSFRelevant) PluginDebug.AddInfo("Callstack", 0, lSF.ToArray());
				if (!bAddCheckbox || !AddCheckBox(e.Form)) return;
				m_sf = e.Form as SearchForm;
				m_sf.Shown += OnSearchFormShown;
				m_sf.Closed += OnSearchFormClosed;
			}
			if (e.Form is ListViewForm)
			{
				ListViewFormAdded(e.Form as ListViewForm);
			}
		}

		private void ListViewFormAdded(ListViewForm f)
		{
			var lv = f.Controls.OfType<ListView>().ToList().FirstOrDefault();
			if (lv == null) return;
			lv.RS_Sortable(true);
			f.Shown += OnShowListviewForm;
		}

		private void OnShowListviewForm(object sender, EventArgs e)
		{
			if (Config.PasswordDisplay == Config.PasswordDisplayMode.Always) return;

			if (Config.PasswordDisplay == Config.PasswordDisplayMode.EntryviewBased)
			{
				AceColumn c = KeePass.Program.Config.MainWindow.EntryListColumns.Where(x => x.Type == AceColumnType.Password).FirstOrDefault();
				if (c != null && !c.HideWithAsterisks) return;
			}

			var lv = (sender as Form).Controls.OfType<ListView>().ToList().FirstOrDefault();
			if (lv == null) return;
			ColumnHeader h = null;
			foreach (ColumnHeader x in lv.Columns)
			{
				if (x.Text == KPRes.Password)
				{
					h = x;
					break;
				}
			}
			if (h == null) return;
			foreach (ListViewItem x in lv.Items)
			{
				x.SubItems[h.Index].Text = PwDefs.HiddenPassword;
			}
		}

		private void OnSearchFormClosed(object sender, EventArgs e)
		{
			//m_sf = null;
			//m_btnOK = null;
			m_cbSearchAllDatabases = null;
			m_lEntryListColumns = null;
		}

		private bool AddCheckBox(Form form)
		{
			CheckBox m_cbIgnoreGroupSettings = (CheckBox)Tools.GetControl("m_cbIgnoreGroupSettings", form);
			CheckBox m_cbDerefData = (CheckBox)Tools.GetControl("m_cbDerefData", form);
			m_btnOK = (Button)Tools.GetControl("m_btnOK", form);
			Button btnCancel = (Button)Tools.GetControl("m_btnCancel", form);
			Button btnHelp = (Button)Tools.GetControl("m_btnHelp", form); //KeePass 2.47
			if ((m_cbIgnoreGroupSettings == null) || (m_cbDerefData == null) || (m_btnOK == null) || (btnCancel == null))
			{
				PluginDebug.AddError("Could not add 'Search in all DB' checkbox", 0,
					"m_cbIgnoreGroupSettings: " + (m_cbIgnoreGroupSettings == null).ToString(),
					"m_cbDerefData: " + (m_cbDerefData == null).ToString(),
					"m_btnOK: " + (m_btnOK == null).ToString(),
					"m_btnCancel: " + (btnCancel == null).ToString(),
					"m_btnHelp: " + (btnHelp == null).ToString());
				return false;
			}
			m_cbSearchAllDatabases = new CheckBox();
			m_cbSearchAllDatabases.Name = "m_cbRookieSearchAllDB";
			m_cbSearchAllDatabases.Text = PluginTranslate.Search;
			m_cbSearchAllDatabases.AutoSize = true;
			m_cbSearchAllDatabases.Left = m_cbDerefData.Left;
			int spacing = m_cbDerefData.Top - m_cbIgnoreGroupSettings.Top;
			m_cbSearchAllDatabases.Top = m_cbDerefData.Top + spacing;
			Control c = m_cbDerefData.Parent;
			while (c != null)
			{
				c.Height += spacing;
				c = c.Parent;
			}
			m_btnOK.Top += spacing;
			btnCancel.Top += spacing;
			if (btnHelp != null) btnHelp.Top += spacing;
			m_cbDerefData.Parent.Controls.Add(m_cbSearchAllDatabases);
			PluginDebug.AddInfo("'Search db' checkbox added", 0);
			if (m_cbSearchAllDatabases.Enabled)
				m_cbSearchAllDatabases.CheckedChanged += OnSelectAllDB_CheckedChanged;
			else
			{
				PluginDebug.AddInfo("'Search db' checkbox is disabled, something went terribly wrong", 0);
				m_cbSearchAllDatabases.Enabled = true;
				m_cbSearchAllDatabases.CheckedChanged += OnSelectAllDB_CheckedChanged;
			}
			return true;
		}

		private void OnSelectAllDB_CheckedChanged(object sender, EventArgs e)
		{
			if ((m_cbSearchAllDatabases == null) || !m_cbSearchAllDatabases.Enabled) return;
			FindInfo fi = SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchForm);
			Config.SearchFormGlobalSession = m_cbSearchAllDatabases.Checked;
			if (m_cbSearchAllDatabases.Checked)
			{
				fi.StandardEventHandlers = m_btnOK.GetEventHandlers("Click");
				List<string> lInfos = new List<string>();
				foreach (Delegate d in fi.StandardEventHandlers)
					lInfos.Add(d.Method.DeclaringType.FullName + " " + d.Method.Name);
				lInfos.Insert(0, "Count: " + lInfos.Count.ToString());
				lInfos.Insert(0, fi.ToString());
				PluginDebug.AddInfo("Replaced eventhandler", 0, lInfos.ToArray());
				m_btnOK.RemoveEventHandlers("Click", fi.StandardEventHandlers);
				m_btnOK.Click += OnSearchExecute;
				if (fi.StandardEventHandlers.Count == 0)
				{
					m_btnOK.DialogResult = DialogResult.None;
					m_sf.AcceptButton = null;
				}
			}
			else
			{
				m_btnOK.Click -= OnSearchExecute;
				m_btnOK.AddEventHandlers("Click", fi.StandardEventHandlers);
				PluginDebug.AddInfo("Restored eventhandler", 0);
				m_btnOK.DialogResult = DialogResult.OK;
				m_sf.AcceptButton = m_btnOK;
			}
		}

		private void OnSearchFormShown(object sender, EventArgs e)
		{
			m_sf.Shown -= OnSearchFormShown;
			if ((m_cbSearchAllDatabases == null) || !m_cbSearchAllDatabases.Enabled) return;
			m_cbSearchAllDatabases.Checked = Config.SearchFormGlobalSession;
		}

		private void OnSearchExecute(object sender, EventArgs e)
		{
			//Perform search in all open databases
			m_dDBGroups = new Dictionary<PwDatabase, PwGroup>();
			PwGroup g = null;
			List<PwDatabase> lOpenDB = m_host.MainWindow.DocumentManager.GetOpenDatabases();
			m_btnOK.Click -= OnSearchExecute;
			List<string> lMsg = new List<string>();
			foreach (PwDatabase db in lOpenDB)
			{
				lMsg.Clear();
				lMsg.Add("DB: " + db.IOConnectionInfo.Path);
				if ((m_sf != null) && (m_sf.SearchResultsGroup != null) && (m_sf.SearchResultsGroup.Entries != null))
				{
					lMsg.Add("Previos search results cleared: " + true.ToString());
					m_sf.SearchResultsGroup.Entries.Clear();
				}
				m_sf.InitEx(db, db.RootGroup);
				FindInfo fi = SearchHelp.FindList.Find(x => x.Name == SearchHelp.SearchForm);
				if (fi.StandardEventHandlers.Count > 0)
				{
					using (MonoWorkaroundDialogResult mwaDR = new MonoWorkaroundDialogResult(sender))
					{
						foreach (Delegate onclick in fi.StandardEventHandlers)
						{
							lMsg.Add("Calling method: " + onclick.Method.Name + " - " + onclick.Method.ReflectedType.Name);
							onclick.DynamicInvoke(new object[] { sender, e });
						}
					}
				}
				else
				{
					lMsg.Add("Calling standard method");
					m_btnOK.PerformClick();
				}
				if ((m_sf.SearchResultsGroup == null) || (m_sf.SearchResultsGroup.Entries == null))
				{
					lMsg.Add("Found entries: 0");
				}
				else
				{
					lMsg.Add("Found entries: " + m_sf.SearchResultsGroup.Entries.UCount.ToString());
				}

				//Do NOT use m_sf.SearchResultsGroup.CloneDeep
				//It makes the virtual SearchResultsGroup the 
				//parent group of the found entries
				if (g == null) g = new PwGroup(true, true, m_sf.SearchResultsGroup.Name, m_sf.SearchResultsGroup.IconId);
				foreach (PwEntry pe in m_sf.SearchResultsGroup.Entries)	g.AddEntry(pe, false);
				PluginDebug.AddInfo("Executing search", 0, lMsg.ToArray());
			}

			//Don't continue if not even a single entry was found
			if ((g == null) || (g.GetEntriesCount(true) == 0))
			{
				if (m_sf.DialogResult == DialogResult.None) m_sf.DialogResult = DialogResult.OK;
				return;
			}
			//Prepare ImageList (CustomIcons can be different per database)
			ImageList il = new ImageList();
			ImageList il2 = (ImageList)Tools.GetField("m_ilCurrentIcons", m_host.MainWindow);
			foreach (Image img in il2.Images)
				il.Images.Add(img);
			Dictionary<PwEntry, int> dEntryIconIndex = new Dictionary<PwEntry, int>();
			PwDatabase dbFirst = null;
			bool bMultipleDB = false;

			foreach (PwEntry pe in g.Entries)
			{
				PwDatabase db = m_host.MainWindow.DocumentManager.FindContainerOf(pe);
				if (db == null)
				{
					PluginDebug.AddError("Could not get database for entry", 0, pe.Uuid.ToHexString());
					continue;
				}
				if (!m_dDBGroups.ContainsKey(db))	m_dDBGroups[db] = new PwGroup(true, false, SearchHelp.GetDBName(pe), PwIcon.Folder) { IsVirtual = true };
				m_dDBGroups[db].AddEntry(pe, false);
				if (dbFirst == null) dbFirst = db;
				bMultipleDB |= db != dbFirst;
				if (!pe.CustomIconUuid.Equals(PwUuid.Zero))
				{
					il.Images.Add(db.GetCustomIcon(pe.CustomIconUuid, DpiUtil.ScaleIntX(16), DpiUtil.ScaleIntY(16)));
					dEntryIconIndex[pe] = dEntryIconIndex.Count - 1;
				}
				else dEntryIconIndex[pe] = (int)pe.IconId;
			}

			//If all found entries are contained in the same database
			//simply activate this database (might not be active yet) and return
			if (!bMultipleDB)
			{
				PwGroup pgSF = (PwGroup)Tools.GetField("m_pgResultsGroup", m_sf);
				if (pgSF != null)
				{
					//clear list of found entries
					//otherwise duplicates might be shown if last searched db is the only one that is to be shown
					pgSF.Entries.Clear();
					pgSF.Entries.Add(g.Entries);
				}
				else //KeePass 2.47
				{
					pgSF = new PwGroup(true, true, g.Name, g.IconId);
					pgSF.IsVirtual = true;
					pgSF.Entries.Add(g.Entries);
				}
				PluginDebug.AddInfo("Found "+pgSF.Entries.UCount.ToString()+" entries in 1 database");
				m_host.MainWindow.UpdateUI(false, m_host.MainWindow.DocumentManager.FindDocument(dbFirst), true, pgSF, false, null, false);
				il.Dispose();
				m_sf.SearchResultsGroup.Entries.Clear();
				m_sf.SearchResultsGroup.Entries.Add(pgSF.Entries);
				m_sf.DialogResult = DialogResult.OK;
				return;
			}

			//We found entries from at least 2 databases
			//Show the results in ListViewForm and close SearchForm
			try
			{
				PluginDebug.AddInfo("Found " + g.Entries.UCount.ToString() + " entries in multiple database");
				m_sf.DialogResult = DialogResult.Abort;
				m_sf.Visible = false;
				m_sf.Close();
			}
			catch (Exception ex)
			{
				PluginDebug.AddError("Error closing searchform", new string[] { ex.Message });
			}

			m_aStandardLvInit = InitListViewMain;
			List<object> l = GetFoundEntriesList(g, dEntryIconIndex);

			ListViewForm dlg = new ListViewForm();
			int iCount = l.FindAll(x => (x as ListViewItem) != null).Count;
			string sSubTitle = iCount == 1 ? KPRes.SearchEntriesFound1 : KPRes.SearchEntriesFound;
			sSubTitle = sSubTitle.Replace("{PARAM}", iCount.ToString());
			dlg.InitEx(KPRes.Search, sSubTitle, null, null, l, il, InitListView);
			ShowMultiDBInfo(true);
			PluginDebug.AddInfo("Multi-DB results: Show", 0);
			if (dlg.ShowDialog(m_host.MainWindow) != DialogResult.OK)
			{
				PluginDebug.AddInfo("Multi-DB results: Shown", 0);
				UIUtil.DestroyForm(dlg);
				return;
			}
			PluginDebug.AddInfo("Multi-DB results: Show and navigate", 0);
			il.Dispose();
			NavigateToSelectedEntry(dlg, true);
			PluginDebug.AddInfo("Multi-DB results: Dispose form", 0);
			UIUtil.DestroyForm(dlg);
			PluginDebug.AddInfo("Multi-DB results: Disposed form", 0);
		}

		private List<object> GetFoundEntriesList(PwGroup g, Dictionary<PwEntry, int> dEntryIconIndex)
		{
			List<Object> l = new List<object>();
			m_lEntryListColumns = new List<AceColumn>();
			List<AceColumn> lColumns = null;
			if (m_miGetEntryFieldEx != null)
				lColumns = KeePass.Program.Config.MainWindow.EntryListColumns;
			else //add a basic set of columns
			{
				lColumns = new List<AceColumn>();
				AddColumn(lColumns, AceColumnType.Title, false);
				AddColumn(lColumns, AceColumnType.UserName, false);
				AddColumn(lColumns, AceColumnType.Password, true);
				AddColumn(lColumns, AceColumnType.Url, false);
				AddColumn(lColumns, AceColumnType.Notes, false);
			}
			foreach (PwEntry pe in g.Entries)
			{
				PwGroup pg = pe.ParentGroup;
				if (pg != null)
				{
					if (l.Find(x => (x is ListViewGroup) && ((x as ListViewGroup).Tag == pg)) == null)
					{
						ListViewGroup lvg = new ListViewGroup(pg.GetFullPath(" - ", pg.ParentGroup == null));
						lvg.Tag = pg;
						l.Add(lvg);
					}
				}
				ListViewItem lvi = new ListViewItem();
				lvi.Tag = new object[] { pe, g };
				lvi.Text = SearchHelp.GetDBName(pe);
				lvi.ImageIndex = dEntryIconIndex[pe];
				ListViewItem.ListViewSubItem lvsi = null;
				//Show all columns that are shown in the entry list view if possible
				if (m_miGetEntryFieldEx != null)
				{
					for (int i = 0; i < lColumns.Count; i++)
					{
						lvsi = new ListViewItem.ListViewSubItem();
						lvsi.Text = (string)m_miGetEntryFieldEx.Invoke(m_host.MainWindow, new object[] { pe, i, true, null });
						if (!m_lEntryListColumns.Contains(lColumns[i])) m_lEntryListColumns.Add(lColumns[i]);
						lvi.SubItems.Add(lvsi);
					}
				}
				else //Show a basic set of columns
				{
					foreach (AceColumn c in lColumns)
					{
						lvsi = new ListViewItem.ListViewSubItem();
						if (c.Type == AceColumnType.Title)
							lvsi.Text = c.HideWithAsterisks ? PwDefs.HiddenPassword : pe.Strings.ReadSafe(PwDefs.TitleField);
						if (c.Type == AceColumnType.UserName)
							lvsi.Text = c.HideWithAsterisks ? PwDefs.HiddenPassword : pe.Strings.ReadSafe(PwDefs.UserNameField);
						if (c.Type == AceColumnType.Password)
							lvsi.Text = c.HideWithAsterisks ? PwDefs.HiddenPassword : pe.Strings.ReadSafe(PwDefs.PasswordField);
						if (c.Type == AceColumnType.Url)
							lvsi.Text = c.HideWithAsterisks ? PwDefs.HiddenPassword : pe.Strings.ReadSafe(PwDefs.UrlField);
						if (c.Type == AceColumnType.Notes)
							lvsi.Text = c.HideWithAsterisks ? PwDefs.HiddenPassword : KeePassLib.Utility.StrUtil.MultiToSingleLine(pe.Strings.ReadSafe(PwDefs.NotesField));
						//Deref data if required
						lvsi.Text = DerefString(lvsi.Text, pe);
						if (!m_lEntryListColumns.Contains(c)) m_lEntryListColumns.Add(c);
						lvi.SubItems.Add(lvsi);
					};
				};
				l.Add(lvi);
			}
			return l;
		}

		private string DerefString(string text, PwEntry pe)
		{
			if (m_miSprCompileFn == null) return text;
			if (!KeePass.Program.Config.MainWindow.EntryListShowDerefData) return text;
			if (KeePass.Program.Config.MainWindow.EntryListShowDerefDataAsync) return text;
			if (!text.Contains("{")) return text;
			PwListItem pli = new PwListItem(pe);
			return (string)m_miSprCompileFn.Invoke(null, new object[] { text, pli });
		}

		private void AddColumn(List<AceColumn> lColumns, AceColumnType ColType, bool hide)
		{
			AceColumn c = KeePass.Program.Config.MainWindow.EntryListColumns.Find(x => x.Type == ColType);
			if (c == null)
			{
				c = new AceColumn(ColType);
				c.HideWithAsterisks = hide;
			}
			else c = new AceColumn(c.Type, c.CustomName, c.HideWithAsterisks, c.SafeGetWidth(1));
			lColumns.Add(c);
		}

		private void InitListViewMain(ListView lv)
		{
			int w = lv.ClientSize.Width - UIUtil.GetVScrollBarWidth();
			int wf = w / (m_lEntryListColumns.Count);
			int di = Math.Min(UIUtil.GetSmallIconSize().Width, wf);

			List<int> lIndices = new List<int>();

			lIndices.Add(0);
			for (int i = 0; i < m_lEntryListColumns.Count; i++)
			{
				lIndices.Add(i + 1);
				int cw = wf;
				if (i == 0) cw = wf + di;
				if (i == m_lEntryListColumns.Count - 1) cw = wf - di;

				lv.Columns.Add(m_lEntryListColumns[i].GetDisplayName(), cw);
			}
			UIUtil.SetDisplayIndices(lv, lIndices.ToArray());
			m_lEntryListColumns = null;
		}
		#endregion

		#region All 'Find' functions beside 'Search...'
		private void OnClickFindEntry(object sender, EventArgs e)
		{
			string f = (sender as ToolStripItem).Name;
			FindInfo fi = SearchHelp.FindList.Find(x => x.Name == (sender as ToolStripItem).Name);

			if (CallStandardSearch(fi, (sender as ToolStripItem).Name))
			{
				if (fi != null)
				{
					foreach (Delegate d in fi.StandardEventHandlers)
						d.DynamicInvoke(new object[] { sender, e });
				}
				return;
			}

			PluginDebug.AddInfo("Call own find routine", 0, "Action: " + f);
			//Show status logger
			Form fOptDialog = null;
			IStatusLogger sl = StatusUtil.CreateStatusDialog(m_host.MainWindow, out fOptDialog, null,
				(KPRes.SearchingOp ?? "..."), true, false);
			m_host.MainWindow.UIBlockInteraction(true);

			m_aStandardLvInit = null;

			//Perform find for all open databases
			PwDatabase dbAll = MergeDatabases();
			List<object> l = null;
			try
			{
				object[] parameters;
				if (fi.SearchType != SearchType.BuiltIn) parameters = new object[] { dbAll, sl, null, fi };
				else parameters = new object[] { dbAll, sl, null };
				
				l = (List<object>)fi.StandardMethod.Invoke(m_host, parameters);
				m_aStandardLvInit = (Action<ListView>)parameters[2];
			}
			catch (Exception ex)
			{
				l = null;
				PluginDebug.AddError("Call standard find routine", 0, "Action: " + f, "Reason for standard call: " + ex.Message);
				foreach (Delegate d in fi.StandardEventHandlers)
					d.DynamicInvoke(new object[] { sender, e });
			}
			finally { dbAll.Close(); }

			m_host.MainWindow.UIBlockInteraction(false);
			sl.EndLogging();

			if (l == null) return;

			//Fill db column
			ImageList il = new ImageList();
			ImageList il2 = (ImageList)Tools.GetField("m_ilCurrentIcons", m_host.MainWindow);
			foreach (Image img in il2.Images)
				il.Images.Add(img);

			foreach (var o in l)
			{
				ListViewItem lvi = o as ListViewItem;
				if (lvi == null) continue;
				ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
				if (lvi.Tag is PwEntry)
				{
					lvsi.Text = SearchHelp.GetDBName(lvi.Tag as PwEntry);
					PwEntry pe = lvi.Tag as PwEntry;
					PwDatabase db = m_host.MainWindow.DocumentManager.FindContainerOf(pe);
					if (!pe.CustomIconUuid.Equals(PwUuid.Zero))
					{
						il.Images.Add(db.GetCustomIcon(pe.CustomIconUuid, DpiUtil.ScaleIntX(16), DpiUtil.ScaleIntY(16)));
						lvi.ImageIndex = il.Images.Count - 1;
					}
					else lvi.ImageIndex = (int)pe.IconId;
				}
				else if (lvi.Tag is PwGroup)
				{
					PwGroup pg = lvi.Tag as PwGroup;
					lvsi.Text = SearchHelp.GetDBName(pg.Entries.GetAt(0));
				}
				lvi.SubItems.Insert(0, lvsi);
			}

			if ((l.Count == 0) && !string.IsNullOrEmpty(fi.NothingFound))
			{
				Tools.ShowInfo(fi.NothingFound);
				il.Dispose();
				return;
			}

			//Show results
			ListViewForm dlg = new ListViewForm();
			//Prepare ImageList (CustomIcons can be different per database)
			dlg.InitEx(fi.Title, fi.SubTitle, fi.Note, fi.img, l, il, InitListView);
			UIUtil.ShowDialogAndDestroy(dlg);
			if (dlg.DialogResult != DialogResult.OK) return;
			il.Dispose();
			NavigateToSelectedEntry(dlg, false);
		}

		private void NavigateToSelectedEntry(ListViewForm dlg, bool CalledFromSearchForm)
		{
			PwGroup pg = dlg.ResultGroup as PwGroup; //parent group of selected entry
			PwEntry pe = dlg.ResultItem as PwEntry;
			if (pe == null) //try getting the virtual group for the selected entries database
			{
				object[] oEntryAndGroup = dlg.ResultItem as object[];
				if ((oEntryAndGroup != null) && (oEntryAndGroup.Length == 2))
				{
					pe = oEntryAndGroup[0] as PwEntry;
					pg = oEntryAndGroup[1] as PwGroup;
				}
			}
			if (pe != null) ActivateDB(pe);
			if ((pg == null) && (pe == null))
				pg = (dlg.ResultItem as PwGroup);
			if ((pg != null) || (pe != null))
			{
				if (pg != null)
				{
					PwDocument doc = null;
					if (pe == null)
						doc = m_host.MainWindow.DocumentManager.FindDocument(m_host.MainWindow.DocumentManager.SafeFindContainerOf(pg.Entries.GetAt(0)));
					else
						doc = m_host.MainWindow.DocumentManager.FindDocument(m_host.MainWindow.DocumentManager.SafeFindContainerOf(pe));
					bool bCleanUpDone = false;
					for (int i = (int)pg.Entries.UCount - 1; i >= 0; i--)
					{
						if (doc != m_host.MainWindow.DocumentManager.FindDocument(m_host.MainWindow.DocumentManager.SafeFindContainerOf(pg.Entries.GetAt((uint)i))))
						{
							bCleanUpDone = true;
							pg.Entries.RemoveAt((uint)i);
						}
					}
					if (bCleanUpDone && !CalledFromSearchForm) ShowMultiDBInfo(CalledFromSearchForm);
					if (pg != null)
					{
						pe = pg.Entries.GetAt(0);
						foreach (KeyValuePair<PwDatabase, PwGroup> kvp in m_dDBGroups)
						{
							if (kvp.Value.FindEntry(pe.Uuid, true) != null)
							{
								pg = kvp.Value;
								break;
							}
						}
					}
					m_host.MainWindow.UpdateUI(false, doc, false, null, true, pg, false, m_lvEntries);
				}
				else
				{
					PwDocument doc = m_host.MainWindow.DocumentManager.FindDocument(m_host.MainWindow.DocumentManager.SafeFindContainerOf(pe));
					m_host.MainWindow.UpdateUI(false, doc, true, pe.ParentGroup, true, null, false, m_lvEntries);
				}

				MethodInfo mi = null;
				if (pe != null)
				{
					KeePassLib.Collections.PwObjectList<PwEntry> lSel = new KeePassLib.Collections.PwObjectList<PwEntry>();
					lSel.Add(pe);
					m_host.MainWindow.SelectEntries(lSel, true, true);
					mi = m_host.MainWindow.GetType().GetMethod("EnsureVisibleSelected", BindingFlags.Instance | BindingFlags.NonPublic);
					if (mi != null) mi.Invoke(m_host.MainWindow, new object[] { false });
				}
				else
				{
					mi = m_host.MainWindow.GetType().GetMethod("SelectFirstEntryIfNoneSelected", BindingFlags.Instance | BindingFlags.NonPublic);
					if (mi != null) mi.Invoke(m_host.MainWindow, null);
				}

				mi = m_host.MainWindow.GetType().GetMethod("UpdateUIState", BindingFlags.Instance | BindingFlags.NonPublic, null,
					new Type[] { typeof(bool) }, null);
				if (mi != null) mi.Invoke(m_host.MainWindow, new object[] { false });
			}
			m_dDBGroups.Clear();
		}

		private void ActivateDB(PwEntry pe)
		{
			PwDatabase db = m_host.MainWindow.DocumentManager.FindContainerOf(pe);
			if (db == null) return;
			PwDocument doc = m_host.MainWindow.DocumentManager.FindDocument(db);
			if (doc == null) return;
			m_host.MainWindow.MakeDocumentActive(doc);
		}

		private bool CallStandardSearch(FindInfo fi, string sendername)
		{
			if (fi == null)
			{
				PluginDebug.AddError("Call standard find routine", 0, "Reason for standard call: FindInfo empty", "Event sender: " + sendername);
				return true;
			}
			if (fi.StandardMethod == null)
			{
				PluginDebug.AddError("Call standard find routine", 0, "Action: " + fi.Name, "Reason for standard call: No hook for " + fi.Name);
				return true;
			}
			if (m_host.MainWindow.DocumentManager.GetOpenDatabases().Count < 2)
			{
				PluginDebug.AddInfo("Call standard find routine", 0, "Action: " + fi.Name, "Reason for standard call: No more than one db opened");
				return true;
			}
			return false;
		}

		private void InitListView(ListView lv)
		{
			if (m_aStandardLvInit == null) return;
			m_aStandardLvInit(lv);
			m_aStandardLvInit = null;
			int[] iDisplayIndices = new int[lv.Columns.Count + 1];
			int w = lv.ClientSize.Width - UIUtil.GetVScrollBarWidth();
			int wf = w / iDisplayIndices.Length;
			iDisplayIndices[0] = 0;
			int di = Math.Min(UIUtil.GetSmallIconSize().Width, wf);
			for (int i = 0; i < lv.Columns.Count; i++)
			{
				iDisplayIndices[i + 1] = lv.Columns[i].DisplayIndex + 1;
				lv.Columns[i].Width = (i == lv.Columns.Count - 1) ? wf - di : wf;
			}
			lv.Columns.Insert(0, KPRes.Database, wf + di);
			UIUtil.SetDisplayIndices(lv, iDisplayIndices);
		}

		private PwDatabase MergeDatabases()
		{
			PwDatabase dbAll = new PwDatabase();
			dbAll.New(new KeePassLib.Serialization.IOConnectionInfo(), new KeePassLib.Keys.CompositeKey());
			foreach (PwDatabase db in m_host.MainWindow.DocumentManager.GetOpenDatabases())
			{
				dbAll.RootGroup.AddGroup(db.RootGroup, false, false);
			}
			return dbAll;
		}

		private void ReplaceFindHandlers()
		{
			GetFindHandlers();
			foreach (FindInfo fi in SearchHelp.FindList)
			{
				if (string.IsNullOrEmpty(fi.Func))
					continue;
				if (!Config.HookActive(fi.Name)) continue;
				if (fi.tsiMenuItem == null) continue;
				if (fi.tsiMenuItem.IsDisposed) continue;
				fi.tsiMenuItem.RemoveEventHandlers("Click", fi.StandardEventHandlers);
				fi.tsiMenuItem.Click += OnClickFindEntry;
			}
		}

		private void GetFindHandlers()
		{
			foreach (FindInfo fi in SearchHelp.FindList)
			{
				if (string.IsNullOrEmpty(fi.Func)) continue;
				if (fi.img == null) fi.img = SmallIcon;
				if (fi.tsiMenuItem == null)
					fi.StandardEventHandlers = new List<Delegate>();
				else
					fi.StandardEventHandlers = fi.tsiMenuItem.GetEventHandlers("Click");
			}
		}

		private void RestoreFindHandlers()
		{
			foreach (FindInfo fi in SearchHelp.FindList)
			{
				if (fi.tsiMenuItem == null) continue;
				if (fi.tsiMenuItem.IsDisposed) continue;
				fi.tsiMenuItem.Click -= OnClickFindEntry;
				fi.tsiMenuItem.RemoveEventHandlers("Click", fi.StandardEventHandlers);
				fi.tsiMenuItem.AddEventHandlers("Click", fi.StandardEventHandlers);
			}
		}
		#endregion

		#region Options
		private void OnShowOptions(object sender, EventArgs e)
		{
			Tools.ShowOptions();
		}

		private void Tools_OptionsFormShown(object sender, Tools.OptionsFormsEventArgs e)
		{
			Options o = new Options();
			o.cbSearchForm.Checked = Config.SearchForm;
			o.cbSearchDupPw.Checked = o.cbSearchDupPw.Enabled && Config.HookSearchDupPw;
			o.cbSearchPwPairs.Checked = o.cbSearchPwPairs.Enabled && Config.HookSearchPwPairs;
			o.cbSearchPwCluster.Checked = o.cbSearchPwCluster.Enabled && Config.HookSearchPwCluster;
			o.cbSearchPwQuality.Checked = o.cbSearchPwQuality.Enabled && Config.HookPwQuality;
			o.cbSearchLarge.Checked = o.cbSearchLarge.Enabled && Config.HookLargeEntries;
			o.cbSearchLastMod.Checked = o.cbSearchLastMod.Enabled && Config.HookLastMod;
			o.cbSearchAllExpired.Checked = o.cbSearchAllExpired.Enabled && Config.HookAllExpired;
			o.cbMultiDBSearchInfoSearchFormActive.Checked = Config.ShowMultiDBInfoSearchForm;
			o.cbMultiDBSearchInfoSingleSearchActive.Checked = Config.ShowMultiDBInfoSingleSearch;
			o.SetPwDisplayMode(Config.PasswordDisplay); 
			Tools.AddPluginToOptionsForm(this, o);
		}

		private void Tools_OptionsFormClosed(object sender, Tools.OptionsFormsEventArgs e)
		{
			if (e.form.DialogResult != DialogResult.OK) return;
			bool bShown = false;
			Options o = (Options)Tools.GetPluginFromOptions(this, out bShown);
			if (!bShown) return;
			Config.SearchForm = o.cbSearchForm.Checked;
			if (o.cbSearchDupPw.Enabled) Config.HookSearchDupPw = o.cbSearchDupPw.Checked;
			if (o.cbSearchPwPairs.Enabled) Config.HookSearchPwPairs = o.cbSearchPwPairs.Checked;
			if (o.cbSearchPwCluster.Enabled) Config.HookSearchPwCluster = o.cbSearchPwCluster.Checked;
			if (o.cbSearchPwQuality.Enabled) Config.HookPwQuality = o.cbSearchPwQuality.Checked;
			if (o.cbSearchLarge.Enabled) Config.HookLargeEntries = o.cbSearchLarge.Checked;
			if (o.cbSearchLastMod.Enabled) Config.HookLastMod = o.cbSearchLastMod.Checked;
			if (o.cbSearchAllExpired.Enabled) Config.HookAllExpired = o.cbSearchAllExpired.Checked;
			Config.ShowMultiDBInfoSearchForm = o.cbMultiDBSearchInfoSearchFormActive.Checked;
			Config.ShowMultiDBInfoSingleSearch = o.cbMultiDBSearchInfoSingleSearchActive.Checked;
			Config.PasswordDisplay = o.GetPwDisplayMode();
			Activate();
		}
		#endregion

		#region General stuff
		private void GetStandardMethods()
		{
			m_miUpdateColumnsEx = m_host.MainWindow.GetType().GetMethod("UpdateColumnsEx", BindingFlags.Instance | BindingFlags.NonPublic);
			if (m_miUpdateColumnsEx == null) PluginDebug.AddError("Could not get method 'UpdateColumnsEx'", 0);

			m_miGetEntryFieldEx = m_host.MainWindow.GetType().GetMethod("GetEntryFieldEx", BindingFlags.Instance | BindingFlags.NonPublic);
			if (m_miGetEntryFieldEx == null) PluginDebug.AddError("Could not get method 'GetEntryFieldEx'", 0);

			try
			{
				Type t = typeof(KeePass.Program).Assembly.GetType("KeePass.UI.AsyncPwListUpdate");
				m_miSprCompileFn = t.GetMethod("SprCompileFn", BindingFlags.Static | BindingFlags.NonPublic);
			}
			catch { }
			if (m_miSprCompileFn == null) PluginDebug.AddError("Could not get method 'SprCompileFn'", 0);
		}

		private void Activate()
		{
			if ((m_miUpdateColumnsEx == null) || (m_lvEntries == null))
			{
				Tools.ShowError(string.Format(PluginTranslate.ErrorNoActivation, PluginTranslate.PluginName));
				return;
			}

			GlobalWindowManager.WindowAdded -= OnSearchFormAdded;
			GlobalWindowManager.WindowAdded += OnSearchFormAdded;

			RestoreFindHandlers();
			ReplaceFindHandlers();
		}

		public void ShowMultiDBInfo(bool CalledFromSearchForm)
		{
			if (CalledFromSearchForm && Config.ShowMultiDBInfoSearchForm)
			{
				DialogResult dr = MessageBox.Show(string.Format(PluginTranslate.MultiDBSearchInfoSearchForm, KPRes.No), PluginTranslate.PluginName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
				Config.ShowMultiDBInfoSearchForm = dr == DialogResult.Yes;
			}
			else if (!CalledFromSearchForm && Config.ShowMultiDBInfoSingleSearch)
			{
				DialogResult dr = MessageBox.Show(string.Format(PluginTranslate.MultiDBSearchInfoSingleSearch, KPRes.No), PluginTranslate.PluginName, MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
				Config.ShowMultiDBInfoSingleSearch = dr == DialogResult.Yes;
			}
		}
		#endregion

		public override void Terminate()
		{
			if (m_host == null) return;

			RestoreFindHandlers();

			GlobalWindowManager.WindowAdded -= OnSearchFormAdded;
			m_host.MainWindow.ToolsMenu.DropDownItems.Remove(m_menu);
			Tools.OptionsFormShown -= Tools_OptionsFormShown;
			Tools.OptionsFormClosed -= Tools_OptionsFormClosed;
			m_menu.Click -= OnShowOptions;

			if ((m_menu != null) && !m_menu.IsDisposed)
				m_menu.Dispose();
			if ((m_cbSearchAllDatabases != null) && !m_cbSearchAllDatabases.IsDisposed)
				m_cbSearchAllDatabases.Dispose();

			PluginDebug.SaveOrShow();

			m_host = null;
		}

		public override string UpdateUrl
		{
			get { return "https://raw.githubusercontent.com/rookiestyle/globalsearch/master/version.info"; }
		}

		public override Image SmallIcon
		{
			get { return m_img; }
		}
	}
}
