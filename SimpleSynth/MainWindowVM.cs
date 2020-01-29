using Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SimpleSynth
{
    public class MainWindowVM : ViewModelBase
    {
        private ISoundGenerator _soundGenerator;

        public MainWindowVM(ISoundGenerator soundGenerator)
        {
            _soundGenerator = soundGenerator;
            VirtualKeyboard.NotePressed += VirtualKeyboard_NotePressed;
            Oscillator1 = new OscillatorVM();
            Oscillator2 = new OscillatorVM();
        }

        public ICommand GenerateSound { get { return new DelegateCommand(() => _generateSound(), null); } }

        private float _frequency = 240f;
        public float Frequency { get
            {
                return _frequency;
            }
            set
            {
                Console.WriteLine("setting to :" + value);
                SetProperty(ref _frequency, value);
            }
        }

        public OscillatorVM Oscillator1 { get; }

        public OscillatorVM Oscillator2 { get; }

        private void VirtualKeyboard_NotePressed(object sender, float frequency)
        {
            _soundGenerator.GenerateSound(frequency, Oscillator1.OscillatorData, Oscillator2.OscillatorData);
        }

        private void _generateSound()
        {
            _soundGenerator.GenerateSound(Frequency, Oscillator1.OscillatorData, Oscillator2.OscillatorData);
        }
    }
}
