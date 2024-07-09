using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace ArithmeticGenerator;
public partial class WindowMain : Window
{
    public WindowMain()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {

    }

    private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Escape)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}