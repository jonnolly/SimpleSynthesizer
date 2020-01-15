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

        public static short[] Square(float frequency, int sampleRate)
        {
            int amplitude = 10000;
            short[] wave = new short[sampleRate];
            var t = (Math.PI * 2 * frequency) / sampleRate;

            for (int i = 0; i < sampleRate; i++)
            {
                wave[i] = Convert.ToInt16(amplitude * Math.Sign(Math.Sin(t * i)));
            }

            return wave;
        }

        public static short[] Saw(float frequency, int sampleRate)
        {
            int amplitude = 10000;
            short[] wave = new short[sampleRate];

            // Determine the number of samples per wavelength
            int samplesPerWavelength = Convert.ToInt32(sampleRate / frequency);

            // Determine the amplitude step for consecutive samples
            short ampStep = Convert.ToInt16((amplitude * 2) / samplesPerWavelength);

            // Temporary sample value, added to as we go through the loop
            short tempSample = (short)-amplitude;

            // Total number of samples written so we know when to stop
            int totalSamplesWritten = 0;

            while (totalSamplesWritten < sampleRate)
            {
                tempSample = (short)-amplitude;

                for (uint i = 0; i < samplesPerWavelength && totalSamplesWritten < sampleRate; i++)
                {
                    tempSample += ampStep;
                    wave[totalSamplesWritten] = tempSample;

                    totalSamplesWritten++;
                }
            }

            return wave;
        }

        public static short[] Triangle(short ampStep, int sampleRate)
        {
            int amplitude = 10000;
            short[] wave = new short[sampleRate];

            short tempSample = 0;

            // amp step plays a similar role to frequency
            //short ampStep = 1000;

            for (int i = 0; i < sampleRate - 1; i++)
            {
                // Negate ampstep whenever it hits the amplitude boundary
                if (Math.Abs(tempSample) > amplitude)
                    ampStep = (short)-ampStep;

                tempSample += ampStep;
                wave[i] = tempSample;
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