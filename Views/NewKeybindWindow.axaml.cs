using Avalonia.Controls;
using Avalonia.Interactivity;
using extrakeys.Utility;
using extrakeys.ViewModels;

namespace extrakeys.Views;

public partial class NewKeybindWindow : Window
{
    public NewKeybindWindow()
    {
        InitializeComponent();
        DataContext = this.CreateInstance<NewKeybindWindowViewModel>();
    }

    private void Accept(object? sender, RoutedEventArgs e)
    {
        var vm = ((NewKeybindWindowViewModel?)DataContext);
        if(vm != null) vm.Success = true;
        Close();
    }

    private void Cancel(object? sender, RoutedEventArgs e)
    {
        var vm = ((NewKeybindWindowViewModel?)this.DataContext);
        if(vm != null) vm.Success = false;
        Close();
    }
}