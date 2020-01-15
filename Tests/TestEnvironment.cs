using System;
using System.Collections.Generic;
using System.Text;
using SimpleSynth;
using NSubstitute;
using Interfaces;

namespace Tests
{
    public class TestEnvironment
    {
        public TestEnvironment()
        {
            MainWindow mainWindow = new MainWindow();
            ISoundGenerator soundGeneratorMock = Substitute.For<ISoundGenerator>();
            MainWindowVM mainWindowVM = new MainWindowVM(soundGeneratorMock);

            mainWindow.DataContext = mainWindowVM;
            mainWindow.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.Show();

        }

        public void UserSelectsGenerateSound()
        {
            
        }
    }
}
