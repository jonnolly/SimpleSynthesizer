using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SimpleSynth
{
    public static class VirtualKeyboard
    {
        public static Dictionary<Key, float> defaultKeyboardFrequencies = new Dictionary<Key, float>()
        {
            { Key.Q, 261.6f },      // Middle C
            { Key.W, 293.6f },      // D
            { Key.E, 329.6f },      // E
            { Key.R, 349.2f },      // F
            { Key.T, 392.0f },      // G
            { Key.Y, 440.0f },      // A
            { Key.U, 493.9f },      // B
            { Key.I, 523.3f }       // tenor C
        };

        public static void PressKey(Key key)
        {
            if (defaultKeyboardFrequencies.ContainsKey(key))
            {
                float frequency = defaultKeyboardFrequencies[key];
                NotePressed.Invoke(null, frequency);
            }
        }

        public static event EventHandler<float> NotePressed;
    }
}
