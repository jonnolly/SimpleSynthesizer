using System;

namespace ClassCreator
{
    public static class WaveFormGenerator
    {
        public static short[] Sine(float frequency, int sampleRate)
        {
            short[] wave = new short[sampleRate];

            for (int i = 0; i < sampleRate; i++)
            {
                var t = (Math.PI * 2 * frequency) / sampleRate;

                wave[i] = Convert.ToInt16(short.MaxValue * Math.Sin(t * i));
            }

            return wave;
        }

        public static short[] Noise(int sampleRate)
        {
            Random rnd = new Random();
            int amplitude = 10000;
            short randomValue = 0;

            short[] wave = new short[sampleRate];

            for (int i = 0; i < sampleRate; i++)
            {
                randomValue = Convert.ToInt16(rnd.Next(-amplitude, amplitude));
                wave[i] = randomValue;
            }

            return wave;
        }
    }
}