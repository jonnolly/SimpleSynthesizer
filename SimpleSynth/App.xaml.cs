using Interfaces;
using System.Windows;
using Factory;

namespace SimpleSynth
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MainWindow mainWindow = new MainWindow();
            SoundGenerator soundGenerator = new SoundGenerator();
            MainWindowVM mainWindowViewModel = new MainWindowVM(soundGenerator);
            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
        }

    }
}
