using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableCore;

namespace JavaFormat
{
    internal sealed class BoolParse : IParseValue
    {
        public string Name => "bool";

        public bool Parse(string value, out object res)
        {
            res = false;
            if (string.IsNullOrWhiteSpace(value))
            {
                res = false;
                return true;
            }
            res = Convert.ToBoolean(value);
            return true;
            //if (int.TryParse(value, out var result))
            //{
            //    res = result == 1;
            //    return true;
            //}
            //return false;
        }
    }
}
