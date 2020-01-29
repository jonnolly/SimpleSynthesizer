using Interfaces;
using System;

namespace SimpleSynth
{
    public class OscillatorVM : ViewModelBase
    {
        private string _currentWaveFormName;

        public Array WaveForms { get { return Enum.GetValues(typeof(WaveForm)); } }

        public string CurrentWaveFormName
        {
            set
            {
                SetProperty(ref _currentWaveFormName, value);

                // set wave form in back-end
                WaveForm waveForm;
                if (Enum.TryParse(value, out waveForm))
                    OscillatorData.CurrentWaveForm = waveForm;
            }
        }

        // volume of oscillator signal in the overall mix
        public float Volume
        {
            get { return OscillatorData.Volume; }
            set { OscillatorData.Volume = value; }
        }

        public float OctaveOffset
        {
            get { return OscillatorData.OctaveOffset; }
            set { OscillatorData.OctaveOffset = value; }
        }

        public bool IsEnabled { get; set; }

        // Oscillator data to be passed into the model
        public OscillatorParams OscillatorData { get; private set; } = new OscillatorParams();
    }
}