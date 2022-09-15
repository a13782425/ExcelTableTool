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
        public static unsafe string ToLowerFirst(this string str)
        {
            if (str == null) return null;
            string temp = new string(str);
            fixed (char* ptr = temp)
                *ptr = char.ToLower(*ptr);
            return temp;
        }
        /// <summary>
        /// ÍÕ·å×ª»¯
        /// </summary>
        /// <param name="waitStr"></param>
        /// <returns></returns>
        public static string ToHump(this string waitStr)
        {
            string[] strItems = waitStr.Split('_');
            string strItemTarget = strItems[0];
            for (int j = 1; j < strItems.Length; j++)
            {
                string temp = strItems[j].ToString();
                string temp1 = temp[0].ToString().ToUpper();
                string temp2 = "";
                temp2 = temp1 + temp.Remove(0, 1);
                strItemTarget += temp2;
            }
            return strItemTarget;
        }
    }
}
