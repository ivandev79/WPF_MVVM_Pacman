namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Score
    {
        public int ScoreId { get; set; }
        public string UserName { get; set; }
        public Nullable<int> MyScore { get; set; }
        public System.DateTime DateAdded { get; set; }

        public override string ToString()
        {
            return $"{UserName} : {MyScore}";
        }

        public int CompareTo(Score p)
        {
            return this.UserName.CompareTo(p.UserName);
        }

        public bool Equals(Score other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return UserName.Equals(other.UserName) && MyScore.Equals(other.MyScore) && DateAdded.Equals(other.DateAdded);
        }

        public override int GetHashCode()
        {
            return ((this.MyScore != null ? this.MyScore.GetHashCode() : 0) * 397) ^ (this.MyScore != null ? this.MyScore.GetHashCode() : 0);
        }
    }


}
