using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AI_Interfaces
{
    public interface IMoveTo
    {
       void MoveTo( FieldPoint to, Speeds speed);
    }
}
