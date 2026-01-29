using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using KeePass;
using KeePass.Plugins;
using PluginTools;

namespace GlobalSearch.Utilities
{
  internal class KeeThemeStub
  {
    private Plugin _kt;
    private object _ktTheme = null;
    private PropertyInfo _pEnabled = null;
    private object _themeListView = null;
    private PropertyInfo _pOddRowColor = null;
    private object _controlVisitor = null;
    private MethodInfo _visit = null;

    internal bool Installed { get { return _kt != null; } }

    internal List<string> lErrors = new List<string>();
    internal bool Enabled
    {
      get
      {
        if (!Installed || _ktTheme == null || _pEnabled == null) return false;
        return (bool)_pEnabled.GetValue(_ktTheme, null);
      }
    }

    internal Color GetOddRowColor(Color colDefault)
    {
      if (!Installed || _themeListView == null || _pOddRowColor == null) return colDefault;
      try
      {
        return (Color)_pOddRowColor.GetValue(_themeListView, null);
      }
      catch { return colDefault; }
    }

    internal KeeThemeStub()
    {
      _kt = Tools.GetPluginInstance("KeeTheme") as Plugin;

      if (_kt != null)
      {
        GetFields();
        var f = _kt.GetType().GetField("_controlVisitor", BindingFlags.Instance | BindingFlags.NonPublic);
        _controlVisitor = f.GetValue(_kt);
        _visit = _controlVisitor.GetType().GetMethod("Visit");
      }
    }

    private void GetFields()
    {
      var propKeeTheme = _kt.GetType().GetField("_theme", BindingFlags.Instance | BindingFlags.NonPublic);
      if (propKeeTheme == null)
      {
        lErrors.Add("Could not locate field KeeTheme._theme");
        return;
      }
      _ktTheme = propKeeTheme.GetValue(_kt);
      if (_ktTheme == null)
      {
        lErrors.Add("Could not get KeeTheme._theme");
        return;
      }

      _pEnabled = _ktTheme.GetType().GetProperty("Enabled");
      if (_pEnabled == null)
      {
        lErrors.Add("Could not locate property KeeTheme._theme.Enabled");
        return;
      }

      var f = _ktTheme.GetType().GetField("_theme", BindingFlags.Instance | BindingFlags.NonPublic);
      if (f == null)
      {
        lErrors.Add("Could not locate field KeeTheme._theme._theme");
        return;
      }
      var _theme = f.GetValue(_ktTheme);
      if (_theme == null)
      {
        lErrors.Add("Could not get KeeTheme._theme._theme");
        return;
      }
      var pListView = _theme.GetType().GetProperty("ListView", BindingFlags.Instance | BindingFlags.Public);
      if (pListView == null)
      {
        lErrors.Add("Could not locate property KeeTheme._theme._theme.ListViw");
        return;
      }
      _themeListView = pListView.GetValue(_theme, null);
      if (_themeListView == null)
      {
        lErrors.Add("Could not get property KeeTheme._theme._theme.ListView");
        return;
      }

      _pOddRowColor = _themeListView.GetType().GetProperty("OddRowColor", BindingFlags.Instance | BindingFlags.Public);
      if (_pOddRowColor == null)
      {
        lErrors.Add("Could not locate property KeeTheme._theme._theme.ListView.OddRowColor");
        return;
      }
    }

    private bool IsKeeThemeEvent(Delegate del)
    {
      if (del == null) return false;
      if (_kt == null) return false;
      return del.Target == _kt;
    }

    internal void HookMenu(EventHandler OnClickEvent)
    {
      var lMsg = new List<string>();
      if (Installed)
      {
        lMsg.Add("KeeTheme is installed");
        lMsg.AddRange(lErrors);
        if (Enabled) lMsg.Add("KeeTheme is enabled, KeeTheme menu does not need to be hooked");
        else
        {
          lMsg.Add("KeeTheme is not enabled, KeeTheme menu must to be hooked");
          bool bFound = false;
          foreach (var i in Program.MainForm.ToolsMenu.DropDownItems)
          {
            if (!(i is ToolStripMenuItem)) continue;
            var m = i as ToolStripMenuItem;
            var lEvents = m.GetEventHandlers("Click");
            var ktClick = lEvents.Find(x => IsKeeThemeEvent(x));
            if (ktClick != null)
            {
              //if KeeTheme is not active at startup, we need to hook it
              //ListView.OwnerDraw will be set by us otherwise and thus KeeTheme's handlers won't be added
              m.RemoveEventHandlers("Click", lEvents);
              m.Tag = lEvents;
              m.Click += OnClickEvent;
              bFound = true;
              break;
            }
          }
          if (bFound) lMsg.Add("KeeTheme menu found and replaced");
          else lMsg.Add("KeeTheme menu not found and not replaced");
        }
      }
      else lMsg.Add("KeeTheme is not installed");

      PluginDebug.AddInfo("Check for KeeTheme during startup", 0, lMsg.ToArray());
    }

    internal void Visit(Control c)
    {
      if (!Enabled) return;
      if (_visit == null) return;
      try
      {
        _visit.Invoke(_controlVisitor, new object[] { c });
      }
      catch { PluginDebug.AddError("Could not call KeeTheme to decorate the new control");}
    }
  }
}
