using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Core.NPC
{
    public class RedGhost : AbstractGhost
    {
        public RedGhost(dynamic behavior) : base((object)behavior)
        {
        }

        public RedGhost(dynamic behavior, Image modelImage) : base((object)behavior)
        {
            Model = modelImage;
        }
    }
}
