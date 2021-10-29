using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Core.NPC
{
    public class GreenGhost : AbstractGhost
    {
     
        public GreenGhost(dynamic behavior):base((object)behavior)
        {

        }

        public GreenGhost(dynamic behavior,Image modelImage) : base((object)behavior)
        {
            Model = modelImage;
        }

        public FieldPoint zz(PacmanEssence pacman)
        {
            return Behavior.PlayerPoint( pacman); ;
        }
    }
}
