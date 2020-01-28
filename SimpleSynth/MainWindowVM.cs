using Interfaces;
using System;
using System.Windows.Input;

namespace SimpleSynth
{
    public class MainWindowVM : ViewModelBase
    {
        private ISoundGenerator _soundGenerator;
        private string _currentWaveFormName;

        public MainWindowVM(ISoundGenerator soundGenerator)

        {
            _soundGenerator = soundGenerator;
            VirtualKeyboard.NotePressed += VirtualKeyboard_NotePressed;
        }

        public ICommand GenerateSound { get { return new DelegateCommand(_generateSound, null); } }

        public Array WaveForms { get { return Enum.GetValues(typeof(WaveForm)); } }

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

        public string CurrentWaveFormName
        {
            set
            {
                SetProperty(ref _currentWaveFormName, value);

                // set wave form in back-end
                WaveForm waveForm;
                if(Enum.TryParse(value, out waveForm))
                    _soundGenerator.SetWaveForm(waveForm);
            }
        }

        private void VirtualKeyboard_NotePressed(object sender, float frequency)
        {
            _soundGenerator.GenerateSound(frequency);
        }

        private void _generateSound(object commandParameter)
        {
            _soundGenerator.GenerateSound(Frequency);
        }
    }
}
