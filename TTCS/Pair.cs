using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCS
{
    public class Pair
    {
        public string Key;
        public int Value;

        public Pair(string key, int val)
        {
            Key = key;
            Value = val;
        }

        public override string ToString()
        {
            return Key;
        }
    }
}
