namespace AndroidTestCross.Classes.FileStructureClasses;

public class ErrorFile : IFileTreeObject
{
    private string name;

    public ErrorFile(string name)
    {
        this.name = name; 
    }
    public string getPath { get; }

    public string getName
    {
        get { return name; }
    }

    public string getImagePath
    {
        get { return "/Assets/NotAllowed.svg"; }
    }
    public void executeDefault()
    {
        
    }
}