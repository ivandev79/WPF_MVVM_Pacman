using Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    /// <summary>
    /// EF Data base Firs model
    /// </summary>
    public class ScoreData
    {
        public ScoreData()
        {
        }

        /// <summary>
        /// Save Results
        /// </summary>
        /// <param name="playerName">Player Name</param>
        /// <param name="score">Player score</param>
        public static void SaveResult(string playerName,int score)
        {
            using (UserScoreEntities db = new UserScoreEntities())
            {
                var newScore = new Score() { UserName=playerName , MyScore = score,DateAdded = DateTime.Now};
                db.Users.Add(newScore);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Get 10 top results
        /// </summary>
        /// <returns></returns>
        public static List<Score> GetTopResult()
        {
            using (UserScoreEntities db = new UserScoreEntities())
            {
                var resList = db.Users.OrderByDescending(x => x.MyScore).ToList();
                if (resList.Count >10)
                {
                    resList = resList.Take(10).ToList();
                }
                else
                {
                    resList = resList.Take(resList.Count).ToList();
                }
              
               return resList;
            }
        }

    }
}
