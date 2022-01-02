using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;

using KeePass.Plugins;
using KeePass.Util;
using KeePassLib.Utility;

using PluginTools;
using System.Windows.Forms;

namespace PluginTranslation
{
	public class TranslationChangedEventArgs : EventArgs
	{
		public string OldLanguageIso6391 = string.Empty;
		public string NewLanguageIso6391 = string.Empty;

		public TranslationChangedEventArgs(string OldLanguageIso6391, string NewLanguageIso6391)
		{
			this.OldLanguageIso6391 = OldLanguageIso6391;
			this.NewLanguageIso6391 = NewLanguageIso6391;
		}
	}

	public static class PluginTranslate
	{
		public static long TranslationVersion = 0;
		public static event EventHandler<TranslationChangedEventArgs> TranslationChanged = null;
		private static string LanguageIso6391 = string.Empty;
		#region Definitions of translated texts go here
		public const string PluginName = "Global Search";
		/// <summary>
		/// Please select the search functions that {0} shall enhance.
		/// 	
		/// {0} will perform the search in all open databases and will enrich the result shown with the database a found entry is contained in.
		/// 
		/// KeePass as well as many of the available plugins assume that all entries shown in the entry list are contained in the currently active database.
		/// To not break compatibility and to avoid inconsistencies like flagging the wrong database as changed, {0} will behave as follows:
		/// 
		/// If a search returns results from multiple databases, the results will be shown in a separate window. 
		/// Selecting an entry will
		/// - Activate the selected entry's database
		/// - Show all found entries in the entry list, that are contained in this database
		/// 
		/// If '{1}' returns only entries from exactly one database, this database will be activated and the search results will be shown as usual.
		/// </summary>
		public static readonly string Description = @"Please select the search functions that {0} shall enhance.
	
{0} will perform the search in all open databases and will enrich the result shown with the database a found entry is contained in.

KeePass as well as many of the available plugins assume that all entries shown in the entry list are contained in the currently active database.
To not break compatibility and to avoid inconsistencies like flagging the wrong database as changed, {0} will behave as follows:

If a search returns results from multiple databases, the results will be shown in a separate window. 
Selecting an entry will
- Activate the selected entry's database
- Show all found entries in the entry list, that are contained in this database

If '{1}' returns only entries from exactly one database, this database will be activated and the search results will be shown as usual.";
		/// <summary>
		/// Enhance searches
		/// </summary>
		public static readonly string OptionsCaption = @"Enhance searches";
		/// <summary>
		/// Search in all open databases
		/// </summary>
		public static readonly string Search = @"Search in all open databases";
		/// <summary>
		/// Show info if '{0}' returns entries from multiple databases
		/// </summary>
		public static readonly string MultiDBSearchInfoSearchFormActive = @"Show info if '{0}' returns entries from multiple databases";
		/// <summary>
		/// The result of this search contain entries from multiple databases and will be displayed in a separate window.
		/// After selecting an entry, the entry list will show only found entries contained in the selected entry's database.
		/// 
		/// To disable this message, choose '{0}' or visit the plugin's options
		/// </summary>
		public static readonly string MultiDBSearchInfoSearchForm = @"The result of this search contain entries from multiple databases and will be displayed in a separate window.
After selecting an entry, the entry list will show only found entries contained in the selected entry's database.

To disable this message, choose '{0}' or visit the plugin's options";
		/// <summary>
		/// Show info if other searches return entries from multiple databases
		/// </summary>
		public static readonly string MultiDBSearchInfoSingleSearchActive = @"Show info if other searches return entries from multiple databases";
		/// <summary>
		/// You selected an entry from a multi-db search result.
		/// The entry list will show only found entries contained in the selected entry's database.
		/// 
		/// To disable this message, choose '{0}' or visit the plugin's options
		/// </summary>
		public static readonly string MultiDBSearchInfoSingleSearch = @"You selected an entry from a multi-db search result.
The entry list will show only found entries contained in the selected entry's database.

To disable this message, choose '{0}' or visit the plugin's options";
		/// <summary>
		/// Activation of {0} not possible.
		/// 
		/// Could not find required objects
		/// </summary>
		public static readonly string ErrorNoActivation = @"Activation of {0} not possible.

Could not find required objects";
		/// <summary>
		/// Password display
		/// </summary>
		public static readonly string PWDisplayMode = @"Password display";
		/// <summary>
		/// Always
		/// </summary>
		public static readonly string PWDisplayModeAlways = @"Always";
		/// <summary>
		/// Never
		/// </summary>
		public static readonly string PWDisplayModeNever = @"Never";
		/// <summary>
		/// Like Entry List
		/// </summary>
		public static readonly string PWDisplayModeEntryView = @"Like Entry List";
		#endregion

		#region NO changes in this area
		private static StringDictionary m_translation = new StringDictionary();

