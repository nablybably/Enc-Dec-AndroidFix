using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AndroidTestCross.Classes;
using AndroidTestCross.Classes.FileStructureClasses;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AndroidTestCross.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string input;
    [ObservableProperty] private string output;
    [ObservableProperty] private string fileShuffle;
    [ObservableProperty] private string shuffleSeed;
    [ObservableProperty] private string fhFilePath;
    [ObservableProperty] private string debug;
    //[ObservableProperty] private List<IFileTreeObject> dirFiles;
    [ObservableProperty]private ObservableCollection<IFileTreeObject> dirFiles;
    
    private static FileHandler fh;

    public MainViewModel()
    {
        debug = "";
        fhFilePath = "Test";
        shuffleSeed = "";
        fileShuffle = "";
        output = "";
        input = "";
        dirFiles = new ObservableCollection<IFileTreeObject>();
        
        fh = new FileHandler(this);
    }

    public void showFiles()
    {
        foreach (var file in DirFiles)
        {
            Console.WriteLine(file.getName);
        }
    }
    public void toggleHidden()
    {
        fh.toggleHidden();
    }
}