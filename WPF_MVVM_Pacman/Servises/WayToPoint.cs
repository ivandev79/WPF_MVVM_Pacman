using Core;
using Logs;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Servises
{
    /// <summary>
    /// Generate map with out unavailable points
    /// </summary>
    class WayToPoint
    {
        private int n { get; set; }
        readonly private int[,] A;
        private Random _r = new Random();
        public FieldPoint StartPoint { get; set; }
        private bool changed = false;

        public WayToPoint(int[,] field, int range)
        {
            n = range;
            A = new int[n, n];
            A = field;
        }

        internal void CheckField()
        {
            try
            {
                List<FieldPoint> AvailablePoints = A.TakeAllPointsWhere(0);
                //Just for more random
                AvailablePoints.Shuffle();
                foreach (FieldPoint it in AvailablePoints)
                {
                    CheckPoint(it);
                }
                foreach (FieldPoint it in AvailablePoints)
                {
                    CheckWays(it);
                }
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }

        private void CheckWays(FieldPoint p)
        {
            if (p == StartPoint) return;
            HaveWay(StartPoint, p);
        }
        /// <summary>
        /// Checking ways between points and build the way if it's not exist
        /// </summary>
        /// <param name="from"> Point from</param>
        /// <param name="to"> Point to</param>
        /// <returns></returns>
        private bool HaveWay(FieldPoint from, FieldPoint to)
        {
            try
            {
                int[,] tmpField = new int[n, n];
                Array.Copy(A, 0, tmpField, 0, A.Length);
                tmpField[from.X, from.Y] = 1000;
                bool fl = true;
                int i, j, i_end = to.X, j_end = to.Y, temp = 1000;
                while (tmpField[i_end, j_end] == 0 && fl)
                {
                    fl = false;
                    for (i = 0; i < n; i++)
                        for (j = 0; j < n; j++)
                            if (tmpField[i, j] == temp)
                            {
                                if (j > 0 && tmpField[i, j - 1] == 0)
                                {
                                    tmpField[i, j - 1] = temp + 1;
                                    fl = true;
                                }
                                if (i > 0 && tmpField[i - 1, j] == 0)
                                {
                                    tmpField[i - 1, j] = temp + 1;
                                    fl = true;
                                }
                                if (j < n - 1 && tmpField[i, j + 1] == 0)
                                {
                                    tmpField[i, j + 1] = temp + 1;
                                    fl = true;
                                }
                                if (i < n - 1 && tmpField[i + 1, j] == 0)
                                {
                                    tmpField[i + 1, j] = temp + 1;
                                    fl = true;
                                }
                                //debugString2(tmpField, i, j, temp);

                            }
                    temp++;
                }

                if ((tmpField[i_end, j_end] == 0))
                {
                    FieldPoint p = ClearWayTo(to, tmpField);
                    A[p.X, p.Y] = 0;
                    HaveWay(from, to);
                }
                // ! = 0 becouse it's becomes a wave number
                return (tmpField[i_end, j_end] != 0);
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
            return false;
        }
        /// <summary>
        /// Change main field , build ways to point
        /// </summary>
        /// <param name="from">Point way to which isn't exist</param>
        /// <param name="tmpField">Field after wawe alchoritm change </param>
        /// <returns></returns>
        private FieldPoint ClearWayTo(FieldPoint from, int[,] tmpField)
        {
            try
            {
                FieldPoint PointToChange = new FieldPoint();
                tmpField[from.X, from.Y] = 100;
                bool fl = true;
                int i, j, temp = 100;
                while (fl)
                {
                    fl = false;
                    for (i = 0; i < n; i++)
                        for (j = 0; j < n; j++)
                            if (tmpField[i, j] == temp)
                            {
                                if (j > 0)
                                {
                                    fl = true;
                                    if (tmpField[i, j - 1] >= 1000)
                                    {
                                        PointToChange.New(i, j);
                                        fl = false;
                                        i = j = n;
                                        continue;
                                    }
                                    else
                                        tmpField[i, j - 1] = temp + 1;
                                }
                                if (i > 0)
                                {
                                    fl = true;
                                    if (tmpField[i - 1, j] >= 1000)
                                    {
                                        PointToChange.New(i, j);
                                        fl = false;
                                        i = j = n;
                                        continue;
                                    }
                                    else
                                        tmpField[i - 1, j] = temp + 1;
                                }
                                if (j < n - 1)
                                {
                                    fl = true;
                                    if (tmpField[i, j + 1] >= 1000)
                                    {
                                        PointToChange.New(i, j);
                                        fl = false;
                                        i = j = n;
                                        continue;
                                    }
                                    else
                                        tmpField[i, j + 1] = temp + 1;
                                }
                                if (i < n - 1)
                                {
                                    fl = true;
                                    if (tmpField[i + 1, j] >= 1000)
                                    {
                                        PointToChange.New(i, j);
                                        fl = false;
                                        i = j = n;
                                        continue;
                                    }
                                    else
                                        tmpField[i + 1, j] = temp + 1;
                                }
                            }
                    temp++;
                }
                return PointToChange;
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
            return null;
        }

        private void CheckPoint(FieldPoint p, bool flag = false)
        {
            try
            {
                int canMove = 0;
                if (p.Y > 0)
                {
                    canMove += A[p.X, p.Y - 1] == 0 ? 1 : 0;
                }
                if (p.X > 0)
                {
                    canMove += A[p.X - 1, p.Y] == 0 ? 1 : 0;
                }
                if (p.Y < n - 1)
                {
                    canMove += A[p.X, p.Y + 1] == 0 ? 1 : 0;
                }
                if (p.X < n - 1)
                {
                    canMove += A[p.X + 1, p.Y] == 0 ? 1 : 0;
                }

                if (flag)
                {
                    //Almost 2 ways
                    if (canMove >= 2)
                    {
                        A[p.X, p.Y] = 0;
                        changed = true;
                    }
                }

                if (canMove < 1)
                {
                    changed = false;
                    if (p.Y > 0)
                        CheckPoint(new FieldPoint(p.X, p.Y - 1), true);
                    if (p.X > 0 && !changed)
                        CheckPoint(new FieldPoint(p.X - 1, p.Y), true);
                    if (p.Y < n - 1 && !changed)
                        CheckPoint(new FieldPoint(p.X, p.Y + 1), true);
                    if (p.X < n - 1 && !changed)
                        CheckPoint(new FieldPoint(p.X + 1, p.Y), true);
                }
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }
    }
}
