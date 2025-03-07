using System.IO;

namespace AndroidTestCross.Classes.FileStructureClasses;

public class DirectoryFile : IFileTreeObject
{
    private string fullPath;
    private const string imagePath = "/Assets/Folder.svg";
    private FileHandler fh;

    public DirectoryFile(string fullPath, FileHandler fh)
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
            return basePath ?? "/ee";
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
        fh.loadDir();
        //Console.WriteLine($"Full path: {fullPath}, file name: {getName}\nBase name: {getPath}, image path: {getImagePath}");
    }
}