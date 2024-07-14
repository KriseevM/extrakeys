using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using DynamicData;
using extrakeys.Models;
using extrakeys.Utility;
using extrakeys.ViewModels;

namespace extrakeys.Views;

public partial class ProgrammerWindow : Window
{
    public ProgrammerWindow()
    {
        InitializeComponent();
        this.DataContext = this.CreateInstance<ProgrammerWindowViewModel>();
    }

    private void RemoveKeybinding(int buttonIndex, int keyCodeIndex)
    {
        ((ProgrammerWindowViewModel?)this.DataContext)?.RemoveKeybinding(buttonIndex, keyCodeIndex);
    }

    private void RemoveKeybindHandler(object? sender, RoutedEventArgs e)
    {
        var button = (Button?)sender;
        if (button == null) return;
        var buttonDataContext = button.Parent?.DataContext as KeyBinding;
        if (buttonDataContext == null) return;
        var buttonIndex = buttonDataContext.KeyNumber;
        var keyCodeIndex = buttonDataContext.KeyCodes.IndexOf(button.Tag as KeyCodeData);
        RemoveKeybinding(buttonIndex, keyCodeIndex);
    }

    private void CleanKeybinding(object? sender, RoutedEventArgs e)
    {
        var button = (Button?)sender;
        if (button == null) return;
        var buttonDataContext = button.DataContext as KeyBinding;
        if (buttonDataContext == null) return;
        var buttonIndex = buttonDataContext.KeyNumber;
        ((ProgrammerWindowViewModel?)this.DataContext)?.CleanKeybinding(buttonIndex);
    }

    private async void AddKeybinding(object? sender, RoutedEventArgs e)
    {
        var window = new NewKeybindWindow();
        await window.ShowDialog(this);
        var windowViewModel = ((NewKeybindWindowViewModel?)window.DataContext);
        if (windowViewModel == null) return;
        if(windowViewModel.SelectedIndex == -1) return;
        if (windowViewModel.Success != true) return;
        var button = (Button?)sender;
        if (button?.DataContext is not KeyBinding buttonDataContext) return;
        var buttonIndex = buttonDataContext.KeyNumber;
        ((ProgrammerWindowViewModel?)DataContext)?.AddKeyToKeyBinding(buttonIndex,
            KeyCodeData.PreDefinedKeyCodes[windowViewModel.SelectedIndex]);
    }

    private void UploadKeybindings(object? sender, RoutedEventArgs e)
    {
        ((ProgrammerWindowViewModel?)this.DataContext)?.UploadKeybindings();
    }

    private void RestoreKeybindings(object? sender, RoutedEventArgs e)
    {
        ((ProgrammerWindowViewModel?)this.DataContext)?.ReloadKeybinds();
    }

    private void ClearKeybindings(object? sender, RoutedEventArgs e)
    {
        ((ProgrammerWindowViewModel?)this.DataContext)?.ResetKeybindings();
    }
}