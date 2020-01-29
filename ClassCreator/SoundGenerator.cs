using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private short[] GetWave(WaveForm waveForm, float frequency)
        {
            short[] wave = new short[SAMPLE_RATE];

            switch (waveForm)
            {
                case WaveForm.Noise:
                    return WaveFormGenerator.Noise(SAMPLE_RATE);
                
                case WaveForm.Square:
                    return WaveFormGenerator.Square(frequency, SAMPLE_RATE);
                
                case WaveForm.Saw:
                    return WaveFormGenerator.Saw(frequency, SAMPLE_RATE);
                
                case WaveForm.Triangle:
                    return WaveFormGenerator.Triangle((short)frequency, SAMPLE_RATE);

                case WaveForm.Sine:
                default:
                    return WaveFormGenerator.Sine(frequency, SAMPLE_RATE);
            }
        }

        // todo: change to only have one loop. this method is not very efficient.
        public void GenerateSound(float frequency, List<OscillatorParams> oscillatorData)
        {

            byte[] binaryWave = new byte[SAMPLE_RATE * sizeof(short)];

            var waves = oscillatorData.Select(oscillator => GetWave(oscillator.CurrentWaveForm, frequency)).ToList();

            short[] resultantWave = new short[SAMPLE_RATE];

            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                short waveSum = 0;
                foreach (var wave in waves)
                {
                    waveSum += wave[i];
                }

                resultantWave[i] = (short)((waveSum) / oscillatorData.Count);
            }

            Buffer.BlockCopy(resultantWave, 0, binaryWave, 0, resultantWave.Length * sizeof(short));

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
    }
}
