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
    public class SimpleAI :BaseAI, IMoveTo ,IThink
    {
        AbstractGhost _ghost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="field">Game field matrix</param>
        public SimpleAI(int[,] field) : base(field)
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
            MoveTo(RandomPointNearPlayer(3,pacman), ghost.Speed);
        }
        /// <summary>
        /// Move context gost to point with speed
        /// </summary>
        /// <param name="to">Target point</param>
        /// <param name="speed">Moving speed</param>
        public void MoveTo( FieldPoint to, Speeds speed)
        {
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
