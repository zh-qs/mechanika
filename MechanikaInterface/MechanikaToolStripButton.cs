using System;
using System.Collections.Generic;
using System.Text;

namespace MechanikaInterface
{
    class MechanikaToolStripButton : System.Windows.Forms.ToolStripButton
    {
        public string Command { get; set; }
        public int NumberOfArgumentsNeeded { get; set; }
    }
}
