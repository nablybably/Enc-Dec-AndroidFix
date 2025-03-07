using System;
using System.IO;

namespace AndroidTestCross.Classes.FileStructureClasses;

public class KeyFile : IFileTreeObject
{
    public const string imagePath = "/Assets/svgviewer-outputRed.svg";
    private string fullPath;
    private FileHandler fh;

    public KeyFile(string fullPath, FileHandler fh)
    {
        this.fullPath = fullPath;
        this.fh = fh;
    }
    // Mog niet nodig.
    public string getPath
    {
        get
        {
            string basePath = Path.GetDirectoryName(fullPath);
            return basePath ?? "/";
        }
    }

    public string getName
    {
        get { return Path.GetFileName(fullPath); }
    }

    public string getImagePath
    {
        get { return imagePath; }
    }

    public void executeDefault()
    {
        fh.setPath(fullPath);
        fh.loadKey();
        Console.WriteLine($"Full path: {fullPath}, file name: {getName}\nBase name: {getPath}, image path: {getImagePath}");
    }
}