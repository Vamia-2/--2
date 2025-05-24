using System.Diagnostics;

//Пошук chrome.exe у системі

var filename = "chrome.exe";

var disks = DriveInfo.GetDrives();
string? filePath = null;
foreach (var disk in disks)
{
    Console.WriteLine($"Searching in{disk.Name}");
    if(disk.IsReady)
    {
        var dir = new DirectoryInfo(disk.Name);
        filePath = FindFilePath(dir, filename);
        if(filePath != null)
        {
            break;
        }
    }
}

if(filePath != null)
{
    Console.WriteLine($"File found: {filePath}");
}
else
{
    Console.WriteLine($"File {filename} not found.");
}

static string? FindFilePath(DirectoryInfo dir, string filename)

{

    try

    {

        var file = dir.GetFiles().FirstOrDefault(f => f.Name == filename);

        if (file != null)

        {

            return file.FullName;

        }

        foreach (var subDir in dir.GetDirectories())

        {

            var subFilePath = FindFilePath(subDir, filename);

            if (subFilePath != null)

            {

                return subFilePath;

            }

        }

        return null;

    }
    catch (Exception)

    {

        return null;

    }

}

//var url = "https://mailprotector.com/wp-content/uploads/2021/11/website-hacked-mailprotector-wordpress-scaled.jpg";

//while(true)
//{
//var startInfo = new ProcessStartInfo()
//{
//    FileName = filePath,
//    Arguments = url,
//};
//Process process = new Process()
//{
//    StartInfo=startInfo
//};
//process.Start();
//}



