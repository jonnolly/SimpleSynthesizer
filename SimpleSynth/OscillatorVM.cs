using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSynth
{
    public class OscillatorVM : ViewModelBase
    {
        private string _currentWaveFormName;
        private ISoundGenerator _soundGenerator;

        public OscillatorVM(ISoundGenerator soundGenerator)
        {
            _soundGenerator = soundGenerator;
        }

        public Array WaveForms { get { return Enum.GetValues(typeof(WaveForm)); } }

        public string CurrentWaveFormName
        {
            set
            {
                SetProperty(ref _currentWaveFormName, value);

                // set wave form in back-end
                WaveForm waveForm;
                if (Enum.TryParse(value, out waveForm))
                    _soundGenerator.SetWaveForm(waveForm);
            }
        }
    }
}
