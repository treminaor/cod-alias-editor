using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("UGX AliasEditor++")]
[assembly: AssemblyDescription("Edit soundalias csv files from any Call of Duty game.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("UGX - Ultimate Gaming Experience")]
[assembly: AssemblyProduct("UGX AliasEditor++")]
[assembly: AssemblyCopyright("Copyright © Andy King 2014")]
[assembly: AssemblyTrademark("Andy King")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("6a520c53-be51-4ca0-b57e-1a31464083ec")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]

// - v0.9.0
// - Added  "Samples" list to the Edit menu which you can use to insert a sample 2d, 2d streamed, 2d looping, 3d, and 3d looping alias to your file.
// - Opening a soundalias csv which only contains a header line are no longer "invalid files".
// - Added a right-click menu to the alias list which has the same functionality as the Edit menu.
// - Implemented the Duplicate, Clear, and Remove alias commands on the Edit and right-click menus.
// - Added key shortcuts for all of the edit commands.
// - Added a registry path fixer to the Tools menu.
// - Added better CoDWaW registry handling - if your installation is not found the program will ask you to locate it instead of throwing an unhandled exception.
// - If the soundalias header has a skipped setting (",,"), it will show as "<null>"

// - v0.9.0
// - Fixed some file saving bugs when one or more aliases had been removed from the opened file.
// - Fixed some file saving bugs when one or more of the aliases had been edited in the open file.

//Future Changelog
// - Added "X" buttons to the aliasList table for faster alias removal.
// - Fixed a bug where when searching for an aliasname the settings for the found alias would not be updated.

[assembly: AssemblyVersion("0.9.0.0")]
[assembly: AssemblyFileVersion("0.9.0.0")]