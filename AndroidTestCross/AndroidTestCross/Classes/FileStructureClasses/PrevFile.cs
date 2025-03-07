namespace AndroidTestCross.Classes.FileStructureClasses;

public class PrevFile : IFileTreeObject
{
    private string fullPath;
    private const string imagePath = "/Assets/Back.svg";
    private FileHandler fh;

    public PrevFile(FileHandler fh)
    {
        this.fullPath = fullPath;
        this.fh = fh;
    }
    
    public string getPath
    {
        get { return "";  }
    }

    public string getName
    {
        get { return ".."; }
    }

    public string getImagePath
    {
        get { return imagePath; }
    }
    
    public void executeDefault()
    {
        fh.retDir();
    }
}