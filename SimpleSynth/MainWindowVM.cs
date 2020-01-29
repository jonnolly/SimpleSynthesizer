using Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SimpleSynth
{
    public class MainWindowVM : ViewModelBase
    {
        private ISoundGenerator _soundGenerator;
        private List<OscillatorVM> _oscillators;

        public MainWindowVM(ISoundGenerator soundGenerator)
        {
            _soundGenerator = soundGenerator;
            VirtualKeyboard.NotePressed += VirtualKeyboard_NotePressed;

            _oscillators = new List<OscillatorVM>() { 
                new OscillatorVM("Oscillator1", true),
                new OscillatorVM("Oscillator2"),
                new OscillatorVM("Oscillator3"),
                new OscillatorVM("Oscillator4"),
                new OscillatorVM("Oscillator5"),
                new OscillatorVM("Oscillator6")
            };

            Oscillators = new ObservableCollection<OscillatorVM>(_oscillators);
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

        public ObservableCollection<OscillatorVM> Oscillators { get; }

        private List<OscillatorParams> GetActiveOscillatorData()
        {
            var oscillatorParams = new List<OscillatorParams>();
            foreach (var oscillator in _oscillators)
            {
                if (oscillator.IsEnabled)
                    oscillatorParams.Add(oscillator.OscillatorData);
            }

            return oscillatorParams;
        }

        private void VirtualKeyboard_NotePressed(object sender, float frequency)
        {
            var oscillatorData = GetActiveOscillatorData();
            if(oscillatorData.Count > 0)
                _soundGenerator.GenerateSound(frequency, oscillatorData);
        }

        private void _generateSound()
        {
            var oscillatorData = GetActiveOscillatorData();
            if (oscillatorData.Count > 0)
                _soundGenerator.GenerateSound(Frequency, oscillatorData);
        }
    }
}