		public static void Init(Plugin plugin, string LanguageCodeIso6391)
		{
			List<string> lDebugStrings = new List<string>();
			m_translation.Clear();
			bool bError = true;
			LanguageCodeIso6391 = InitTranslation(plugin, lDebugStrings, LanguageCodeIso6391, out bError);
			if (bError && (LanguageCodeIso6391.Length > 2))
			{
				LanguageCodeIso6391 = LanguageCodeIso6391.Substring(0, 2);
				lDebugStrings.Add("Trying fallback: " + LanguageCodeIso6391);
				LanguageCodeIso6391 = InitTranslation(plugin, lDebugStrings, LanguageCodeIso6391, out bError);
			}
			if (bError)
			{
				PluginDebug.AddError("Reading translation failed", 0, lDebugStrings.ToArray());
				LanguageCodeIso6391 = "en";
			}
			else
			{
				List<FieldInfo> lTranslatable = new List<FieldInfo>(
					typeof(PluginTranslate).GetFields(BindingFlags.Static | BindingFlags.Public)
					).FindAll(x => x.IsInitOnly);
				lDebugStrings.Add("Parsing complete");
				lDebugStrings.Add("Translated texts read: " + m_translation.Count.ToString());
				lDebugStrings.Add("Translatable texts: " + lTranslatable.Count.ToString());
				foreach (FieldInfo f in lTranslatable)
				{
					if (m_translation.ContainsKey(f.Name))
					{
						lDebugStrings.Add("Key found: " + f.Name);
						f.SetValue(null, m_translation[f.Name]);
					}
					else
						lDebugStrings.Add("Key not found: " + f.Name);
				}
				PluginDebug.AddInfo("Reading translations finished", 0, lDebugStrings.ToArray());
			}
			if (TranslationChanged != null)
			{
				TranslationChanged(null, new TranslationChangedEventArgs(LanguageIso6391, LanguageCodeIso6391));
			}
			LanguageIso6391 = LanguageCodeIso6391;
			lDebugStrings.Clear();
		}

		private static string InitTranslation(Plugin plugin, List<string> lDebugStrings, string LanguageCodeIso6391, out bool bError)
		{
			if (string.IsNullOrEmpty(LanguageCodeIso6391))
			{
				lDebugStrings.Add("No language identifier supplied, using 'en' as fallback");
				LanguageCodeIso6391 = "en";
			}
			string filename = GetFilename(plugin.GetType().Namespace, LanguageCodeIso6391);
			lDebugStrings.Add("Translation file: " + filename);

			if (!File.Exists(filename)) //If e. g. 'plugin.zh-tw.language.xml' does not exist, try 'plugin.zh.language.xml'
			{
				lDebugStrings.Add("File does not exist");
				bError = true;
				return LanguageCodeIso6391;
			}
			else
			{
				string translation = string.Empty;
				try { translation = File.ReadAllText(filename); }
				catch (Exception ex)
				{
					lDebugStrings.Add("Error reading file: " + ex.Message);
					LanguageCodeIso6391 = "en";
					bError = true;
					return LanguageCodeIso6391;
				}
				XmlSerializer xs = new XmlSerializer(m_translation.GetType());
				lDebugStrings.Add("File read, parsing content");
				try
				{
					m_translation = (StringDictionary)xs.Deserialize(new StringReader(translation));
				}
				catch (Exception ex)
				{
					string sException = ex.Message;
					if (ex.InnerException != null) sException += "\n" + ex.InnerException.Message;
					lDebugStrings.Add("Error parsing file: " + sException);
					LanguageCodeIso6391 = "en";
					MessageBox.Show("Error parsing translation file\n\n" + sException, PluginName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					bError = true;
					return LanguageCodeIso6391;
				}
				bError = false;
				return LanguageCodeIso6391;
			}
		}

		private static string GetFilename(string plugin, string lang)
		{
			string filename = UrlUtil.GetFileDirectory(WinUtil.GetExecutable(), true, true);
			filename += KeePass.App.AppDefs.PluginsDir + UrlUtil.LocalDirSepChar + "Translations" + UrlUtil.LocalDirSepChar;
			filename += plugin + "." + lang + ".language.xml";
			return filename;
		}
		#endregion
	}

	#region NO changes in this area
	[XmlRoot("Translation")]
	public class StringDictionary : Dictionary<string, string>, IXmlSerializable
	{
		public System.Xml.Schema.XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(XmlReader reader)
		{
			bool wasEmpty = reader.IsEmptyElement;
			reader.Read();
			if (wasEmpty) return;
			bool bFirst = true;
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				if (bFirst)
				{
					bFirst = false;
					try
					{
						reader.ReadStartElement("TranslationVersion");
						PluginTranslate.TranslationVersion = reader.ReadContentAsLong();
						reader.ReadEndElement();
					}
					catch { }
				}
				reader.ReadStartElement("item");
				reader.ReadStartElement("key");
				string key = reader.ReadContentAsString();
				reader.ReadEndElement();
				reader.ReadStartElement("value");
				string value = reader.ReadContentAsString();
				reader.ReadEndElement();
				this.Add(key, value);
				reader.ReadEndElement();
				reader.MoveToContent();
			}
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("TranslationVersion");
			writer.WriteString(PluginTranslate.TranslationVersion.ToString());
			writer.WriteEndElement();
			foreach (string key in this.Keys)
			{
				writer.WriteStartElement("item");
				writer.WriteStartElement("key");
				writer.WriteString(key);
				writer.WriteEndElement();
				writer.WriteStartElement("value");
				writer.WriteString(this[key]);
				writer.WriteEndElement();
				writer.WriteEndElement();
			}
		}
	}
	#endregion
}