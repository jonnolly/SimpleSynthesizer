using Interfaces;
using System;
using System.Windows.Input;

namespace SimpleSynth
{
    public class MainWindowVM : ViewModelBase
    {
        private ISoundGenerator _soundGenerator;

        public MainWindowVM(ISoundGenerator soundGenerator)
        {
            _soundGenerator = soundGenerator;

        }

        public ICommand GenerateSound { get { return new DelegateCommand(_generateSound, null); } }

        public ICommand SetWaveType { get { return new DelegateCommand(_setWaveType, null); } }

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

        private void _generateSound(object commandParameter)
        {
            _soundGenerator.GenerateSound(Frequency);
        }

        private void _setWaveType(object commandParameter)
        {
            // default will be Sine
            WaveForm waveForm = WaveForm.Sine;

            Enum.TryParse(commandParameter.ToString(), out waveForm);

            _soundGenerator.SetWaveForm(waveForm);
        }
    }
}
