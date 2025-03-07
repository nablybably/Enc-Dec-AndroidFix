using System;
using System.Collections.Generic;
using AndroidTestCross.Classes;
using AndroidTestCross.Classes.FileStructureClasses;
using AndroidTestCross.ViewModels;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AndroidTestCross.Views;

public partial class MainView : UserControl
{
    // https://stackoverflow.com/questions/76372302 /avaloniaui-access-viewmodel-from-code-behind
    private MainViewModel? mvm;
    public MainView()
    {
        InitializeComponent();
        ConfigHandler.loadConfig();
    }

    private void getMvm(object? sender, EventArgs e)
    {
        mvm = DataContext as MainViewModel;
    }
    
    private void InputBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        mvm!.Debug = Input.Text??String.Empty;
    }

    private void copy(object? sender, RoutedEventArgs e)
    {
        TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync(Input.Text);
    }

    private async void paste(object? sender, RoutedEventArgs e)
    {
        try
        {
            mvm!.Input = await TopLevel.GetTopLevel(this)?.Clipboard?.GetTextAsync()! ?? string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void shuffle(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Clicked");
    }
    
    public void toggleHidden(object? sender, RoutedEventArgs e)
    {
        mvm.toggleHidden();
    }
}