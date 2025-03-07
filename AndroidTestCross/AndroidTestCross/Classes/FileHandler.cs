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

    private bool hideHidden = true;
    
    private MainViewModel mvm;
    public string FilePath
    {
        get { return _filePath; }
        set
        {
            _filePath = value;
        }
    }
    
    public FileHandler(MainViewModel mvm, string filePass = "" )
    {
        FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + Path.DirectorySeparatorChar + "nabsEncryptorDecryptor";
        this.filePass = filePass;
        if (!Directory.Exists(FilePath))
        {
            makeFileStructure();
        }
        else
        {
            //FilePath = ConfigHandler.getConfigValue("Path");
        }
        
        this.mvm = mvm;
        
        mvm.Output = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        
        loadDir();
    }

    /// <summary>
    /// Makes the filestructure that the app uses(folder in documents that would contain config/keys/...)
    /// </summary>
    public void makeFileStructure()
    {
        Directory.CreateDirectory(FilePath);
        Directory.CreateDirectory(FilePath + Path.DirectorySeparatorChar + "keys");
        ConfigHandler.createConfig(FilePath + Path.DirectorySeparatorChar);
    }

    /// <summary>
    /// Create a file if it doesn't already exist.
    /// </summary>
    /// <param name="overwrite">Overwrite the file if it already exists.</param>
    /// <exception cref="FileExistsException">File already exists.</exception>
    public void fileCreate(bool overwrite = false)
    {
        string fileFullPath = Path.Combine(this.FilePath, this.fileName); 
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
        string fileFullPath = Path.Combine(this.FilePath, this.fileName);
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
        string fileFullPath = Path.Combine(this.FilePath, this.fileName);
        if (!File.Exists(fileFullPath))
        {
            throw new FileNotFoundException("Given file/path does not exist: " + fileFullPath + ".");
        }
        return File.ReadAllLines(fileFullPath);
    }

    public void toggleHidden()
    {
        hideHidden ^= true;
        loadDir();
    }
    public void loadDir()
    {
        Console.WriteLine("Loading directory: " + FilePath);
        // Clear list for filestructure
        mvm.DirFiles.Clear();
        if (FilePath != "/")
        {
            mvm.DirFiles.Add(new PrevFile(this));
        }
        
        // Get all directories at directory and put it in a sorted list
        try
        {
            // Fetch all directories
            string[] directories = Directory.GetDirectories(FilePath);
            List<string> directs = directories.ToList();
            directs.RemoveAll(ob => Path.GetFileName(ob).StartsWith('.') && hideHidden);
            directs.Sort();
            
            // Fetch all files
            string[] files = Directory.GetFiles(FilePath);
            List<string> fils = files.ToList();
            fils.Sort();
            fils.RemoveAll(ob => Path.GetFileName(ob).StartsWith('.') && hideHidden);
            
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
        Console.WriteLine(FilePath.Length);
        // Pos size variable
        if (FilePath.Length >= 30)
        {
            mvm.FhFilePath = "..." + FilePath.Substring(FilePath.Length - 27);
        }
        else
        {
            mvm.FhFilePath = FilePath;
        }
        
    }

    public void loadKey()
    {
        
    }

    public void loadFile()
    {
        
    }

    public void retDir()
    {
        Console.WriteLine("PrevPath: " + FilePath);
        FilePath = Path.GetDirectoryName(FilePath) ?? "/";
        Console.WriteLine("NewPath: " + FilePath);
        loadDir();
    }

    public void setPath(string path)
    {
        FilePath = path;
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

