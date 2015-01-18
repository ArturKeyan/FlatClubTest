using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Models
{
    public class BaseModel
    {
        new public static bool Equals(object objA, object objB)
        {
            return ((objA == objB) || (((objA != null) && (objB != null)) && objA.Equals(objB)));
        }

        public static bool Equals(IEnumerable<object> objA, IEnumerable<object> objB)
        {
            return ((objA == objB) || (((objA != null) && (objB != null)) && objA.SequenceEqual(objB)));
        }
    }
}
