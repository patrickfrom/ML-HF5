using Avalonia.Controls;
using ReviewChecker.ViewModels;

namespace ReviewChecker.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        MainWindowViewModel? viewModel = DataContext as MainWindowViewModel;
        viewModel?.HandleTextChanged();
    }
}