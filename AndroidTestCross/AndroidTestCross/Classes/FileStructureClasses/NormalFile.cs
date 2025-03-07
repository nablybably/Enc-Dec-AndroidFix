using System;
using System.IO;

namespace AndroidTestCross.Classes.FileStructureClasses;

public class NormalFile : IFileTreeObject
{
    private string fullPath;
    private const string imagePath = "/Assets/File.svg";
    private FileHandler fh;

    public NormalFile(string fullPath, FileHandler fh)
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
        fh.setName(Path.GetFileName(fullPath));
        fh.loadFile();
        Console.WriteLine($"Full path: {fullPath}, file name: {getName}\nBase name: {getPath}, image path: {getImagePath}");
    }
}