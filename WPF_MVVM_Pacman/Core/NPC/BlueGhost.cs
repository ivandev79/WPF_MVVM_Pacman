using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Core.NPC
{
    public class BlueGhost : AbstractGhost
    {
        public BlueGhost(dynamic behavior) : base((object)behavior)
        {
        }

        public BlueGhost(dynamic behavior, Image modelImage) : base((object)behavior)
        {
            Model = modelImage;
        }

    }
}
