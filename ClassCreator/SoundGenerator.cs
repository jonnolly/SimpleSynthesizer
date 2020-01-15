using System;
using System.IO;
using System.Media;
using ClassCreator;
using Interfaces;

namespace Factory
{
    public class SoundGenerator : ISoundGenerator
    {
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;
        private WaveForm _waveForm = WaveForm.Sine;

        public void GenerateSound(float frequency)
        {
            byte[] binaryWave = new byte[SAMPLE_RATE * sizeof(short)];
            short[] wave = new short[SAMPLE_RATE];

            if (_waveForm == WaveForm.Sine)
                wave = WaveFormGenerator.Sine(frequency, SAMPLE_RATE);
            else if (_waveForm == WaveForm.Noise)
                wave = WaveFormGenerator.Noise(SAMPLE_RATE);
            else if (_waveForm == WaveForm.Square)
                wave = WaveFormGenerator.Square(frequency, SAMPLE_RATE);
            else if (_waveForm == WaveForm.Saw)
                wave = WaveFormGenerator.Saw(frequency, SAMPLE_RATE);
            else if (_waveForm == WaveForm.Triangle)
                wave = WaveFormGenerator.Triangle((short)frequency, SAMPLE_RATE);
            else
                wave = WaveFormGenerator.Sine(frequency, SAMPLE_RATE);

            Buffer.BlockCopy(wave, 0, binaryWave, 0, wave.Length * sizeof(short));

            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
            {
                short blockAlign = BITS_PER_SAMPLE / 8;
                int subChunkTwoSize = SAMPLE_RATE * blockAlign;
                binaryWriter.Write(new[] { 'R', 'I', 'F', 'F' });
                binaryWriter.Write(36 + subChunkTwoSize);
                binaryWriter.Write(new[] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });
                binaryWriter.Write(16);
                binaryWriter.Write((short)1);
                binaryWriter.Write((short)1);
                binaryWriter.Write(SAMPLE_RATE);
                binaryWriter.Write(SAMPLE_RATE * blockAlign);
                binaryWriter.Write(blockAlign);
                binaryWriter.Write(BITS_PER_SAMPLE);
                binaryWriter.Write(new[] { 'd', 'a', 't', 'a' });
                binaryWriter.Write(subChunkTwoSize);
                binaryWriter.Write(binaryWave);

                memoryStream.Position = 0;

                new SoundPlayer(memoryStream).Play();
            }
        }

        public void SetWaveForm(WaveForm waveForm)
        {
            _waveForm = waveForm;
        }
    }
}
