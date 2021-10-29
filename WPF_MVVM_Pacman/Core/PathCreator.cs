using Logs;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core
{
    /// <summary>
    /// Special class using for AI path
    /// </summary>
    public class PathCreator
    {
        private int n = 0;
        private  int[,] A;
        private int temp = 100;
        private List<FieldPoint> _path;

        /// <param name="Path">Get path</param>
        public List<FieldPoint> Path
        {
            get { return _path; }
            private set { _path = value; }
        }

        /// <param name="Array">Game matrix</param>
        public int[,] Array
        {
            get { return A; }
            set { A = value; }
        }
        /// <summary>
        /// Init context params
        /// </summary>
        /// <param name="field">Current/Context game field</param>
        public PathCreator(int[,] field)
        {
            n = field.GetUpperBound(0)+1 ;
            A = (int[,])field.Clone();
            Path = new List<FieldPoint>();
        }
        /// <summary>
        /// Checking the ways from point A to B
        /// </summary>
        /// <param name="start">Start point</param>
        /// <param name="end">End point</param>
        /// <returns>If the path exists, its points are returned</returns>
        public List<FieldPoint> GetWay(FieldPoint start, FieldPoint end)
        {
            try
            {
                var str = new string[A.GetUpperBound(0) + 1];
                A[start.X, start.Y] = 2;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        str[i] += A[i, j] + " ";
                    }
                }

                if (HaveWay(end.X, end.Y, start))
                {
                    for (int i = 0; i < n; i++)
                    {
                        str[i] = "";
                        for (int j = 0; j < n; j++)
                        {
                            str[i] += A[i, j] + " ";
                        }
                    }
                    n--;
                    LookingForStepsToPoint(end.X, end.Y, start);
                    n++;
                    Path.Reverse();
                    return Path;
                }
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x">End X coordinate</param>
        /// <param name="y">End Y coordinate</param>
        private void LookingForStepsToPoint(int x, int y,  FieldPoint start)
        {
            try
            {
                Path.Add(new FieldPoint(x, y));

                if (x == start.X && y == start.Y) { return; }

                if (A[x, y] == temp)
                {
                    temp--;
                    if (y > 0 && A[x, y - 1] == temp)
                    {
                        LookingForStepsToPoint(x, y - 1, start);
                        return;
                    }
                    if (x > 0 && A[x - 1, y] == temp)
                    {
                        LookingForStepsToPoint(x - 1, y, start);
                        return;
                    }
                    if (y < n && A[x, y + 1] == temp)
                    {
                        LookingForStepsToPoint(x, y + 1, start);
                        return;
                    }
                    if (x < n && A[x + 1, y] == temp)
                    {
                        LookingForStepsToPoint(x + 1, y, start);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error",ex));
            }
        }

        private bool HaveWay(int x, int y, FieldPoint start)
        {
            try
            {
                bool fl = true;
                A[start.X, start.Y] = temp;
                while (A[x, y] == 0 && fl)
                {
                    fl = false;
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                            if (A[i, j] == temp)
                            {
                                if (j > 0 && A[i, j - 1] == 0)
                                {
                                    A[i, j - 1] = temp + 1;
                                    fl = true;
                                }
                                if (i > 0 && A[i - 1, j] == 0)
                                {
                                    A[i - 1, j] = temp + 1;
                                    fl = true;
                                }
                                if (j < n - 1 && A[i, j + 1] == 0)
                                {
                                    A[i, j + 1] = temp + 1;
                                    fl = true;
                                }
                                if (i < n - 1 && A[i + 1, j] == 0)
                                {
                                    A[i + 1, j] = temp + 1;
                                    fl = true;
                                }
                            }
                    temp++;
                }

                // ! = 0 becouse it's becomes a wave number
                return (A[x, y] != 0);
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
                return (A[x, y] != 0);
        }
    }
}
