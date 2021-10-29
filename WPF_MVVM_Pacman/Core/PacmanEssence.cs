using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Core
{
    /// <summary>
    /// All iformation about pacman in game
    /// </summary>
    public class PacmanEssence
    {
        private FieldPoint _point;
        private FieldPoint _nextPoint;
        private int _iamageAngle;
        /// <summary>
        /// Angle to rotate main image model
        /// </summary>
        public int ImgAngle
        {
            get { return _iamageAngle; }
            set { _iamageAngle = value; }
        }
        /// <summary>
        /// Point where pacman now
        /// </summary>
        public FieldPoint Point
        {
            get { return _point; }
            set { _point = value; }
        }
        /// <summary>
        /// pacman move to this point
        /// </summary>
        public FieldPoint NextPoint
        {
            get { return _nextPoint; }
            set { _nextPoint = value; }
        }
        /// <summary>
        /// Pacman model
        /// </summary>
        public Image MainImage;

        public PacmanEssence()        {        }
        /// <summary>
        ///     Create pacman
        /// </summary>
        /// <param name="p">Start point</param>
        /// <param name="img">Main model</param>
        public PacmanEssence(FieldPoint p,Image img)
        {
            Point = p;
            MainImage = img;
        }
    }
}
