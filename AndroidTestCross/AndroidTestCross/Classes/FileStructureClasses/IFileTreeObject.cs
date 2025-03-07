namespace AndroidTestCross.Classes.FileStructureClasses;

public interface IFileTreeObject
{
    public string getPath
    {
        get;
    }

    public string getName
    {
        get;
    }

    public string getImagePath
    {
        get;
    }

    public void executeDefault();
}