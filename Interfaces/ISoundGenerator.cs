namespace Interfaces
{
    public interface ISoundGenerator
    {
        void GenerateSound(float frequency);
        void SetWaveForm(WaveForm waveType);
    }
}
