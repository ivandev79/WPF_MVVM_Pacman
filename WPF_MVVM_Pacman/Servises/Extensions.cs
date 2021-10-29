using Core;
using System;
using System.Collections.Generic;

namespace Servises
{
    static class Extensions
    {
        private static Random r = new Random();
        /// <summary>
        /// Get list of points with param from array
        /// </summary>
        /// <param name="array">Extansion array signature int[,]</param>
        /// <param name="param">value will which the selection </param>
        /// <returns> List<FieldPoint> with coords where you param </returns>
        public static List<FieldPoint> TakeAllPointsWhere(this int[,] array, int param)
        {
            int n = array.GetUpperBound(0) + 1;
            List<FieldPoint> fieldPoints = new List<FieldPoint>();
         
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (array[i, j] == param)
                    {
                        fieldPoints.Add(new FieldPoint(i, j));
                    }
                }
            }
            return fieldPoints;
        }

        /// <summary>
        /// Random list shuffle
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
