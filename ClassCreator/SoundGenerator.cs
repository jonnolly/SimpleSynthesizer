﻿using System;
using System.IO;
using System.Media;
using Interfaces;

namespace Factory
{
    public class SoundGenerator : ISoundGenerator
    {
        private const int SAMPLE_RATE = 44100;
        private const short BITS_PER_SAMPLE = 16;

        public void GenerateSound(float frequency)
        {
            short[] wave = new short[SAMPLE_RATE];
            byte[] binaryWave = new byte[SAMPLE_RATE * sizeof(short)];

            for (int i = 0; i < SAMPLE_RATE; i++)
            {
                wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(((Math.PI * 2 * frequency) / SAMPLE_RATE) * i));
            }

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
    }
}
