using System;
using System.Collections.Generic;
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

        // todo: Changes OscillatorParams to List<OscillatorParams> and filter out for isEnabled before sending it here
        public void GenerateSound(float frequency, OscillatorParams oscillator1, OscillatorParams oscillator2)
        {

            byte[] binaryWave = new byte[SAMPLE_RATE * sizeof(short)];
            short[] wave1 = GetWave(oscillator1.CurrentWaveForm, frequency);
            short[] wave2 = GetWave(oscillator2.CurrentWaveForm, frequency);

            short[] resultantWave = new short[SAMPLE_RATE];

            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                resultantWave[i] = (short)((wave1[i] + wave2[i]) / 2);
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

        public void SetWaveForm(WaveForm waveForm)
        {
            _waveForm = waveForm;
        }
    }
}
