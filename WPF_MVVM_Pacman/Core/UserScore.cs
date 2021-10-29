using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// User information
    /// </summary>
    public class UserScore
    {
        private int _score;
        /// <summary>
        /// User Score
        /// </summary>
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }

        private string _name;
        /// <summary>
        /// User Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private DateTime _date;
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
    }
}
