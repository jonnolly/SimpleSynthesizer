   M S B A M L   `   `   `  ����F  BSimpleSynth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SimpleSynthSimpleSynth  5   	       SimpleSynth.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:SimpleSynth     5   	   -   5          � Title$  
MainWindow��5   	   $	��700��6   $	��800��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��2��6;   ��6   �� *Oscillators+��5      �� 5      .R�5         SimpleSynth.Oscillator  5      5      �� 5   
   $��1��6    � Minimum$	 240Q�5    >    � Maximum$
 1000Q�6L   $	��500��5!      $��2��6   $��Center=�5"      $��Center=�6.    � Value 5       �� *	Frequency+ �� Mode$ OneWayToSource=�5"   .   �� 5#   
   $��2��6   $��Frequency: ��6   �� 5$   
   $��2��6   $��1��6   ��6-   �� *	Frequency+$ OneWay=��� 5%   
   -   5         ��Name$ btnGenerateSound��5%      $��3��6   $��2��68   ��6L   �� *GenerateSound+.��6n   Generate sound6~   �� 5&   
   $��4��6    �� Orientation$ Vertical=�6"   .+�5'      �� $B��<To use virtual keyboard, use Keys: Q 2 W 3 E R 5 T 6 Y 7 U I��6   �� 5(      .��6   	Octave:6   �� 5)      $ 
Horizontal=�6   .+�5*      �� ��6   �� *PreviousOctave+.��6<   
Previous6F   �� 5+      ��6   �� *CurrentOctave+�� 5,      ��6   �� *
NextOctave+.��68   Next6>   5.      5/      50      51      