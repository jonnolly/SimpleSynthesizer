using System.Collections.Generic;

namespace Interfaces
{
    public interface ISoundGenerator
    {
        void GenerateSound(float frequency, int octave, List<OscillatorParams> oscillatorData);
    }
}
