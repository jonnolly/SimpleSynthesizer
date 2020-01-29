namespace Interfaces
{
    public class OscillatorParams
    {
        // Wave form of oscillator signal
        public WaveForm CurrentWaveForm { get; set; } = WaveForm.Sine;

        // volume of oscillator signal in the overall mix (0.0 --> 1.0)
        public float Volume { get; set; } = 1.0f;

        // Octave offset of oscillator signal
        public float OctaveOffset { get; set; } = 0.0f;

    }
}
