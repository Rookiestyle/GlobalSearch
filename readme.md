# GlobalSearch
[![Version](https://img.shields.io/github/release/rookiestyle/globalsearch)](https://github.com/rookiestyle/globalsearch/releases/latest)
[![Releasedate](https://img.shields.io/github/release-date/rookiestyle/globalsearch)](https://github.com/rookiestyle/globalsearch/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/rookiestyle/globalsearch/total?color=%2300cc00)](https://github.com/rookiestyle/globalsearch/releases/latest/download/GlobalSearch.plgx)\
[![License: GPL v3](https://img.shields.io/github/license/rookiestyle/globalsearch)](https://www.gnu.org/licenses/gpl-3.0)

Keepass offers a variety of powerful search functions, unfortunately none of them performs a search in all currently open databases.

GlobalSearch is a KeePass plugin that uses the built-in search functions to search in all open databases.  
The database containing a found entry will be shown as an additional column in the result.

If only one database is open, it will simply use the standard KeePass search function.
If more then one databases are open, it will search in all of them and will show you all entries in all open databases that match your search criteria.

Currently covered search functions:
- Find... (who would have thought that...)
- Last Modified Entries
- Large Entries
- Duplicate Passwords
- Similar Passwords (Pairs)...
- Similar Passwords (Clusters)...
- Password Quality...
- Expired entries

# Table of Contents
- [Configuration](#configuration)
- [Usage](#usage)
- [Translations](#translations)
- [Download and Requirements](#download-and-requirements)

# Configuration
GlobalSearch integrates into KeePass' options form.
![Options](images/GlobalSearch%20-%20Options.png)

The upper part lets you chose which search functions shall be enhanced.  
GlobalSearch will perform a search in all open databases if both criteria are met:
- More then one database is opened
- The corresponding option in GlobalSearch's configuration is active

The lower part will show additional information if a search returned results from multiple databases.

# Usage
Global Search will perform the search in all open databases.  

KeePass as well as many of the available plugins assume that all entries shown in the entry list are contained in the currently active database.  
To not break compatibility and to avoid inconsistencies like flagging the wrong database as changed, Global Search will behave as follows:

If 'Find...' returns only entries from exactly one database, this database will be activated and the search results will be shown as usual.

If a search returns results from multiple databases, the results will be shown in a separate window. 
Selecting an entry will
- Activate the selected entry's database
- Show all found entries in the entry list, that are contained in this database


## Example 1 - Result from a search triggered with *Find...*
![Options](images/GlobalSearch%20-%20Find.png)
![Options](images/GlobalSearch%20-%20Result.png)

## Example 1 - Result from a search for duplicate passwords
![Options](images/GlobalSearch%20-%20Duplicate%20Password%20Result.png)

# Translations
GlobalSearch is provided with English language built-in and allow usage of translation files.
These translation files need to be placed in a folder called *Translations* inside in your plugin folder.
If a text is missing in the translation file, it is backfilled with the English text.
You're welcome to add additional translation files by creating a pull request as described in the [wiki](https://github.com/Rookiestyle/GlobalSearch/wiki/Create-or-update-translations).

Naming convention for translation files: `<plugin name>.<language identifier>.language.xml`\
Example: `GlobalSearch.de.language.xml`
  
The language identifier in the filename must match the language identifier inside the KeePass language that you can select using *View -> Change language...*\
This identifier is shown there as well, if you have [EarlyUpdateCheck](https://github.com/rookiestyle/earlyupdatecheck) installed.

# Download and Requirements
## Download
Please follow these links to download the plugin file itself.
- [Download newest release](https://github.com/rookiestyle/globalsearch/releases/latest/download/GlobalSearch.plgx)
- [Download history](https://github.com/rookiestyle/globalsearch/releases)

If you're interested in any of the available translations in addition, please download them from the [Translations](Translations) folder.
## Requirements
* KeePass: 2.41
* .NET framework: 3.5
