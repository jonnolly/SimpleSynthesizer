using Interfaces;
using NSubstitute;
using SimpleSynth;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;

namespace Tests
{
    public class TestEnvironment
    {
        private MainWindow _mainWindow;
        private ISoundGenerator _soundGeneratorMock;

        public TestEnvironment()
        {
            _mainWindow = new MainWindow();
            _soundGeneratorMock = Substitute.For<ISoundGenerator>();
            MainWindowVM mainWindowVM = new MainWindowVM(_soundGeneratorMock);

            _mainWindow.DataContext = mainWindowVM;
            _mainWindow.Visibility = System.Windows.Visibility.Collapsed;
            _mainWindow.Show();
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Acts
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public TestEnvironment UserSelectsGenerateSound()
        {
            var button = _mainWindow.FindName("btnGenerateSound");

            ((Button)button).RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));

            // Alternative way of programatically clicking:
            //System.Windows.Automation.Peers.ButtonAutomationPeer peer = new System.Windows.Automation.Peers.ButtonAutomationPeer((Button)button);
            //System.Windows.Automation.Provider.IInvokeProvider invokeProv = peer.GetPattern(System.Windows.Automation.Peers.PatternInterface.Invoke) as System.Windows.Automation.Provider.IInvokeProvider;
            //invokeProv.Invoke();

            return this;
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Asserts
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public TestEnvironment AssertThatSoundIsGenerated(float frequency)
        {
            _soundGeneratorMock.Received().GenerateSound(Arg.Any<float>(), Arg.Any<List<OscillatorParams>>());

            return this;
        }
    }
}
