using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MechanikaInterface
{
    public static class TextBoxExtender
    {
        public static void Backspace(this TextBox textBox, int noOfChars)
        {
            if (textBox.Lines.Length == 0) return;
            if (textBox.Lines[textBox.Lines.Length - 1].Length < noOfChars) textBox.Lines[textBox.Lines.Length - 1] = string.Empty;
            textBox.Lines[textBox.Lines.Length - 1] = textBox.Lines[textBox.Lines.Length - 1].Substring(0, textBox.Lines[textBox.Lines.Length - 1].Length - noOfChars);
        }
    }
}
