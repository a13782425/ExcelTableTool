using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableCore
{
    public static class Extension
    {
        public static unsafe string ToUpperFirst(this string str)
        {
            if (str == null) return null;
            string temp = new string(str);
            fixed (char* ptr = temp)
                *ptr = char.ToUpper(*ptr);
            return temp;
        }
    }
}
