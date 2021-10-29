using Core.AI_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Base class for create custom behavior
    /// Each returned FieldPoint is Available to move
    /// </summary>
    public abstract class BaseAI
    {
        private Random _r = new Random();
        protected int[,] _fieldMatrix;
        protected int _fieldRange;
        public bool IsThinkEachTurn { get; set; } = false;
        protected List<FieldPoint> AvailableFieldPoints;


        public BaseAI(int[,] field)
        {
            _fieldMatrix = field;
            _fieldRange = _fieldMatrix.GetUpperBound(0)+1;
            FillAvailableFieldPoints();
        }

        private void FillAvailableFieldPoints()
        {
            AvailableFieldPoints = new List<FieldPoint>();
            for (int i = 0; i < _fieldRange; i++)
            {
                for (int j = 0; j < _fieldRange; j++)
                {
                    if (_fieldMatrix[i,j] == 0)
                    {
                        AvailableFieldPoints.Add(new FieldPoint(i, j));
                    }
                }
            }
        }

        /// <summary>
        /// is available point
        /// </summary>
        /// <returns>Random available point</returns>
        public bool IsAvailablePoint(FieldPoint p)
        {
            if (_fieldMatrix[p.X,p.Y]==0)
            {
                return true;
            }
            return false;
        }

        #region Select a field point
        /// <summary>
        /// Get a random point
        /// </summary>
        /// <returns>Random available point</returns>
        public FieldPoint RandomPoint()
        {
            int count = 5;
            FieldPoint tempPoint;
            do
            {
                tempPoint = new FieldPoint(_r.Next(0, _fieldRange), _r.Next(0, _fieldRange));
                if (IsAvailablePoint(tempPoint))
                {
                    return tempPoint;
                }
                count++;
            } while (count < 5);

            return AvailableFieldPoints[_r.Next(AvailableFieldPoints.Count)];
        }
        /// <summary>
        /// Get a random point near player 
        /// </summary>
        /// <returns> available point in area of player</returns>
        public FieldPoint RandomPointNearPlayer(int area, PacmanEssence player)
        {

            int x_start, y_start, x_end, y_end;
            #region Fridges
            if (player.Point.X - area > 0)
            {
                x_start = player.Point.X - area;
            }
            else
            {
                x_start = 0;
            }
            if (player.Point.X + area < _fieldRange)
            {
                x_end = player.Point.X + area;
            }
            else
            {
                x_end = _fieldRange - 1;
            }
            if (player.Point.Y + area < _fieldRange)
            {
                y_end = player.Point.Y + area;
            }
            else
            {
                y_end = _fieldRange - 1;
            }
            if (player.Point.Y - area > 0)
            {
                y_start = player.Point.Y - area;
            }
            else
            {
                y_start = 0;
            }

            #endregion
            int count = 5;
            FieldPoint tempPoint;
            do
            {
                tempPoint = new FieldPoint(_r.Next(x_start, x_end), _r.Next(y_start, y_end));
                if (IsAvailablePoint(tempPoint))
                {
                    return tempPoint;
                }
                count++;
            } while (count < 5);
            return AvailableFieldPoints[_r.Next(AvailableFieldPoints.Count)];
        }

        /// <summary>
        /// Get player point
        /// </summary>
        /// <param name="player">Pacman</param>
        /// <returns>player point </returns>
        public FieldPoint PlayerPoint(PacmanEssence player)
        {
            return new FieldPoint(player.Point.X, player.Point.Y);
        } 
        #endregion


    }
}
