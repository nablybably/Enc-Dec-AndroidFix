using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using AndroidTestCross.Classes.FileStructureClasses;
using AndroidTestCross.ViewModels;
using CrossPlatformTest.Exceptions;

namespace AndroidTestCross.Classes;

public class FileHandler
{
    private string _filePath = "";
    private string fileName = "";
    private string filePass = "";

    private string loadedBuffer = "";

    private MainViewModel mvm;
    public string filePath
    {
        get { return _filePath; }
        set
        {
            _filePath = value;
        }
    }
    
    public FileHandler(string filePath, MainViewModel mvm, string filePass = "" )
    {
        this.filePath = filePath;
        this.filePass = filePass;

        Console.WriteLine("PossRoot: " + Directory.GetDirectoryRoot("."));
        
        this.mvm = mvm;
        
        mvm.Output = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        mvm.Input = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        
        loadDir();
    }

    /// <summary>
    /// Create a file if it doesn't already exist.
    /// </summary>
    /// <param name="overwrite">Overwrite the file if it already exists.</param>
    /// <exception cref="FileExistsException">File already exists.</exception>
    public void fileCreate(bool overwrite = false)
    {
        string fileFullPath = Path.Combine(this.filePath, this.fileName); 
        if (overwrite)
        {
            File.Delete(fileFullPath);
        }
        if (!File.Exists(fileFullPath))
        {
            File.Create(fileFullPath);
        }
        else
        {
            throw new FileExistsException(fileFullPath);
        }
    }

    /// <summary>
    /// Writes data to a file.
    /// </summary>
    /// <param name="content">Content to write to file.</param>
    /// <param name="append">Should the content be appended? Default overwrite.</param>
    /// <exception cref="FileNotFoundException">Given file does not exist.</exception>
    public void fileWrite(string content, bool append = false)
    {
        string fileFullPath = Path.Combine(this.filePath, this.fileName);
        if (!File.Exists(fileFullPath))
        {
            throw new FileNotFoundException("Given file/path does not exist: " + fileFullPath + ".");
        }

        if (append)
        {
            File.AppendAllTextAsync(fileFullPath, content);
        }
        else
        {
            File.WriteAllTextAsync(fileFullPath, content);
        }
    }

    public string[] fileRead()
    {
        string fileFullPath = Path.Combine(this.filePath, this.fileName);
        if (!File.Exists(fileFullPath))
        {
            throw new FileNotFoundException("Given file/path does not exist: " + fileFullPath + ".");
        }
        return File.ReadAllLines(fileFullPath);
    }
    
    public void loadDir()
    {
        Console.WriteLine("Loading directory: " + this.filePath);
        // Clear list for filestructure
        mvm.DirFiles.Clear();
        if (filePath != "/")
        {
            mvm.DirFiles.Add(new PrevFile(this));
        }
        
        // Get all directories at directory and put it in a sorted list
        try
        {
            // Fetch all directories
            string[] directories = Directory.GetDirectories(filePath);
            List<string> directs = directories.ToList();
            directs.Sort();
            
            // Fetch all files
            string[] files = Directory.GetFiles(filePath);
            List<string> fils = files.ToList();
            fils.Sort();
            
            // For each directory add an directory object to the list
            foreach (string dir in directs)
            {
                mvm.DirFiles.Add(new DirectoryFile(dir, this));
            }

            foreach (string fil in fils)
            {
                mvm.DirFiles.Add(new NormalFile(fil, this));
            }
        }
        catch (UnauthorizedAccessException uae)
        {
            mvm.DirFiles.Add(new ErrorFile("No permissions."));
        }

        /*foreach (IFileTreeObject file in fileList)
        {
            Console.WriteLine(file.getName);
        }*/
        Console.WriteLine("Loaded directory: " + this.filePath);
    }

    public void loadKey()
    {
        
    }

    public void loadFile()
    {
        
    }

    public void retDir()
    {
        Console.WriteLine("PrevPath: " + filePath);
        filePath = Path.GetDirectoryName(filePath) ?? "/";
        Console.WriteLine("NewPath: " + filePath);
        loadDir();
    }

    public void setPath(string path)
    {
        filePath = path;
    }

    public void setName(string name)
    {
        fileName = name;
    }

    public string getLoaded()
    {
        return loadedBuffer;
    }
    
    /// <summary>
    /// Look if a file/path exists.
    /// </summary>
    /// <param name="filePath">Path.</param>
    /// <param name="fileName">File.</param>
    /// <returns>True if exists, else false.</returns>
    /// <exception cref="NullReferenceException">Path or file is null.</exception>
    public static bool fileTest(string? filePath, string? fileName = "") 
    {
        if (filePath == null || fileName == null)
        {
            string er = filePath == null ? "Path" : "Name";
            throw new NullReferenceException($"{er} is null");
        }
        bool result = Path.Exists(filePath);
        if (result && fileName != "")
        {
            fileName = filePath.EndsWith('/') ? fileName : "/" + fileName;
            result = File.Exists(filePath + fileName);
        }
        System.Console.WriteLine("Path: " + filePath + " Name: " + fileName);
        return result;
    }
}

