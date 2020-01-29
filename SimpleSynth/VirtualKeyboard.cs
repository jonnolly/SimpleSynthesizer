using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SimpleSynth
{
    public static class VirtualKeyboard
    {
        public static Dictionary<Key, float> defaultKeyboardFrequencies = new Dictionary<Key, float>()
        {
            { Key.I, 523.3f },      // tenor C
            { Key.U, 493.9f },      // B
            { Key.D7, 466.2f },     // A#
            { Key.Y, 440.0f },      // A
            { Key.D6, 415.3f },     // G#
            { Key.T, 392.0f },      // G
            { Key.D5, 370.0f },     // F#
            { Key.R, 349.2f },      // F
            { Key.E, 329.6f },      // E
            { Key.D3, 311.1f },     // D#
            { Key.W, 293.6f },      // D
            { Key.D2, 277.2f },     // C#
            { Key.Q, 261.6f },      // Middle C
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
