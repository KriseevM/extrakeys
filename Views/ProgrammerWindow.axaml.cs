using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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
}