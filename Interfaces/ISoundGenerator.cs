using System.Collections.Generic;

namespace Interfaces
{
    public interface ISoundGenerator
    {
        void GenerateSound(float frequency, List<OscillatorParams> oscillatorData);
    }
}
