using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Custom class for work with field coords
    /// </summary>
    public class FieldPoint
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y coordinate
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Innitialize obj and writes both coords
        /// </summary>
        /// <param name="x">X - coordinate</param>
        /// <param name="y">Y - coordinate</param>
        public FieldPoint(int x,int y)
        {
            X = x; Y = y;
        }
        /// <summary>
        /// Default 
        /// </summary>
        public FieldPoint() { }
        /// <summary>
        /// Just for rewrite both coords
        /// </summary>
        /// <param name="x">X - coordinate</param>
        /// <param name="y">Y - coordinate</param>
        public void New(int x, int y)
        {
            X = x; Y = y;
        }
        #region override
        public override string ToString()
        {
            return $"x={X},y={Y}";
        }
        public override bool Equals(object obj)
        {
            if (obj is FieldPoint )
            {
                return this.X == (obj as FieldPoint).X && this.Y == (obj as FieldPoint).Y;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public static bool operator ==(FieldPoint p1, FieldPoint p2)
        {
            return p1.Equals(p2);
        }
        public static bool operator !=(FieldPoint p1, FieldPoint p2)
        {
            return !p1.Equals(p2);
        }

        public static FieldPoint operator -(FieldPoint p1, FieldPoint p2)
        {
            return new FieldPoint { X = p1.X - p2.X ,Y= p1.Y - p2.Y };
        }
        #endregion
    }
}
