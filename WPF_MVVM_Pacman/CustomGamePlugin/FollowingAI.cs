using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.AI_Interfaces;
using Core.NPC;

namespace CustomGamePlugin
{
    public class FollowingAI :BaseAI, IMoveTo ,IThink
    {
        private AbstractGhost _ghost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field">Game field matrix</param>
        public FollowingAI(int[,] field) : base(field)
        {
        }

        /// <summary>
        /// Action logic when ghost complete moving 
        /// </summary>
        /// <param name="ghost">Current ghost</param>
        /// <param name="pacman">Context pacman</param>
        public void Think(AbstractGhost ghost,PacmanEssence pacman)
        {
            _ghost = ghost;
            MoveTo(PlayerPoint(pacman), ghost.Speed);
        }
        /// <summary>
        /// Move context gost to point with speed
        /// </summary>
        /// <param name="to">Target point</param>
        /// <param name="speed">Moving speed</param>
        public void MoveTo( FieldPoint to, Speeds speed)
        {
            _ghost.Speed = speed;
            if (this.IsThinkEachTurn)
            {

            }
            else
            {
                _ghost.Path =new PathCreator(_fieldMatrix).GetWay(_ghost.FieldPointNow, to);
            }
        }
    }
}
