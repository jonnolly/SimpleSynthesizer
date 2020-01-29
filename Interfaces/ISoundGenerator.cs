using System.Collections.Generic;

namespace Interfaces
{
    public interface ISoundGenerator
    {
        void GenerateSound(float frequency, OscillatorParams oscillator1, OscillatorParams oscillator2);
        void SetWaveForm(WaveForm waveType);
    }
}
