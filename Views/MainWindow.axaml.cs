using Avalonia.Controls;
using Avalonia.Interactivity;
using extrakeys.ViewModels;

namespace extrakeys.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void RefreshButton_Click(object? sender, RoutedEventArgs e)
    {
        (DataContext as MainWindowViewModel)?.RefreshBoardList();
    }

    private void StartButton_Click(object? sender, RoutedEventArgs e)
    {
        // TODO: Start second window
    }
}