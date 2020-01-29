using Interfaces;
using System;

namespace SimpleSynth
{
    public class OscillatorVM : ViewModelBase
    {
        public Array WaveForms { get { return Enum.GetValues(typeof(WaveForm)); } }

        public OscillatorVM(string title, bool defaultEnabled = false)
        {
            Title = title;
            IsEnabled = defaultEnabled;
        }

        public WaveForm CurrentWaveFormName
        {
            get { return OscillatorData.CurrentWaveForm; }
            set
            {
                // set wave form in back-end
                OscillatorData.CurrentWaveForm = value;
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
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